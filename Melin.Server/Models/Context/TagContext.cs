using Microsoft.EntityFrameworkCore;

namespace Melin.Server.Models.Context;

public class TagContext : DbContext
{
    public TagContext(DbContextOptions<TagContext> options)
        : base(options)
    {}

    public DbSet<Tag> Tags => Set<Tag>();
}