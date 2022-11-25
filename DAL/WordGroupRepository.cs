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

    public async Task<List<WordGroup>> GetAllWordGroups(string userIdentifier)
    {
        var wordGroups = await _context.WordGroups.Where(wg => wg.UserIdentifier == userIdentifier).ToListAsync();
        return wordGroups;

    }

    public async Task<WordGroup> GetWordGroup(int wordGroupId)
    {
        return await _context.WordGroups.AsNoTracking().FirstOrDefaultAsync(wg => wg.WordGroupId == wordGroupId);
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
        _context.WordGroups.Remove(wordGroupToDelete);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> CheckIfWordGroupExists(string userIdentifier, string wordGroupName)
    {
        var wordGroup = await _context.WordGroups
                    .SingleOrDefaultAsync(wg => wg.Name == wordGroupName && wg.UserIdentifier == userIdentifier);
        if (wordGroup == default) return false;
        return true;
    }

    public async Task<bool> CheckIfUserIsAuthorized(string userIdentifier, int wordGroupId)
    {
        var wordGroup = await _context.WordGroups
                    .SingleOrDefaultAsync(wg => wg.WordGroupId == wordGroupId && wg.UserIdentifier == userIdentifier);
        if (wordGroup == default) return false;
        return true;
    }

}