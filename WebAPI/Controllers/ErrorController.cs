using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics;

namespace WebAPI.Controllers;

public class ErrorController : ControllerBase
{
    private readonly ILogger<ErrorController> _logger;

    public ErrorController(ILogger<ErrorController> logger)
    {
        _logger = logger;
    }

    [Route("/error")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult HandleError()
    {
        var exceptionHandler = HttpContext.Features.Get<IExceptionHandlerFeature>()!;
        _logger.LogError(exceptionHandler.Error, $"({DateTime.UtcNow}) ");
        return Problem();
    }

    [Route("/error-indevelopment")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult HandleErrorInDevelopment(
    [FromServices] IHostEnvironment hostEnvironment)
    {
        if (!hostEnvironment.IsDevelopment()) return NotFound();

        var exceptionHandler = HttpContext.Features.Get<IExceptionHandlerFeature>()!;
        string? stackTrace = exceptionHandler.Error.StackTrace?.TrimStart();
        string? message = exceptionHandler.Error.Message;
        return Problem(detail: stackTrace, title: message);
    }
}