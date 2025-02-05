using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Melin.Server.Models;

public class Tag
{
    public int Id { get; set; }

    [Required]
    [MaxLength(128)]
    public required string Text {get; set;}


    [MaxLength(256)]
    public string? Description {get; set;}

    public ICollection<Reference>? References { get; set; } = new List<Reference>();

    [MaxLength(256)]
    public string? CreatedBy {get; set;}
    // TODO add field to track how many uses the Tag has

    public DateTime CreatedAt {get; init;} = DateTime.UtcNow;
    public DateTime UpdatedAt {get; set;} = DateTime.UtcNow;

}