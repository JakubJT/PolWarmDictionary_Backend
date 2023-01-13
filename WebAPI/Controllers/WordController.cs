using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using ApplicationServices.Domain.WordActions.Queries;
using ApplicationServices.Domain.WordActions.Commands;
using ApplicationServices.Domain.Models;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class WordController : ControllerBase
{
    private readonly IMediator _mediator;
    public WordController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<Words>> GetWords(string sortBy, bool ascendingOrder, int pageNumber = 0, int wordsPerPage = 20)
    {
        var response = await _mediator.Send(new GetWordsQuery()
        {
            AscendingOrder = ascendingOrder,
            SortBy = sortBy,
            PageNumber = pageNumber,
            WordsPerPage = wordsPerPage
        });
        if (response.WordList!.Count() == 0) return NoContent();
        return response;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<List<Word>>> GetWord(string word, bool translateFromPolish)
    {
        var response = await _mediator.Send(new GetWordQuery() { Word = word, TranslateFromPolish = translateFromPolish });
        if (response.Count == 0) return NoContent();
        return response;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Word>> GetWordById(int wordId)
    {
        var response = await _mediator.Send(new GetWordByIdQuery() { WordId = wordId });
        if (response.Word == null) return NotFound();
        return response.Word;

    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> CreateWord(Word word)
    {
        var wordAlreadyExists = await _mediator.Send(new CheckIfWordExistsQuery() { Word = word });

        if (wordAlreadyExists)
        {
            return Conflict();
        }
        else
        {
            var response = await _mediator.Send(new CreateWordCommand() { Word = word });
            return NoContent();
        }
    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> EditWord(Word word)
    {
        var wordFromDB = await _mediator.Send(new GetWordByIdQuery() { WordId = word.Id });
        if (wordFromDB.Word == null) return NotFound();

        var wordAlreadyExists = await _mediator.Send(new CheckIfWordExistsQuery() { Word = word });

        if (wordAlreadyExists)
        {
            return Conflict();
        }
        else
        {
            var response = await _mediator.Send(new EditWordCommand() { Word = word });
            return NoContent();
        }
    }

    [HttpDelete]
    [Authorize]
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
