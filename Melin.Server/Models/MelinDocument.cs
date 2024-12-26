using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Melin.Server.Models;

public class MelinDocument
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required] public string Title { get; set; } = "";

    public string Content { get; set; } = "";
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime LastModified { get; set; } = DateTime.UtcNow;

    [Required] public string OwnerEmail { get; set; } = "";

    [Required] public string LastModifiedByEmail { get; set; } = "";

}