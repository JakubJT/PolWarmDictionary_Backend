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
        var wordGroups = await _context.WordGroups!.Include(wg => wg.Words).Where(wg => wg.UserADId == userADId).ToListAsync();
        return wordGroups;

    }

    public async Task<WordGroup> GetWordGroup(int wordGroupId)
    {
        return await _context.WordGroups!.AsNoTracking().FirstOrDefaultAsync(wg => wg.WordGroupId == wordGroupId);
    }

    public async Task CreateWordGroup(WordGroup wordGroup)
    {
        _context.Add(wordGroup);
        await _context.SaveChangesAsync();
    }
    public async Task EditWordGroup(WordGroup wordGroup)
    {
        _context.Update(wordGroup);
        await _context.SaveChangesAsync();
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
                    .SingleOrDefaultAsync(wg => wg.Name == wordGroupName && wg.UserADId == userADId);
        if (wordGroup == default) return false;
        return true;
    }

    public async Task<bool> CheckIfUserIsAuthorized(string userADId, int wordGroupId)
    {
        var wordGroup = await _context.WordGroups!
                    .SingleOrDefaultAsync(wg => wg.WordGroupId == wordGroupId && wg.UserADId == userADId);
        if (wordGroup == default) return false;
        return true;
    }

}