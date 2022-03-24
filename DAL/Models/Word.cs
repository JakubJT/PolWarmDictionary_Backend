namespace DAL.Models
{
    public class Word
    {
        public int WordId { get; set; }
        public string? ContentWar { get; set; }
        public string? ContentPol { get; set; }

        public int AuthorId { get; set; }
        public Author? Author { get; set; }
        public int? PartOfSpeechId { get; set; }
        public PartOfSpeech? PartOfSpeech { get; set; }
        public List<WordGroup>? WordGroups { get; set; }
    }
}