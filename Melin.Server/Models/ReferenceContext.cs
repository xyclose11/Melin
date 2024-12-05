using Melin.Server.Models.References;
using Microsoft.EntityFrameworkCore;

namespace Melin.Server.Models;

public class ReferenceContext : DbContext
{
    public ReferenceContext(DbContextOptions<ReferenceContext> options)
        : base(options)
    {}

    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<Group> Group => Set<Group>();
    public DbSet<Creator> Creators => Set<Creator>();
    public DbSet<Reference> Reference => Set<Reference>();
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Artwork> Artworks => Set<Artwork>();
    public DbSet<AudioRecording> AudioRecordings => Set<AudioRecording>();
    public DbSet<Bill> Bills => Set<Bill>();
    public DbSet<BlogPost> BlogPosts => Set<BlogPost>();
    public DbSet<BookSection> BookSections => Set<BookSection>();
    public DbSet<ConferencePaper> ConferencePapers => Set<ConferencePaper>();
    public DbSet<DictionaryEntry> DictionaryEntries => Set<DictionaryEntry>();
    public DbSet<Document> Documents => Set<Document>();
    public DbSet<Email> Emails => Set<Email>();
    public DbSet<EncyclopediaArticle> EncyclopediaArticles => Set<EncyclopediaArticle>();
    public DbSet<Film> Films => Set<Film>();
    public DbSet<ForumPost> ForumPosts => Set<ForumPost>();
    public DbSet<Hearing> Hearings => Set<Hearing>();
    public DbSet<InstantMessage> InstantMessages => Set<InstantMessage>();
    public DbSet<Interview> Interviews => Set<Interview>();
    public DbSet<JournalArticle> JournalArticles => Set<JournalArticle>();
    public DbSet<Letter> Letters => Set<Letter>();
    public DbSet<MagazineArticle> MagazineArticles => Set<MagazineArticle>();
    public DbSet<Manuscript> Manuscripts => Set<Manuscript>();
    public DbSet<Map> Maps => Set<Map>();
    public DbSet<NewspaperArticle> NewspaperArticles => Set<NewspaperArticle>();
    public DbSet<Podcast> Podcasts => Set<Podcast>();
    public DbSet<RadioBroadcast> RadioBroadcasts => Set<RadioBroadcast>();
    public DbSet<Statute> Statutes => Set<Statute>();
    public DbSet<Thesis> Theses => Set<Thesis>();
    public DbSet<TVBroadcast> TVBroadcasts => Set<TVBroadcast>();
    public DbSet<VideoRecording> VideoRecordings => Set<VideoRecording>();

    public DbSet<LegalCases> LegalCases => Set<LegalCases>();
    public DbSet<Patent> Patents => Set<Patent>();
    public DbSet<Legislation> Legislations => Set<Legislation>();
    public DbSet<Website> Websites => Set<Website>();
    public DbSet<Report> Reports => Set<Report>();
    public DbSet<Presentation> Presentations => Set<Presentation>();
    public DbSet<PrimarySource> PrimarySources => Set<PrimarySource>();
    public DbSet<Recording> Recordings => Set<Recording>();
    public DbSet<Software> Softwares => Set<Software>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<Reference>()
            // .HasMany(r => r.Creators)
            // .WithOne(c => c.Reference)
            // .HasForeignKey(c => c.ReferenceId)
            // .OnDelete(DeleteBehavior.Cascade);
    }}

