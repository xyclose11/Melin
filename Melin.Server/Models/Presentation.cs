namespace Melin.Server.Models;

// Covers presentations & Performances
public class Presentation : Reference
{
    public string ProceedingTitle { get; set; } = "";
    public string ConferenceName { get; set; } = ""; // Conference name or meeting name
    public string Place { get; set; } = "";
    public string PresentationType { get; set; } = "";
}