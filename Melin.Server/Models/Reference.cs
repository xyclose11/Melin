using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Melin.Server.Models.Binders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Melin.Server.Models;

// Base class for all references
public class Reference
{
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [EmailAddress]
    [MaxLength(256)]
    public string? OwnerEmail { get; set; }
    
    [MaxLength(256)]
    // Base fields
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ReferenceType? Type { get; set; }
    [ModelBinder(BinderType = typeof(CreatorEntityBinder))]
    public ICollection<Creator>? Creators { get; set; } = new List<Creator>();
    
    [MaxLength(512)]
    public string Title { get; set; } = "";
    
    [MaxLength(512)]
    public string? ShortTitle { get; set; } = "";
    [MaxLength(1024)]

    public string? AbstractNote { get; set; } = "";
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    [MaxLength(256)]
    public Language? Language { get; set; }
    
    // MaxLength is 128 since a DateTime cannot be longer than UTC time
    [MaxLength(128)]
    public DateTime? DatePublished { get; set; }
    
    // Extra fields
    [MaxLength(64)]
    public string[]? Rights { get; set; } // Copyright terms, license, or release
    
    [MaxLength(64)]
    public string[]? ExtraFields { get; set; } // TODO Add feature to let user add field
    
    // Tagging and Grouping
    public ICollection<Tag>? Tags { get; set; } = new List<Tag>();
    public ICollection<Group>? Groups { get; set; } = new List<Group>();
    
    // When and How items were accessed
    [MaxLength(256)]
    public string? Accessed { get; set; } = "";
    
    [MaxLength(256)]
    public string? LocationStored { get; set; } = "";
    
    [MaxLength(256)]
    public string? Archive { get; set; }
    
    [MaxLength(256)]
    public string? ArchiveLocation { get; set; }
    
    [MaxLength(256)]
    public string? LibraryCatalog { get; set; }
    
    [MaxLength(256)]
    public string? CallNumber { get; set; }
    
    [Url]
    [MaxLength(2048)]
    public string? URL { get; set; }
    
    // Logging things
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}