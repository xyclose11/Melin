namespace Melin.Server.Models;

// Covers Reports & Theses
public class Report : Reference
{
    public string ReportType { get; set; } = "";
    public int ReportNumber { get; set; } = 0;
    public string Institution { get; set; } = "";
}