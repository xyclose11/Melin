namespace Melin.Server.Models;

// Covers Legislation & Hearings
public class Legislation : Reference
{
    public string NameOfAct { get; set; } = "";
    public string BillNumber { get; set; } = "";
    public string Code { get; set; } = "";
    public string CodeVolume { get; set; } = "";
    public string CodeNumber { get; set; } = "";
    public string PublicLawNumber { get; set; } = "";
    public DateTime? DateEnacted { get; set; }
    public string? Section { get; set; } = "";
    public string? Committee { get; set; } = "";
    public string? DocumentNumber { get; set; } = "";
    public string? CodePages { get; set; } = "";
    public string? LegislativeBody { get; set; } = "";
    public string? Session { get; set; } = "";
    public string? History { get; set; } = "";
}

public class LegalCases : Reference
{
    public string? History { get; set; } = "";
    public string? CaseName { get; set; } = "";
    public string? Court { get; set; } = "";
    public DateTime? DateDecided { get; set; }
    public string? DocketNumber { get; set; } = "";
    public string? Reporter { get; set; } = "";
    public string? ReporterVolume { get; set; } = "";
    public string? FirstPage { get; set; } = "";
}

public class Patent : Reference
{
    public string Country { get; set; } = "United States of America";
    public string? Assignee { get; set; } = "";
    public string? IssuingAuthority { get; set; } = "";
    public string? PatentNumber { get; set; } = "";
    public DateTime? FilingDate { get; set; }
    public DateTime? IssueDate { get; set; }
    public string? ApplicationNumber { get; set; } = "";
    public string? PriorityNumber { get; set; } = "";
    public string[]? References { get; set; }
    public string? LegalStatus { get; set; } = "";

}