using System.ComponentModel.DataAnnotations;

namespace Melin.Server.Models;

public class Reference
{
    [Required]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(256)]
    public string Title { get; set; } = "";
    public string ShortTitle { get; set; } = "";
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}