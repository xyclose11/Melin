namespace Melin.Server.Models;

// Covers Primary Sources & Personal Communications
public class PrimarySource : Reference
{
    public string Medium { get; set; } = "";
    public string PrimarySourceType { get; set; } = "";
    public string Subject { get; set; } = "";
}