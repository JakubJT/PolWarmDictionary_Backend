using Microsoft.AspNetCore.Mvc;
using DAL.Models;
using System.Threading.Tasks;
using System.Linq;
using DAL;
using Microsoft.EntityFrameworkCore;
using MediatR;
using ApplicationServices.Domain;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class PartOfSpeechController : ControllerBase
{

    private readonly ILogger<PartOfSpeechController> _logger;
    private readonly DictionaryContext _dictionaryContext;
    private readonly IMediator _mediator;

    public PartOfSpeechController(ILogger<PartOfSpeechController> logger, DictionaryContext dictionaryContext, IMediator mediator)
    {
        _logger = logger;
        _dictionaryContext = dictionaryContext;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<DAL.Models.PartOfSpeech>> GetPartOfSpeech(int PartOfSpeechId)
    {
        // var request = new GetPartOfSpeechRequest();
        // var response = _mediator.Send(request);

        return await _dictionaryContext.PartOfSpeeches?.FirstOrDefaultAsync(PartOfSpeech => PartOfSpeech.PartOfSpeechId == PartOfSpeechId);
    }

}