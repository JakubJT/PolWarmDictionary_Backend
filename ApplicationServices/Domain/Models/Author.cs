namespace ApplicationServices.Domain.Models;
public class Author
{
    public int AuthorId { get; set; }
    public string? Name { get; set; }

    public List<Word>? Words { get; } = new();
}
