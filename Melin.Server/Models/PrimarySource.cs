using System.ComponentModel.DataAnnotations;

namespace Melin.Server.Models;

// Covers Primary Sources & Personal Communications
public class PrimarySource : Reference
{
    [MaxLength(256)]
    public string Medium { get; set; } = "";
    [MaxLength(256)]
    public string PrimarySourceType { get; set; } = "";
    [MaxLength(256)]
    public string Subject { get; set; } = "";
}