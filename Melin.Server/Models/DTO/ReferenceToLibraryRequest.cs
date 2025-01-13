namespace Melin.Server.Models.DTO;

public class ReferenceToLibraryRequest
{
    public int Id;
    public required string Type;
    public required string Title;
    public string? DatePublished;
    public string? LocationStored;
    public required string UpdatedAt;
    public required string CreatedAt;
    public List<Creator>? Creators;
    public List<TagDisplayRequest>? Tags;
    public string? Language;
    public List<string>? GroupNames;
}