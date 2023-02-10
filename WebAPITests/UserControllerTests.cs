using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAPI.Controllers;
using System.Collections.Generic;
using FakeItEasy;
using MediatR;
using UserActions = ApplicationServices.Domain.UserActions;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;


namespace WebAPITests;

[TestClass]
public class UserControllerTests
{
    [TestMethod]
    public async Task GetAllUsers_AuthorizedContentExists_ReturnUsers()
    {
        //arrange
        var users = new List<ApplicationServices.Domain.Models.User>();
        users.Add(new ApplicationServices.Domain.Models.User());
        users.Add(new ApplicationServices.Domain.Models.User());

        var fakeMediatior = A.Fake<IMediator>();

        A.CallTo(() => fakeMediatior.Send(A<UserActions.Queries.CheckIfUserExistsQuery>.Ignored, default(CancellationToken))).Returns(true);
        A.CallTo(() => fakeMediatior.Send(A<UserActions.Queries.CheckIfUserIsAdminQuery>.Ignored, default(CancellationToken))).Returns(true);
        A.CallTo(() => fakeMediatior.Send(A<UserActions.Queries.GetAllUsersQuery>.Ignored, default(CancellationToken))).Returns(users);
        A.CallTo(() => fakeMediatior.Send(A<ApplicationServices.Graph.UserActions.Queries.GetUsersQuery>.Ignored, default(CancellationToken))).Returns(users);

        var controller = new UserController(fakeMediatior);
        controller.ControllerContext = CreateControllerContext();

        //act
        var result = await controller.GetAllUsers();

        //assert
        var actionResult = result.Result as OkObjectResult;
        var returnedUsers = actionResult!.Value as List<ApplicationServices.Domain.Models.User>;
        Assert.AreEqual(200, actionResult?.StatusCode);
        Assert.IsInstanceOfType(returnedUsers, typeof(List<ApplicationServices.Domain.Models.User>));
        Assert.AreEqual(2, returnedUsers!.Count());
    }

    [TestMethod]
    public async Task GetAllUsers_UserDoesNotExist_ReturnUnauthorized()
    {
        //arrange
        var fakeMediatior = A.Fake<IMediator>();

        A.CallTo(() => fakeMediatior.Send(A<UserActions.Queries.CheckIfUserExistsQuery>.Ignored, default(CancellationToken))).Returns(false);

        var controller = new UserController(fakeMediatior);
        controller.ControllerContext = CreateControllerContext();

        //act
        var result = await controller.GetAllUsers();

        //assert
        var actionResult = result.Result as UnauthorizedResult;
        Assert.IsInstanceOfType(actionResult, typeof(UnauthorizedResult), "Result is not an UnauthorizedResult");
    }

    [TestMethod]
    public async Task GetAllUsers_UserIsNotAdmin_ReturnUnauthorized()
    {
        //arrange
        var fakeMediatior = A.Fake<IMediator>();

        A.CallTo(() => fakeMediatior.Send(A<UserActions.Queries.CheckIfUserExistsQuery>.Ignored, default(CancellationToken))).Returns(true);
        A.CallTo(() => fakeMediatior.Send(A<UserActions.Queries.CheckIfUserIsAdminQuery>.Ignored, default(CancellationToken))).Returns(false);

        var controller = new UserController(fakeMediatior);
        controller.ControllerContext = CreateControllerContext();

        //act
        var result = await controller.GetAllUsers();

        //assert
        var actionResult = result.Result as UnauthorizedResult;
        Assert.IsInstanceOfType(actionResult, typeof(UnauthorizedResult), "Result is not an UnauthorizedResult");
    }

