using Microsoft.AspNetCore.Mvc;
using DAL;
using MediatR;
using ApplicationServices.Domain.WordActions.Queries;
using ApplicationServices.Domain.Models;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class WordController : ControllerBase
{

    private readonly ILogger<WordController> _logger;
    private readonly IMediator _mediator;

    public WordController(ILogger<WordController> logger, DictionaryContext dictionaryContext, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
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
}
