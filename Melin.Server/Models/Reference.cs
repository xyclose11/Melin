namespace Melin.Server.Models;

public class Reference
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string ShortTitle { get; set; } = "";
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}