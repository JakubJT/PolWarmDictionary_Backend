using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using ApplicationServices.Domain.UserActions.Queries;
using Graph = ApplicationServices.Graph;
using ApplicationServices.Domain.Models;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<User>>> GetAllUsers()
    {
        var users = await _mediator.Send(new GetAllUsersQuery());
        if (users.Count() == 0) return NoContent();

        users = await _mediator.Send(new Graph.UserActions.Queries.GetUsersQuery() { UsersFromDB = users });
        return users;
    }
}
