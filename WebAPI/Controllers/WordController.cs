using Microsoft.AspNetCore.Mvc;
using DAL;
using MediatR;
using ApplicationServices.Domain.WordActions.Queries;
using ApplicationServices.Domain.WordActions.Commands;
using ApplicationServices.Domain.Models;
using ApplicationServices.Domain.PartOfSpeechActions.Queries;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class WordController : ControllerBase
{
    private readonly ILogger<WordController> _logger;
    private readonly IMediator _mediator;

    public WordController(ILogger<WordController> logger, IMediator mediator)
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
        _logger.LogError(exceptionHandlerFeature.Error, "Error");
        return Problem();
    }


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Words>> GetWords(string sortBy, bool ascendingOrder, int pageNumber = 0, int wordsPerPage = 20)
    {
        var response = await _mediator.Send(new GetWordsQuery()
        {
            AscendingOrder = ascendingOrder,
            SortBy = sortBy,
            PageNumber = pageNumber,
            WordsPerPage = wordsPerPage
        });
        return response;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<Word>> GetWord(string word, bool translateFromPolish)
    {
        var response = await _mediator.Send(new GetWordQuery() { Word = word, TranslateFromPolish = translateFromPolish });
        if (response == null) return NoContent();
        return response;
    }

    // [HttpGet]
    // public async Task<ActionResult<Word>> GetWord(int wordId)
    // {
    //     var response = await _mediator.Send(new GetWordByIdQuery() { WordId = wordId });
    //     if (response == null) return NotFound();
    //     return response;

    // }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> CreateWord(Word word)
    {
        var partOfSpeech = await _mediator.Send(new GetPartOfSpeechQuery() { Name = word.PartOfSpeech });
        var response = await _mediator.Send(new CreateWordCommand() { Word = word, PartOfSpeechId = partOfSpeech.PartOfSpeechId });
        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> EditWord(Word word)
    {
        var wordFromDB = await _mediator.Send(new GetWordByIdQuery() { WordId = word.Id });
        if (wordFromDB.Word == null) return NotFound();

        var response = await _mediator.Send(new EditWordCommand() { Word = word, PartOfSpeechId = (int)wordFromDB.PartOfSpeechId });
        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteWord(int wordId)
    {
        var wordFromDB = await _mediator.Send(new GetWordByIdQuery() { WordId = wordId });
        if (wordFromDB.Word == null) return NotFound();

        var response = await _mediator.Send(new DeleteWordCommand() { WordId = wordId });
        return NoContent();
    }
}
