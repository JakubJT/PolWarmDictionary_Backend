using Microsoft.EntityFrameworkCore;
using System.Linq;
using DAL.Models;

namespace DAL;

public class WordRepository : Repository<Word>
{
    public WordRepository(DictionaryContext context) : base(context)
    {
    }

    public async Task<Word> GetItem(int itemId)
    {
        return await Items.FirstOrDefaultAsync();
    }

    public async Task<Word> GetWord(string word, bool translateFromPolish)
    {
        if (translateFromPolish) return await Context.Words.Include(w => w.PartOfSpeech).FirstOrDefaultAsync(wx => wx.InPolish == word);
        else return await Context.Words.Include(w => w.PartOfSpeech).FirstOrDefaultAsync(wx => wx.InWarmian == word);
    }

    public async Task<Word> GetWordById(int id)
    {
        return await Context.Words.AsNoTracking().FirstOrDefaultAsync(w => w.WordId == id);
    }

    public async Task<Word> GetWordWithIncludes(int wordId)
    {
        return await Context.Words
            .Include(w => w.PartOfSpeech)
            .Include(w => w.Author)
            .FirstOrDefaultAsync(wx => wx.WordId == wordId);
    }

    public async Task CreateWord(Word word)
    {
        Context.Words.Add(word);
        await Context.SaveChangesAsync();

    }
    public async Task EditWord(Word word)
    {
        Context.Update(word);
        await Context.SaveChangesAsync();
    }

    public async Task DeleteWord(int wordId)
    {
        var wordToDelete = new Word() { WordId = wordId };
        Context.Words.Remove(wordToDelete);
        await Context.SaveChangesAsync();
    }

}