using System.ComponentModel.DataAnnotations;

namespace Melin.Server.Models;

// Covers presentations & Performances
public class Presentation : Reference
{
    [MaxLength(256)]
    public string ProceedingTitle { get; set; } = "";
    [MaxLength(256)]
    public string ConferenceName { get; set; } = ""; // Conference name or meeting name
    [MaxLength(256)]
    public string Place { get; set; } = "";
    [MaxLength(256)]
    public string PresentationType { get; set; } = "";
}