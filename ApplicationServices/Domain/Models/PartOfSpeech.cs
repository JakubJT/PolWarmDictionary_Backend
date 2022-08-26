namespace ApplicationServices.Domain.Models;
public class PartOfSpeech
{
    public int PartOfSpeechId { get; set; }
    public string? Name { get; set; }

    public List<Word>? Words { get; set; }
}
