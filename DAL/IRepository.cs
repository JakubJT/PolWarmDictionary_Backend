using Microsoft.EntityFrameworkCore;
using DAL.Models;

namespace DAL;

public interface IRepository<T> where T : Models.ModelBase
{
    public DbSet<T> GetAllItemsOld();
}