    [TestMethod]
    public async Task GetAllUsers_AuthorizedNoContent_ReturnNoContent()
    {
        //arrange
        var users = new List<ApplicationServices.Domain.Models.User>();

        var fakeMediatior = A.Fake<IMediator>();

        A.CallTo(() => fakeMediatior.Send(A<UserActions.Queries.CheckIfUserExistsQuery>.Ignored, default(CancellationToken))).Returns(true);
        A.CallTo(() => fakeMediatior.Send(A<UserActions.Queries.CheckIfUserIsAdminQuery>.Ignored, default(CancellationToken))).Returns(true);
        A.CallTo(() => fakeMediatior.Send(A<UserActions.Queries.GetAllUsersQuery>.Ignored, default(CancellationToken))).Returns(users);


        var controller = new UserController(fakeMediatior);
        controller.ControllerContext = CreateControllerContext();

        //act
        var result = await controller.GetAllUsers();

        //assert
        var actionResult = result.Result as NoContentResult;
        Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Result is not an NoContentResult");
    }

    [TestMethod]
    public async Task CheckIfUserIsAdmin_UserIsAdmin_ReturnTrue()
    {
        //arrange
        var fakeMediatior = A.Fake<IMediator>();

        A.CallTo(() => fakeMediatior.Send(A<UserActions.Queries.CheckIfUserExistsQuery>.Ignored, default(CancellationToken))).Returns(true);
        A.CallTo(() => fakeMediatior.Send(A<UserActions.Queries.CheckIfUserIsAdminQuery>.Ignored, default(CancellationToken))).Returns(true);

        var controller = new UserController(fakeMediatior);
        controller.ControllerContext = CreateControllerContext();

        //act
        var result = await controller.CheckIfUserIsAdmin();

        //assert
        var actionResult = result.Result as OkObjectResult;
        Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult), "Result is not an OkObjectResult");
        var isUserAdmin = actionResult!.Value as bool?;
        Assert.AreEqual(true, isUserAdmin);
    }

    public async Task CheckIfUserIsAdmin_UserDoesNotExist_ReturnFalse()
    {
        //arrange
        var fakeMediatior = A.Fake<IMediator>();

        A.CallTo(() => fakeMediatior.Send(A<UserActions.Queries.CheckIfUserExistsQuery>.Ignored, default(CancellationToken))).Returns(false);

        var controller = new UserController(fakeMediatior);
        controller.ControllerContext = CreateControllerContext();

        //act
        var result = await controller.CheckIfUserIsAdmin();

        //assert
        var actionResult = result.Result as OkObjectResult;
        Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult), "Result is not an OkObjectResult");
        var isUserAdmin = actionResult!.Value as bool?;
        Assert.AreEqual(false, isUserAdmin);
    }

    public async Task CheckIfUserIsAdmin_UserIsNotAdmin_ReturnFalse()
    {
        //arrange
        var fakeMediatior = A.Fake<IMediator>();

        A.CallTo(() => fakeMediatior.Send(A<UserActions.Queries.CheckIfUserExistsQuery>.Ignored, default(CancellationToken))).Returns(true);
        A.CallTo(() => fakeMediatior.Send(A<UserActions.Queries.CheckIfUserIsAdminQuery>.Ignored, default(CancellationToken))).Returns(false);

        var controller = new UserController(fakeMediatior);
        controller.ControllerContext = CreateControllerContext();

        //act
        var result = await controller.CheckIfUserIsAdmin();

        //assert
        var actionResult = result.Result as OkObjectResult;
        Assert.IsInstanceOfType(actionResult, typeof(OkObjectResult), "Result is not an OkObjectResult");
        var isUserAdmin = actionResult!.Value as bool?;
        Assert.AreEqual(false, isUserAdmin);
    }

    private Microsoft.AspNetCore.Mvc.ControllerContext CreateControllerContext()
    {
        var claim = new System.Security.Claims.Claim("http://schemas.microsoft.com/identity/claims/objectidentifier", "mockedValue");
        var claimList = new List<System.Security.Claims.Claim>();
        claimList.Add(claim);
        var identities = new System.Security.Claims.ClaimsIdentity(claimList);
        var claimsPrincipal = new System.Security.Claims.ClaimsPrincipal(identities);
        var controllerContext = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = claimsPrincipal } };
        return controllerContext;
    }
}