using System.ComponentModel.DataAnnotations;

namespace Melin.Server.Models;

public interface ICreator
{
   public Author Author { get; set; }
}

public class Author
{
    [MaxLength(512)]
    public string FirstName { get; set; } = "";
    
    [MaxLength(512)]
    public string LastName { get; set; } = "";
}