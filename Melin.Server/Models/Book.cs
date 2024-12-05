using System.ComponentModel.DataAnnotations;

namespace Melin.Server.Models;

// Covers Books & Periodicals
public class Book : Reference
{
    [MaxLength(256)]
    public string? Publication { get; set; } = "";
    [MaxLength(256)]
    public string? BookTitle { get; set; } = "";
    [MaxLength(256)]
    public string? Volume { get; set; } = "";
    [MaxLength(256)]
    public string? Issue { get; set; } = "";
    public int? Pages { get; set; } = 0;
    [MaxLength(256)]

    public string? Edition { get; set; } = "";
    [MaxLength(256)]

    public string? Series { get; set; } = "";
    public int? SeriesNumber { get; set; } = 0;
    [MaxLength(256)]

    public string? SeriesTitle { get; set; } = "";
    public int? VolumeAmount { get; set; } = 0;
    public int? PageAmount { get; set; } = 0;
    [MaxLength(256)]

    public string? Section { get; set; } = "";
    [MaxLength(256)]

    public string? Place { get; set; } = "";
    [MaxLength(256)]

    public string? Publisher { get; set; } = "";
    [MaxLength(256)]
    public string? JournalAbbr { get; set; } = "";
    [MaxLength(256)]
    public string? ISBN { get; set; } = "";
    [MaxLength(256)]
    public string? ISSN { get; set; } = "";
}