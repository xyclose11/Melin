using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Melin.Server.Models;

// Base class for all references
public class Reference
{
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public string? OwnerEmail { get; set; }
    
    [MaxLength(256)]
    // Base fields
    public ReferenceType? Type { get; set; }
    public ICollection<Creator>? Creators { get; set; } = new List<Creator>();
    public string Title { get; set; } = "";
    public string? ShortTitle { get; set; } = "";
    public Language? Language { get; set; }
    public DateTime? DatePublished { get; set; }
    
    // Extra fields
    public string[]? Rights { get; set; } // Copyright terms, license, or release
    public string[]? ExtraFields { get; set; } // TODO Add feature to let user add field
    
    // Logging things
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}