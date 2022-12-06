namespace ApplicationServices.Domain.Models;
public class User
{
    public int UserId { get; set; }
    public string? UserADId { get; set; }

    public List<WordGroup>? WordGroups { get; set; }
}
