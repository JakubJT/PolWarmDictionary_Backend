using Microsoft.AspNetCore.Mvc;
using DAL.Models;
using System.Threading.Tasks;
using System.Linq;
using DAL;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class WordController : ControllerBase
{

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly DictionaryContext _dictionaryContext;

    public WordController(ILogger<WeatherForecastController> logger, DictionaryContext dictionaryContext)
    {
        _logger = logger;
        _dictionaryContext = dictionaryContext;
    }

    [HttpGet(Name = "GetWords")]
    public IEnumerable<Word> GetWords()
    {
        return _dictionaryContext.Words.ToList();
    }

    [HttpGet]
    public Word GetWordWar(string? ContentPol)
    {
        return _dictionaryContext.Words?.FirstOrDefault(word => word.ContentPol == ContentPol);
    }

    [HttpGet]
    public Word GetWordPol(string? ContentWar)
    {
        return _dictionaryContext.Words?.FirstOrDefault(word => word.ContentWar == ContentWar);
    }

}
