using System.ComponentModel.DataAnnotations;
using Castle.Components.DictionaryAdapter;
using Microsoft.AspNetCore.Identity;

namespace Melin.Server.Models.User;

public class ApplicationUser : IdentityUser
{
    /// <summary>
    /// Gets or Sets the Teams List
    /// </summary>
    public ICollection<Team>? Teams { get; set; } = new List<Team>();
    
    public bool IsActive { get; set; }
    
    public DateTime? LastLoginDate { get; set; }
    
    [MaxLength(1024)]
    public string? SignalRConnectionId { get; set; }
    
    [MaxLength(64)]
    public string? DisplayName { get; set; }
}