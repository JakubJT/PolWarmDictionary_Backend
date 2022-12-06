using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models;
public class WordGroup
{
    public int WordGroupId { get; set; }
    public string? Name { get; set; }
    public string? UserADId { get; set; }
    [ForeignKey("UserADId")]
    public User? User { get; set; }
    public List<Word>? Words { get; set; }
}