using Microsoft.EntityFrameworkCore;
using DAL.Models;

namespace DAL;

public class UserRepository
{
    private readonly DictionaryContext _context;
    public UserRepository(DictionaryContext context)
    {
        _context = context;
    }

    // public async Task<List<WordGroup>> GetAllWordGroups(string userADId)
    // {
    //     var wordGroups = await _context.WordGroups.Where(wg => wg.UserADId == userADId).ToListAsync();
    //     return wordGroups;

    // }

    // public async Task<WordGroup> GetWordGroup(int wordGroupId)
    // {
    //     return await _context.WordGroups.AsNoTracking().FirstOrDefaultAsync(wg => wg.WordGroupId == wordGroupId);
    // }

    public async Task CreateUser(User user)
    {
        _context.Add(user);
        await _context.SaveChangesAsync();
    }
    // public async Task EditWordGroup(WordGroup wordGroup)
    // {
    //     _context.Update(wordGroup);
    //     await _context.SaveChangesAsync();
    // }

    // public async Task DeleteWordGroup(int wordGroupId)
    // {
    //     var wordGroupToDelete = new WordGroup() { WordGroupId = wordGroupId };
    //     _context.WordGroups.Remove(wordGroupToDelete);
    //     await _context.SaveChangesAsync();
    // }

    public async Task<bool> CheckIfUserExists(string userADId)
    {
        var user = await _context.Users!
                    .SingleOrDefaultAsync(u => u.UserADId == userADId);
        if (user == default) return false;
        return true;
    }

}