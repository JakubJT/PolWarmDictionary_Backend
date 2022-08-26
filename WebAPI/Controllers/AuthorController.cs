using Microsoft.AspNetCore.Mvc;
using DAL.Models;
using System.Threading.Tasks;
using System.Linq;
using DAL;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AuthorController : ControllerBase
{

    private readonly ILogger<AuthorController> _logger;
    private readonly DictionaryContext _dictionaryContext;

    public AuthorController(ILogger<AuthorController> logger, DictionaryContext dictionaryContext)
    {
        _logger = logger;
        _dictionaryContext = dictionaryContext;
    }

    [HttpGet]
    public async Task<ActionResult<DAL.Models.Author>> GetAuthor(int AuthorId)
    {
        return await _dictionaryContext.Authors?.FirstOrDefaultAsync(wordGroup => wordGroup.AuthorId == AuthorId);
    }

}