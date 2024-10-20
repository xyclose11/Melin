using System.ComponentModel.DataAnnotations;

namespace Melin.Server.Models;

public class Software : Reference
{
    [MaxLength(256)]
    public string Version { get; set; } = "";
    [MaxLength(256)]
    public string System { get; set; } = ""; // target OS or platform
    [MaxLength(256)]
    public string? Company { get; set; } = "N/A";
    [MaxLength(256)]
    public string? ProgrammingLanguage { get; set; } = "Unknown";
}