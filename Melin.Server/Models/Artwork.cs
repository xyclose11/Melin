using System.ComponentModel.DataAnnotations;

namespace Melin.Server.Models;

// Covers Image, Artwork, and Maps
public class Artwork : Reference
{
    [MaxLength(128)]
    public string Medium { get; set; } = "";
    [MaxLength(128)]

    public string Dimensions { get; set; } = "";
    [MaxLength(128)]

    public string? Scale { get; set; } = "";
    [MaxLength(128)]

    public string? MapType { get; set; } = "";
}