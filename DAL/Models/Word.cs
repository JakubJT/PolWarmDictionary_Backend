namespace DAL.Models;
public class Word : ModelBase
{
    public int WordId { get; set; }
    public string? InWarmian { get; set; }
    public string? InPolish { get; set; }

    public int AuthorId { get; set; }
    public Author? Author { get; set; }
    public int? PartOfSpeechId { get; set; }
    public PartOfSpeech? PartOfSpeech { get; set; }
    public List<WordGroup>? WordGroups { get; set; }
}