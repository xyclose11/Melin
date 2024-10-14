namespace Melin.Server.Models;

// Covers Books & Periodicals
public class Book : Reference
{
    public string Publication { get; set; } = "";
    public string BookTitle { get; set; } = "";
    public string Volume { get; set; } = "";
    public string Issue { get; set; } = "";
    public int Pages { get; set; } = 0;
    public string Edition { get; set; } = "";
    public string Series { get; set; } = "";
    public int SeriesNumber { get; set; } = 0;
    public string SeriesTitle { get; set; } = "";
    public int VolumeAmount { get; set; } = 0;
    public int PageAmount { get; set; } = 0;
    public string Section { get; set; } = "";
    public string Place { get; set; } = "";
    public string Publisher { get; set; } = "";
    public string JournalAbbr { get; set; } = "";
    public string ISBN { get; set; } = "";
    public string ISSN { get; set; } = "";
}