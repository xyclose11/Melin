using System.ComponentModel.DataAnnotations;

namespace Melin.Server.Models.DTO;

public class TeamCreateDto
{
    [Required]
    [MaxLength(256)]
    public required string Name { get; set; }
    
    [MaxLength(1024)]
    public string? Description { get; set; }

    public ICollection<Member> Members { get; set; } = new List<Member>();
}