namespace Melin.Server.Models.DTO;

public class CreateTextFileRequest
{
    public required string FileName { get; set; }
    public string? Content { get; set; }
}