using System.ComponentModel.DataAnnotations;

namespace Melin.Server.Models;

public class Creator
{
    public int Id { get; set; }
    [MaxLength(512)]
    public string FirstName { get; set; } = "";
    
    [MaxLength(512)]
    public string LastName { get; set; } = "";
}

public class Author
{
    [MaxLength(512)]
    public string FirstName { get; set; } = "";
    
    [MaxLength(512)]
    public string LastName { get; set; } = "";
}