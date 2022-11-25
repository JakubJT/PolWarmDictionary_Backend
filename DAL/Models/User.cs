namespace DAL.Models;
public class User
{
    public int UserId { get; set; }
    public string? Name { get; set; }
    public string? UserIdentifier { get; set; }

    public List<WordGroup>? WordGroups { get; set; }
}
