namespace Melin.Server.Models;

// Covers Image, Artwork, and Maps
public class Artwork : Reference
{
    public string Medium { get; set; } = "";
    public string Dimensions { get; set; } = "";
    public string? Scale { get; set; } = "";
    public string? MapType { get; set; } = "";
}