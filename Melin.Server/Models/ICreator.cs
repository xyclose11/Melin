using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Melin.Server.Models;

public enum CreatorTypes {
    Author,
    Editor,
    Series_Editor,
    Translator,
    Reviewed_Author,
    Artist,
    Performer,
    Composer,
    Director,
    Podcaster,
    Cartographer,
    Programmer,
    Presenter,
    Interview_With,
    Interviewer,
    Recipient,
    Sponsor,
    Inventor
}

public class Creator
{
    public int Id { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public CreatorTypes Types { get; set; } = CreatorTypes.Author;

    [MaxLength(512)]
    public string FirstName { get; set; } = "";
    
    [MaxLength(512)]
    public string LastName { get; set; } = "";
    
    public int ReferenceId { get; set; }
    public Reference Reference { get; set; }
}

public class Author
{
    [MaxLength(512)]
    public string FirstName { get; set; } = "";
    
    [MaxLength(512)]
    public string LastName { get; set; } = "";
}