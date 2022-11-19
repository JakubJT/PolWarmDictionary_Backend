using System.ComponentModel.DataAnnotations;

namespace DAL.Models;
public class Word : ModelBase
{
    public int WordId { get; set; }

    [Required]
    [DataType(DataType.Text)]
    [StringLength(16, MinimumLength = 2)]
    [RegularExpression(@"^[A-ZĄĘÓŃŻŹĆŁŚ]+[a-ząężźćóńłśA-ZĄĘÓŃŻŹĆŁŚ\s]*$")]
    public string? InWarmian { get; set; }

    [Required]
    [DataType(DataType.Text)]
    [StringLength(16, MinimumLength = 2)]
    [RegularExpression(@"^[A-ZĄĘÓŃŻŹĆŁŚ]+[a-ząężźćóńłśA-ZĄĘÓŃŻŹĆŁŚ\s]*$")]
    public string? InPolish { get; set; }

    public int AuthorId { get; set; }
    public Author? Author { get; set; }
    public int? PartOfSpeechId { get; set; }
    public PartOfSpeech? PartOfSpeech { get; set; }
    public List<WordGroup>? WordGroups { get; set; }
}