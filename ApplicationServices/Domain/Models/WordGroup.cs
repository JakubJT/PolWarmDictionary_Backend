namespace ApplicationServices.Domain.Models;
public class WordGroup
{
    public int WordGroupId { get; set; }
    public string? Name { get; set; }

    public List<Word>? Words { get; set; }
    public List<User>? Users { get; set; }
}
