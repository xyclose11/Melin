namespace Melin.Server.Models.DTO;

public class AddTagsRequest
{
    public int RefId { get; set; }
    public required List<Tag> Tags { get; set; }
}