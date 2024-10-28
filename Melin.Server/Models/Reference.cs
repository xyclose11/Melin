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
    
    public string? OwnerEmail { get; set; }
    
    [MaxLength(256)]
    // Base fields
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ReferenceType? Type { get; set; }
    [ModelBinder(BinderType = typeof(CreatorEntityBinder))]
    public ICollection<Creator>? Creators { get; set; } = new List<Creator>();
    public string Title { get; set; } = "";
    public string? ShortTitle { get; set; } = "";
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Language? Language { get; set; }
    public DateTime? DatePublished { get; set; }
    
    // Extra fields
    public string[]? Rights { get; set; } // Copyright terms, license, or release
    public string[]? ExtraFields { get; set; } // TODO Add feature to let user add field
    
    // Tagging and Grouping
    public ICollection<Tag>? Tags { get; set; } = new List<Tag>();
    public ICollection<Group>? Groups { get; set; } = new List<Group>();
    
    // Logging things
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}