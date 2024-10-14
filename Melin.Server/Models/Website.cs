namespace Melin.Server.Models;

public class Website : Reference
{
    public string WebsiteTitle { get; set; } = "";
    public string? ForumTitle { get; set; } = "";
    public string? WebsiteType { get; set; } = "";
    public string? PostType { get; set; } = "";
}