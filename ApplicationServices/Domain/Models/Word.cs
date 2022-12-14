using System.ComponentModel.DataAnnotations;

namespace ApplicationServices.Domain.Models;
public class Word
{
    public int Id { get; set; }
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
    public string? PartOfSpeech { get; set; }
    public int PartOfSpeechId { get; set; }

}
