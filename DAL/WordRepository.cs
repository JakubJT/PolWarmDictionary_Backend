using Microsoft.EntityFrameworkCore;
using DAL.Models;

namespace DAL;

public class WordRepository : Repository<Word>
{
    public WordRepository(DictionaryContext context) : base(context)
    {
    }

    public async Task<(List<Word> Words, int NumbeOfPages)> GetWords(bool ascendingOrder, string sortBy, int pageNumber, int wordsPerPage)
    {
        IQueryable<Word> words = from w in Context.Words
                                 select w;
        int wordsCount = await words.CountAsync();
        int numbeOfPages;
        if ((wordsCount % wordsPerPage) == 0) numbeOfPages = wordsCount / wordsPerPage;
        else numbeOfPages = wordsCount / wordsPerPage + 1;

        if (ascendingOrder)
        {
            switch (sortBy)
            {
                case "InPolish":
                    words = words.OrderBy(w => w.InPolish);
                    break;
                case "InWarmian":
                    words = words.OrderBy(w => w.InWarmian);
                    break;
                case "PartOfSpeech":
                    words = words.OrderBy(w => w.PartOfSpeech.Name);
                    break;
            }
        }

        else
        {
            switch (sortBy)
            {
                case "InPolish":
                    words = words.OrderByDescending(w => w.InPolish);
                    break;
                case "InWarmian":
                    words = words.OrderByDescending(w => w.InWarmian);
                    break;
                case "PartOfSpeech":
                    words = words.OrderByDescending(w => w.PartOfSpeech.Name);
                    break;
            }
        }

        words = words.Skip((pageNumber - 1) * wordsPerPage)
                    .Take(wordsPerPage)
                    .Include(w => w.PartOfSpeech)
                    .AsNoTracking();

        return (await words.ToListAsync(), numbeOfPages);
    }

    public async Task<List<Word>> GetWord(string word, bool translateFromPolish)
    {
        if (translateFromPolish) return await Context.Words.Include(w => w.PartOfSpeech).Where(wx => wx.InPolish == word).ToListAsync();
        else return await Context.Words.Include(w => w.PartOfSpeech).Where(wx => wx.InWarmian == word).ToListAsync();
    }

    public async Task<Word> GetWordById(int id)
    {
        return await Context.Words.AsNoTracking().Include(w => w.PartOfSpeech).FirstOrDefaultAsync(wx => wx.WordId == id);
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

    public async Task<bool> CheckIfWordExists(Word word)
    {
        var wordFromDB = await Context.Words.
                            SingleOrDefaultAsync(w => w.InPolish == word.InPolish && w.InWarmian == word.InWarmian && w.PartOfSpeechId == word.PartOfSpeechId);
        if (wordFromDB == default) return false;
        else return true;
    }

}