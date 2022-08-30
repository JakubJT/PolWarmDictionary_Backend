using Microsoft.AspNetCore.Mvc;
using DAL;
using MediatR;
using ApplicationServices.Domain.WordActions.Queries;
using ApplicationServices.Domain.WordActions.Commands;
using ApplicationServices.Domain.Models;
using ApplicationServices.Domain.PartOfSpeechActions.Queries;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class WordController : ControllerBase
{
    private readonly DictionaryContext _dictionaryContext;
    private readonly ILogger<WordController> _logger;
    private readonly IMediator _mediator;

    public WordController(ILogger<WordController> logger, DictionaryContext dictionaryContext, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
        _dictionaryContext = dictionaryContext;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Word>>> GetAllWords([FromQuery] GetWordsQuery request)
    {
        var response = await _mediator.Send(request);
        return this.Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<Word>> GetWord(string word, bool translateFromPolish)
    {
        var response = await _mediator.Send(new GetWordQuery() { Word = word, TranslateFromPolish = translateFromPolish });
        if (response == null) return NotFound();
        return response;

    }
    [HttpPost]
    public async Task<IActionResult> EditWord(Word word)
    {
        var partOfSpeech = await _mediator.Send(new GetPartOfSpeechQuery() { Name = word.PartOfSpeech });
        var response = await _mediator.Send(new EditWordCommand() { Word = word, PartOfSpeechId = partOfSpeech.PartOfSpeechId });
        return NoContent();
    }
}
