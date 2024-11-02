using System.ComponentModel.DataAnnotations;

namespace Melin.Server.Models;

// Covers Legislation & Hearings
public class Legislation : Reference
{
    [MaxLength(256)]
    public string NameOfAct { get; set; } = "";
    [MaxLength(256)]
    public string BillNumber { get; set; } = "";
    [MaxLength(256)]
    public string Code { get; set; } = "";
    [MaxLength(256)]
    public string CodeVolume { get; set; } = "";
    [MaxLength(256)]
    public string CodeNumber { get; set; } = "";
    [MaxLength(256)]
    public string PublicLawNumber { get; set; } = "";
    public DateTime? DateEnacted { get; set; }
    [MaxLength(256)]
    public string? Section { get; set; } = "";
    [MaxLength(256)]
    public string? Committee { get; set; } = "";
    [MaxLength(256)]
    public string? DocumentNumber { get; set; } = "";
    [MaxLength(256)]
    public string? CodePages { get; set; } = "";
    [MaxLength(256)]
    public string? LegislativeBody { get; set; } = "";
    [MaxLength(256)]
    public string? Session { get; set; } = "";
    [MaxLength(256)]
    public string? History { get; set; } = "";
}

public class LegalCases : Reference
{
    [MaxLength(256)]
    public string? History { get; set; } = "";
    [MaxLength(256)]
    public string? CaseName { get; set; } = "";
    [MaxLength(256)]
    public string? Court { get; set; } = "";
    public DateTime? DateDecided { get; set; }
    [MaxLength(256)]
    public string? DocketNumber { get; set; } = "";
    [MaxLength(256)]
    public string? Reporter { get; set; } = "";
    [MaxLength(256)]
    public string? ReporterVolume { get; set; } = "";
    [MaxLength(256)]
    public string? FirstPage { get; set; } = "";
}

public class Patent : Reference
{
    [MaxLength(256)]
    public string Country { get; set; } = "United States of America";
    [MaxLength(256)]
    public string? Assignee { get; set; } = "";
    [MaxLength(256)]
    public string? IssuingAuthority { get; set; } = "";
    [MaxLength(256)]
    public string? PatentNumber { get; set; } = "";
    public DateTime? FilingDate { get; set; }
    public DateTime? IssueDate { get; set; }
    [MaxLength(256)]
    public string? ApplicationNumber { get; set; } = "";
    [MaxLength(256)]
    public string? PriorityNumber { get; set; } = "";
    public string[]? References { get; set; }
    [MaxLength(256)]
    public string? LegalStatus { get; set; } = "";

}