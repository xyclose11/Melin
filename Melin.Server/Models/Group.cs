using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Melin.Server.Models;

public class Group {
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(128)]
    public required string Name {get; set;}


    [MaxLength(256)]
    public string? Description {get; set;}

    public List<Reference> References { get; } = [];
    // TODO add field to track usage

    [Required]
    [MaxLength(256)]
    public required string CreatedBy {get; set;}

    public DateTime CreatedAt {get; init;} = DateTime.UtcNow;
    public DateTime UpdatedAt {get; set;} = DateTime.UtcNow;
}