using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Castle.Components.DictionaryAdapter;
using Melin.Server.Models.User;

namespace Melin.Server.Models;

public class Team
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required] [MaxLength(512)] public required string Name { get; set; } = null!;
    
    [MaxLength(1024)]
    public string? Description { get; set; }

    public ICollection<Member> Members { get; set; } = new List<Member>();

    [Required]
    [MaxLength(256)]
    public required string OwnerId { get; set; } = null!;
}