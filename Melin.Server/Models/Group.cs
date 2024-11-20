using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Melin.Server.Models;

public class Group {
    [BindNever]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(128)]
    public required string Name {get; set;}


    [MaxLength(256)]
    public string? Description {get; set;}

    public List<Reference>? References { get; } = [];
    // TODO add field to track usage

    public List<Group>? Groups { get; } = [];
    
    public bool IsRoot { get; set; } = true;

    [MaxLength(256)]
    [BindNever]
    public string CreatedBy {get; set;}

    public DateTime CreatedAt {get; init;} = DateTime.UtcNow;
    public DateTime UpdatedAt {get; set;} = DateTime.UtcNow;
}