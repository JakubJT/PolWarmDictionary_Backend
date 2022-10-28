using Microsoft.EntityFrameworkCore;
using System.Linq;
using DAL.Models;

namespace DAL;

public class Repository<T> : IRepository<T> where T : Models.ModelBase
{
    protected readonly DictionaryContext Context;
    protected DbSet<T> Items;

    public Repository(DictionaryContext context)
    {
        Context = context;
        Items = context.Set<T>();
    }
    public DbSet<T> GetAllItemsOld()
    {
        return Items;
    }
}