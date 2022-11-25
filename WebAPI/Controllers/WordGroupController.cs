using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web.Resource;
using MediatR;
using ApplicationServices.Domain.WordGroupActions.Queries;
using ApplicationServices.Domain.WordGroupActions.Commands;
using ApplicationServices.Domain.Models;
using Microsoft.AspNetCore.Diagnostics;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class WordGroupController : ControllerBase
{
    private readonly ILogger<WordController> _logger;
    private readonly IMediator _mediator;

    public WordGroupController(ILogger<WordController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [Route("/error-development")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult HandleErrorDevelopment(
    [FromServices] IHostEnvironment hostEnvironment)
    {
        if (!hostEnvironment.IsDevelopment())
        {
            return NotFound();
        }

        var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>()!;

        return Problem(
            detail: exceptionHandlerFeature.Error.StackTrace,
            title: exceptionHandlerFeature.Error.Message);
    }

    [Route("/error")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult HandleError()
    {
        var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>()!;
        _logger.LogError(exceptionHandlerFeature.Error, $"({DateTime.UtcNow}) ");
        return Problem();
    }


    [HttpGet]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<WordGroup>>> GetAllWordGroups()
    {
        string? userIdentifier = GetUserIdentifier();
        var response = await _mediator.Send(new GetAllWordGroupsQuery()
        {
            UserIdentifier = userIdentifier
        });
        return response;
    }

    [HttpGet]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<WordGroup>> GetWordGroup(int wordGroupId)
    {
        string? userIdentifier = GetUserIdentifier();
        var isUserAuthorized = await _mediator.Send(new CheckIfUserIsAuthorizedQuery()
        {
            UserIdentifier = userIdentifier,
            WordGroupId = wordGroupId
        });
        if (isUserAuthorized == false) return Unauthorized();

        var wordGroupFromDB = await _mediator.Send(new GetWordGroupQuery() { WordGroupId = wordGroupId });
        if (wordGroupFromDB == null) return NoContent();
        return wordGroupFromDB;
    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> CreateWordGroup(WordGroup wordGroup)
    {
        var response = await _mediator.Send(new CreateWordGroupCommand() { WordGroup = wordGroup });
        return NoContent();
    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> EditWordGroup(WordGroup wordGroup)
    {
        string? userIdentifier = GetUserIdentifier();
        var isUserAuthorized = await _mediator.Send(new CheckIfUserIsAuthorizedQuery()
        {
            UserIdentifier = userIdentifier,
            WordGroupId = wordGroup.WordGroupId
        });
        if (isUserAuthorized == false) return Unauthorized();

        var wordGroupFromDB = await _mediator.Send(new GetWordGroupQuery() { WordGroupId = wordGroup.WordGroupId });
        if (wordGroupFromDB == null) return NotFound();

        var response = await _mediator.Send(new EditWordGroupCommand() { WordGroup = wordGroup });
        return NoContent();

    }

    [HttpDelete]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteWordGroup(int wordGroupId)
    {
        string? userIdentifier = GetUserIdentifier();
        var isUserAuthorized = await _mediator.Send(new CheckIfUserIsAuthorizedQuery()
        {
            UserIdentifier = userIdentifier,
            WordGroupId = wordGroupId
        });
        if (isUserAuthorized == false) return Unauthorized();

        var wordGroupFromDB = await _mediator.Send(new GetWordGroupQuery() { WordGroupId = wordGroupId });
        if (wordGroupFromDB == null) return NotFound();

        var response = await _mediator.Send(new DeleteWordGroupCommand() { WordGroupId = wordGroupId });
        return NoContent();
    }

    [HttpGet]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CheckIfWordGroupExists(string wordGroupName)
    {
        string? userIdentifier = GetUserIdentifier();
        bool wordGroupAlreadyExists = await _mediator.Send(new CheckIfWordGroupExistsQuery() { UserIdentifier = userIdentifier, WordGroupName = wordGroupName });
        if (wordGroupAlreadyExists) return Conflict();
        return NoContent();
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CheckIfUserIsAuthorized(int wordGroupId)
    {
        string? userIdentifier = GetUserIdentifier();
        var isUserAuthorized = await _mediator.Send(new CheckIfUserIsAuthorizedQuery()
        {
            UserIdentifier = userIdentifier,
            WordGroupId = wordGroupId
        });
        if (isUserAuthorized == false) return Unauthorized();
        return NoContent();
    }

    private string? GetUserIdentifier() => User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
}
