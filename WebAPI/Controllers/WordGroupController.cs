using Microsoft.AspNetCore.Mvc;
using DAL.Models;
using System.Threading.Tasks;
using System.Linq;
using DAL;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class WordGroupController : ControllerBase
{

    private readonly ILogger<WordGroupController> _logger;
    private readonly DictionaryContext _dictionaryContext;

    public WordGroupController(ILogger<WordGroupController> logger, DictionaryContext dictionaryContext)
    {
        _logger = logger;
        _dictionaryContext = dictionaryContext;
    }

    [HttpGet(Name = "GetWordGroups")]
    public async Task<ActionResult<IEnumerable<DAL.Models.WordGroup>>> GetWordGroups()
    {
        return await _dictionaryContext.WordGroups.ToListAsync();
    }

    [HttpGet]
    public async Task<ActionResult<DAL.Models.WordGroup>> GetWordGroup(int WordGroupId)
    {
        return await _dictionaryContext.WordGroups?.FirstOrDefaultAsync(wordGroup => wordGroup.WordGroupId == WordGroupId);
    }
}
