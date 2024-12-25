using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Melin.Server.Models.User;

public class ApplicationUser : IdentityUser
{
    /// <summary>
    /// Gets or Sets the Teams List
    /// </summary>
    [Range(0, 16)]
    public List<Team>? Teams { get; set; }
    
    public bool IsActive { get; set; }
    
    public DateTime? LastLoginDate { get; set; }
    
    public string? SignalRConnectionId { get; set; }
    
    public string? DisplayName { get; set; }
}