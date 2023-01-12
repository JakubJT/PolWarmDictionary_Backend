using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using ApplicationServices.Domain.UserActions.Queries;
using Models = ApplicationServices.Domain.Models;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly GraphClient _graphClient;

    public UserController(IMediator mediator, GraphClient graphClient)
    {
        _mediator = mediator;
        _graphClient = graphClient;
    }

    [HttpGet]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<Models.User>>> GetAllUsers()
    {
        var users = await _mediator.Send(new GetAllUsersQuery());
        if (users == null) return NoContent();

        var graphClient = _graphClient.GetClient();
        foreach (var user in users)
        {
            var userAD = await graphClient.Users[$"{user.ADId}"]
                .Request()
                .Select("displayName,mail")
                .GetAsync();

            user.Email = userAD.Mail;
            user.Name = userAD.DisplayName;
        }
        return users;
    }
}
