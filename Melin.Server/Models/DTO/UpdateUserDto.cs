using System.ComponentModel.DataAnnotations;

namespace Melin.Server.Models.DTO;

public class UpdateUserDto
{
    [MaxLength(128)]
    public string? FirstName;
    
    [MaxLength(512)]
    public string? LastName;
    
    public UpdateUserEmail? Email;
    public UpdateUserPassword? Password;

    public required string CurrentPassword;
}


public class UpdateUserEmail
{
    [MaxLength(128)]
    [EmailAddress]
    public required string Email;
    [MaxLength(128)]
    [EmailAddress]
    public required string ConfirmEmail;
}

public class UpdateUserPassword
{
    [MaxLength(128)]
    public required string Password;
    [MaxLength(128)]
    public required string ConfirmPassword;
}