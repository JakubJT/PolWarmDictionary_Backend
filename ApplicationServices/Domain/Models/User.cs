namespace ApplicationServices.Domain.Models;
public class User
{
    public int UserId { get; set; }
    public string? Name { get; set; }

    public List<WordGroup>? WordGroups { get; set; }
}
