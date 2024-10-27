using Microsoft.EntityFrameworkCore;

namespace Melin.Server.Models;

public class ReferenceContext : DbContext
{
    public ReferenceContext(DbContextOptions<ReferenceContext> options)
        : base(options)
    {}

    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<Group> Group => Set<Group>();
    public DbSet<Reference> Reference => Set<Reference>();
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Artwork> Artworks => Set<Artwork>();
    public DbSet<LegalCases> LegalCases => Set<LegalCases>();
    public DbSet<Patent> Patents => Set<Patent>();
    public DbSet<Legislation> Legislations => Set<Legislation>();
    public DbSet<Website> Websites => Set<Website>();
    public DbSet<Report> Reports => Set<Report>();
    public DbSet<Presentation> Presentations => Set<Presentation>();
    public DbSet<PrimarySource> PrimarySources => Set<PrimarySource>();
    public DbSet<Recording> Recordings => Set<Recording>();
    public DbSet<Software> Softwares => Set<Software>();
}

