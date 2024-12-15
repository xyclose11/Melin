namespace Melin.Server.Models.DTO;

public class AddGroupToGroup
{
    public required string Parent { get; set; }
    public string? Child { get; set; }
}