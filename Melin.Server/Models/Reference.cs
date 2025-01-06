using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Melin.Server.Models.Binders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Melin.Server.Models;

// Base class for all references
[JsonConverter(typeof(ReferenceConverter))]
public class Reference
{
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public string? OwnerEmail { get; set; }
    
    // Base fields
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ReferenceType Type { get; set; }
    [ModelBinder(BinderType = typeof(CreatorEntityBinder))]
    [JsonIgnore]
    public ICollection<Creator>? Creators { get; set; } = new List<Creator>();
    public string Title { get; set; } = "";
    public string? ShortTitle { get; set; } = "";

    public string? AbstractNote { get; set; } = "";
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Language? Language { get; set; }
    public string? DatePublished { get; set; }
    
    // Extra fields
    public string[]? Rights { get; set; } // Copyright terms, license, or release
    public string[]? ExtraFields { get; set; } // TODO Add feature to let user add field
    
    // Tagging and Grouping
    public ICollection<Tag>? Tags { get; set; } = new List<Tag>();
    public ICollection<Group>? Groups { get; set; } = new List<Group>();
    
    // When and How items were accessed
    public string? Accessed { get; set; } = "";
    public string? LocationStored { get; set; } = "";
    
    public string? Archive { get; set; }
    public string? ArchiveLocation { get; set; }
    public string? LibraryCatalog { get; set; }
    public string? CallNumber { get; set; }
    public string? URL { get; set; }
    // Logging things
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}