using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

// namespace DAL
// {
//     public class DictionaryContextFactory : IDesignTimeDbContextFactory<DictionaryContext>
//     {
//         public DictionaryContext CreateDbContext(string[] args)
//         {
//             var optionsBuilder = new DbContextOptionsBuilder<DictionaryContext>();
//             optionsBuilder.UseSqlite("Data Source=DictionaryDatabase.db");

//             return new DictionaryContext(optionsBuilder.Options);
//         }
//     }
// }