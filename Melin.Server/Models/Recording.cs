namespace Melin.Server.Models;

public class Recording : Reference
{
    public string FileFormat { get; set; } = ""; // DVD, CD, MP3
    public string RunningTime { get; set; } = ""; // 120 mins
    public string ProgramTitle { get; set; } = "";
    public int? EpisodeNumber { get; set; } = 0;
    public string Network { get; set; } = "";
    public string Label { get; set; } = "";
    public string Distributor { get; set; } = "";
    public string Genre { get; set; } = "";
    public string Studio { get; set; } = "";
}