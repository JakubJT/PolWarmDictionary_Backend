using Microsoft.EntityFrameworkCore;
using DAL.Models;

namespace DAL;

public class WordGroupRepository
{
    private readonly DictionaryContext _context;
    public WordGroupRepository(DictionaryContext context)
    {
        _context = context;
    }

    public async Task<List<WordGroup>> GetAllWordGroups(string userADId)
    {
        var wordGroups = await _context.WordGroups!
            .AsNoTracking()
            .Include(wg => wg.Words)
            .Where(wg => wg.UserADId == userADId)
            .ToListAsync();
        return wordGroups;

    }

    public async Task<WordGroup> GetWordGroup(int wordGroupId)
    {
        var wordGroup = await _context.WordGroups!
            .AsNoTracking()
            .FirstOrDefaultAsync(wg => wg.WordGroupId == wordGroupId);
        return wordGroup!;
    }

    public async Task CreateWordGroup(WordGroup wordGroup)
    {
        foreach (var word in wordGroup.Words)
        {
            _context.Attach(word);
        }
        _context.Add(wordGroup);
        await _context.SaveChangesAsync();
    }
    public async Task EditWordGroup(WordGroup wordGroup)
    {
        using (var transaction = _context.Database.BeginTransaction())
        {

            var wordGroupFromDB = _context.WordGroups!
                .Include(wg => wg.Words)
                .FirstOrDefault(wg => wg.WordGroupId == wordGroup.WordGroupId);

            _context.Entry(wordGroupFromDB!).CurrentValues.SetValues(wordGroup);

            foreach (var word in wordGroup.Words)
            {
                var wordFromDB = wordGroupFromDB.Words
                    .FirstOrDefault(w => w.WordId == word.WordId);
                if (wordFromDB == null) wordGroupFromDB.Words.Add(word);
                else _context.Entry(wordFromDB).CurrentValues.SetValues(word);
            }
            _context.SaveChanges();

            foreach (var word in wordGroupFromDB.Words)
            {
                if (!wordGroup.Words.Any(w => w.WordId == word.WordId))
                {
                    _context.Database.ExecuteSqlInterpolated($"DELETE FROM dbo.WordWordGroup WHERE WordsWordId = {word.WordId} AND WordGroupsWordGroupId = {wordGroupFromDB.WordGroupId}");
                }
            }
            await transaction.CommitAsync();
        }
    }

    public async Task DeleteWordGroup(int wordGroupId)
    {
        var wordGroupToDelete = new WordGroup() { WordGroupId = wordGroupId };
        _context.WordGroups!.Remove(wordGroupToDelete);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> CheckIfWordGroupExists(string userADId, string wordGroupName)
    {
        var wordGroup = await _context.WordGroups!
                    .AsNoTracking()
                    .SingleOrDefaultAsync(wg => wg.Name == wordGroupName && wg.UserADId == userADId);
        if (wordGroup == default) return false;
        return true;
    }

    public async Task<bool> CheckIfUserIsAuthorized(string userADId, int wordGroupId)
    {
        var wordGroup = await _context.WordGroups!
                    .AsNoTracking()
                    .SingleOrDefaultAsync(wg => wg.WordGroupId == wordGroupId && wg.UserADId == userADId);
        if (wordGroup == default) return false;
        return true;
    }

}