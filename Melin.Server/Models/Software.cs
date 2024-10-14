namespace Melin.Server.Models;

public class Software : Reference
{
    public string Version { get; set; } = "";
    public string System { get; set; } = ""; // target OS or platform
    public string? Company { get; set; } = "N/A";
    public string? ProgrammingLanguage { get; set; } = "Unknown";
}