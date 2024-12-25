using Melin.Server.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Melin.Server.Data;

public class DataContext : IdentityDbContext<ApplicationUser>
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }
}