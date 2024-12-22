namespace Melin.Server.Models.DTO;

public class GroupDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public bool IsRoot { get; set; }
    public List<GroupDto>? ChildGroups { get; set; }
    public List<Reference>? References { get; set; }


}