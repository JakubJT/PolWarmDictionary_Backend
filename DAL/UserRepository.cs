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

    public async Task<List<User>> GetAllUsers()
    {
        var users = await _context.Users!
        .AsNoTracking()
        .Include(u => u.WordGroups)
        .ToListAsync();

        return users;
    }

    public async Task<User> GetUserById(int userId)
    {
        var user = await _context.Users!
        .AsNoTracking()
        .FirstOrDefaultAsync(u => u.UserId == userId);

        return user!;
    }

    public async Task CreateUser(User user)
    {
        _context.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> CheckIfUserExists(string userADId)
    {
        var user = await _context.Users!
                    .SingleOrDefaultAsync(u => u.UserADId == userADId);
        if (user == default) return false;
        return true;
    }

}