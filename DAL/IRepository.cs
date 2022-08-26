using Microsoft.EntityFrameworkCore;
using DAL.Models;

namespace DAL;

public interface IRepository<T> where T : Models.ModelBase
{
    public DbSet<T> GetAllItems();
    public Task<T> GetItem(int itemId);
    public Task<Word> GetWord(string word, bool translateFromPolish);
}