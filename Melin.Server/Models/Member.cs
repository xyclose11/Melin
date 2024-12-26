using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Melin.Server.Models;

public class Member
{
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public required int Id { get; set; }
    
    [Required]
    [MaxLength(512)]
    [EmailAddress]
    public required string EmailAddress { get; set; }
    
    [Required]
    [MaxLength(256)]
    public required string UserName { get; set; }
    
}