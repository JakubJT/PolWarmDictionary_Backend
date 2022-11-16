using Microsoft.EntityFrameworkCore;
using System.Linq;
using DAL.Models;

namespace DAL;

public class PartOfSpeechRepository : Repository<PartOfSpeech>
{
    public PartOfSpeechRepository(DictionaryContext context) : base(context)
    {
    }
    public async Task<PartOfSpeech> GetItemByName(string name)
    {
        return await Items.FirstOrDefaultAsync(i => i.Name == name);
    }
}