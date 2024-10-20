using System.ComponentModel.DataAnnotations;

namespace Melin.Server.Models;

public class Website : Reference
{
    [MaxLength(256)]
    public string WebsiteTitle { get; set; } = "";
    [MaxLength(256)]
    public string? ForumTitle { get; set; } = "";
    [MaxLength(256)]
    public string? WebsiteType { get; set; } = "";
    [MaxLength(256)]
    public string? PostType { get; set; } = "";
}