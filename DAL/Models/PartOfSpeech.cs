namespace DAL.Models;
public class PartOfSpeech : ModelBase
{
    public int PartOfSpeechId { get; set; }
    public string? Name { get; set; }

    public List<Word>? Words { get; set; }
}
