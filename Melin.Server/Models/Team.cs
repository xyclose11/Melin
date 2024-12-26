using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Castle.Components.DictionaryAdapter;
using Melin.Server.Models.User;

namespace Melin.Server.Models;

public class Team
{
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [MaxLength(512)]
    public required string Name { get; set; }
    
    [MaxLength(1024)]
    public string? Description { get; set; }

    [Range(0, 100)]
    public ICollection<ApplicationUser> Members { get; set; } = new List<ApplicationUser>();

    [MaxLength(256)]
    public required string OwnerId { get; set; } = null!;
}