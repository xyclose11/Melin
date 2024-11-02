using System.ComponentModel.DataAnnotations;

namespace Melin.Server.Models;

public class Recording : Reference
{
    [MaxLength(256)]
    public string FileFormat { get; set; } = ""; // DVD, CD, MP3
    [MaxLength(256)]
    public string RunningTime { get; set; } = ""; // 120 mins
    [MaxLength(256)]
    public string ProgramTitle { get; set; } = "";
    public int? EpisodeNumber { get; set; } = 0;
    [MaxLength(256)]
    public string Network { get; set; } = "";
    [MaxLength(256)]
    public string Label { get; set; } = "";
    [MaxLength(256)]
    public string Distributor { get; set; } = "";
    [MaxLength(256)]
    public string Genre { get; set; } = "";
    [MaxLength(256)]
    public string Studio { get; set; } = "";
}