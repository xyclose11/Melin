using Microsoft.EntityFrameworkCore;

namespace Melin.Server.Models;

public class Database : DbContext
{
    public Database(DbContextOptions<Database> options)
        : base(options)
    {}

    public DbSet<Reference> Reference => Set<Reference>();
}

