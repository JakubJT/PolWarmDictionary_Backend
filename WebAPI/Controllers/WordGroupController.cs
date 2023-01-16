using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using ApplicationServices.Domain.WordGroupActions.Queries;
using ApplicationServices.Domain.WordGroupActions.Commands;
using ApplicationServices.Domain.UserActions.Queries;
using ApplicationServices.Domain.UserActions.Commands;
using ApplicationServices.Domain.Models;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class WordGroupController : ControllerBase
{
    private readonly IMediator _mediator;

    public WordGroupController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<WordGroup>>> GetAllWordGroups()
    {
        string? userADId = GetUserADId();
        var response = await _mediator.Send(new GetAllWordGroupsQuery()
        {
            UserADId = userADId
        });
        if (response.Count() == 0) return NoContent();
        return response;
    }

    [HttpGet]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<WordGroup>> GetWordGroup(int wordGroupId)
    {
        string? userADId = GetUserADId();
        var isUserAuthorized = await _mediator.Send(new CheckIfUserIsAuthorizedQuery()
        {
            UserADId = userADId,
            WordGroupId = wordGroupId
        });
        if (isUserAuthorized == false) return Unauthorized();

        var wordGroupFromDB = await _mediator.Send(new GetWordGroupQuery() { WordGroupId = wordGroupId });
        if (wordGroupFromDB == null) return NotFound();
        return wordGroupFromDB;
    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> CreateWordGroup(WordGroup wordGroup)
    {
        await CreateUserIfDoesNotExist();

        string? userADId = GetUserADId();

        bool wordGroupAlreadyExists = await _mediator.Send(new CheckIfWordGroupExistsQuery()
        {
            UserADId = userADId,
            WordGroupName = wordGroup.Name
        });
        if (wordGroupAlreadyExists) return Conflict();

        var response = await _mediator.Send(new CreateWordGroupCommand()
        {
            WordGroup = wordGroup,
            UserADId = userADId
        });
        return NoContent();
    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> EditWordGroup(WordGroup wordGroup)
    {
        var wordGroupFromDB = await _mediator.Send(new GetWordGroupQuery() { WordGroupId = wordGroup.WordGroupId });
        if (wordGroupFromDB == null) return NotFound();

        string? userADId = GetUserADId();
        var isUserAuthorized = await _mediator.Send(new CheckIfUserIsAuthorizedQuery()
        {
            UserADId = userADId,
            WordGroupId = wordGroup.WordGroupId
        });
        if (isUserAuthorized == false) return Unauthorized();

        if (wordGroupFromDB.Name != wordGroup.Name)
        {
            bool wordGroupAlreadyExists = await _mediator.Send(new CheckIfWordGroupExistsQuery()
            {
                UserADId = userADId,
                WordGroupName = wordGroup.Name
            });
            if (wordGroupAlreadyExists) return Conflict();
        }

        var response = await _mediator.Send(new EditWordGroupCommand() { WordGroup = wordGroup });
        return NoContent();

    }

    [HttpDelete]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteWordGroup(int wordGroupId)
    {
        var wordGroupFromDB = await _mediator.Send(new GetWordGroupQuery() { WordGroupId = wordGroupId });
        if (wordGroupFromDB == null) return NotFound();

        string? userADId = GetUserADId();
        var isUserAuthorized = await _mediator.Send(new CheckIfUserIsAuthorizedQuery()
        {
            UserADId = userADId,
            WordGroupId = wordGroupId
        });
        if (isUserAuthorized == false) return Unauthorized();

        var response = await _mediator.Send(new DeleteWordGroupCommand() { WordGroupId = wordGroupId });
        return NoContent();
    }

    [HttpGet]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> CheckIfWordGroupExists(string wordGroupName)
    {
        string? userADId = GetUserADId();
        bool wordGroupAlreadyExists = await _mediator.Send(new CheckIfWordGroupExistsQuery()
        {
            UserADId = userADId,
            WordGroupName = wordGroupName
        });
        if (wordGroupAlreadyExists) return Conflict();
        return NoContent();
    }

    [HttpGet]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> CheckIfUserIsAuthorized(int wordGroupId)
    {
        string? userADId = GetUserADId();
        var isUserAuthorized = await _mediator.Send(new CheckIfUserIsAuthorizedQuery()
        {
            UserADId = userADId,
            WordGroupId = wordGroupId
        });
        if (isUserAuthorized == false) return Unauthorized();
        return NoContent();
    }

    private string GetUserADId() => User.Claims.First(c => c.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier").Value;

    private async Task CreateUserIfDoesNotExist()
    {
        string? userADId = GetUserADId();

        bool userAlreadyExists = await _mediator.Send(new CheckIfUserExistsQuery() { UserADId = userADId });
        if (userAlreadyExists == false)
        {
            await _mediator.Send(new CreateUserCommand() { UserADId = userADId });
        }
    }
}
