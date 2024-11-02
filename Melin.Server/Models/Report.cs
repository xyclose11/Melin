using System.ComponentModel.DataAnnotations;

namespace Melin.Server.Models;

// Covers Reports & Theses
public class Report : Reference
{
    [MaxLength(256)]
    public string ReportType { get; set; } = "";
    public int ReportNumber { get; set; } = 0;
    [MaxLength(256)]
    public string Institution { get; set; } = "";
}