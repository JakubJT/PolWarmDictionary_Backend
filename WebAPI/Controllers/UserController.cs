using Microsoft.AspNetCore.Mvc;
using DAL.Models;
using System.Threading.Tasks;
using System.Linq;
using DAL;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class UserController : ControllerBase
{

    private readonly ILogger<UserController> _logger;
    private readonly DictionaryContext _dictionaryContext;

    public UserController(ILogger<UserController> logger, DictionaryContext dictionaryContext)
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