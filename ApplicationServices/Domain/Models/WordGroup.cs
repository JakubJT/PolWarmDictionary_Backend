namespace ApplicationServices.Domain.Models;
public class WordGroup
{
    public int WordGroupId { get; set; }
    public string? Name { get; set; }
    public string? UserIdentifier { get; set; }
    public User? User { get; set; }
    public List<Word>? Words { get; set; }
}
