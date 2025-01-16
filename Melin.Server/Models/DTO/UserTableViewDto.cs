using System.Security.Claims;

namespace Melin.Server.Models.DTO;

public class UserTableViewDto
{
    public required string Id;
    public string? FullName;
    public required string Email;
    public DateTime? LastLoginDate;
    public required int AccessFailedCount;
    public required bool PhoneNumberConfirmed;
    public required bool EmailConfirmed;
    public required bool LockoutEnabled;
    public required string UserName;
    public int? ReferenceCount;
    public Claim? Roles;
    public string? Status;
}