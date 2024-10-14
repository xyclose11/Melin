namespace Melin.Server.Models;

// Base class for all references
public class Reference
{
    // Base fields
    public int Id { get; set; }
    public ReferenceType Type { get; set; }
    public Creator[]? Creators { get; set; } = null;
    public string Title { get; set; } = "";
    public string? ShortTitle { get; set; } = "";
    public Language? Language { get; set; }
    public DateTime? DatePublished { get; set; }
    
    // Extra fields
    public string[]? Rights { get; set; } // Copyright terms, license, or release
    public string[]? ExtraFields { get; set; } // TODO Add feature to let user add field
    
    // Logging things
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}