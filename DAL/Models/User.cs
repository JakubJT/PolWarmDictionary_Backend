namespace DAL.Models;
public class User
{
    public int UserId { get; set; }
    public string? UserADId { get; set; }
    public bool IsAdmin { get; set; } = false;

    public List<WordGroup>? WordGroups { get; set; }
}
