namespace Melin.Server.Models.DTO;

public class CreateTextFileRequest
{
    public required string FileName { get; set; }
    public required string Content { get; set; }
}