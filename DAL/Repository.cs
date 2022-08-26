using Microsoft.EntityFrameworkCore;
using System.Linq;
using DAL.Models;

namespace DAL;

public class Repository<T> : IRepository<T> where T : Models.ModelBase
{
    private readonly DictionaryContext _context;
    private DbSet<T> _items;

    public Repository(DictionaryContext context)
    {
        _context = context;
        _items = context.Set<T>();
    }
    public DbSet<T> GetAllItems()
    {
        return _items;
    }

    public async Task<T> GetItem(int itemId)
    {
        return await _items.FirstOrDefaultAsync();
    }

    public async Task<Word> GetWord(string word, bool translateFromPolish)
    {
        if (translateFromPolish) return await _context.Words.Include(w => w.PartOfSpeech).FirstOrDefaultAsync(wx => wx.InPolish == word);
        else return await _context.Words.Include(w => w.PartOfSpeech).FirstOrDefaultAsync(wx => wx.InWarmian == word);
    }
}