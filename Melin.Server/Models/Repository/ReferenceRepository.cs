using System.Linq.Expressions;
using System.Text.Json;
using Melin.Server.Filter;
using Melin.Server.Models.References;
using Melin.Server.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Melin.Server.Models.Repository;

public class ReferenceRepository : GenericRepository<Reference>, IReferenceRepository
{
    private readonly IMemoryCache _cache;
    public ReferenceRepository(ReferenceContext referenceContext, IMemoryCache cache) : base(referenceContext)
    {
        _cache = cache;
    }

    public async Task<Result<List<Reference>>> GetAllOwnedReferencesAsync(string userEmail)
    {
        try
        {
            var pagedReferences = await _context.Reference
                .Include(t => t.Tags)
                .Include(r => r.Creators)
                .Where(a => a.OwnerEmail == userEmail)
                .ToListAsync();

            if (pagedReferences.Count > 0)
            {
                return Result<List<Reference>>.SuccessResult(pagedReferences);
            }
            
            return Result<List<Reference>>.FailureResult("User currently has no references");
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<List<Reference>>> GetOwnedPaginatedReferencesAsync(PaginationFilter filter, string userEmail)
    {
        IQueryable<Reference> query = _context.Reference.AsQueryable();
        
        switch (filter.ReferenceType)
        {
            case ReferenceType.Artwork:
                query = query.OfType<Artwork>();
                break;
            case ReferenceType.Book:
                query = query.OfType<Book>();
                break;
            default:
                query = query.OfType<Reference>();
                break;
        }
        
        var validFilter = new PaginationFilter(
            filter.PageNumber > 0 ? filter.PageNumber : 1, 
            filter.PageSize > 0 ? filter.PageSize : 10
        );
        
        var pagedReferences = await query
            .Include(t => t.Tags)
            .Include(r => r.Creators)
            .Where(a => a.OwnerEmail == userEmail)
            .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
            .Take(validFilter.PageSize)
            .ToListAsync();


        return Result<List<Reference>>.SuccessResult(pagedReferences);
    }

    public async Task<Result<T>> GetReferenceByIdAsync<T>(string userEmail, int id) where T : Reference
    {
        // Check cache first
        if (!_cache.TryGetValue(id, out var cachedReference))
        {
            // Fetch from database
            var reference = await _context.Reference
                .Where(r => r.OwnerEmail == userEmail)
                .FirstOrDefaultAsync(r => r.Id == id);

            // If not found, return failure
            if (reference == null)
            {
                return Result<T>.FailureResult("Reference not found.");
            }

            // Cache the reference
            _cache.Set(id, reference, TimeSpan.FromMinutes(5));
            // Return the reference as T, but ensure that it's cast safely
            if (reference is T typedReference)
            {
                return Result<T>.SuccessResult(typedReference);
            }

            // If reference is not of the type T, return a failure result
            return Result<T>.FailureResult($"Reference found, but it's not of the expected type {typeof(T).Name}.");
        }

        // If found in cache, safely cast to T
        if (cachedReference is T cachedTypedReference)
        {
            return Result<T>.SuccessResult(cachedTypedReference);
        }

        // If cached reference is not of type T, return failure
        return Result<T>.FailureResult($"Cached reference is not of the expected type {typeof(T).Name}.");
    }

    public List<Reference> GetOwnedReferences(PaginationFilter filter, string userEmail)
    {
        var validFilter = new PaginationFilter(
            filter.PageNumber > 0 ? filter.PageNumber : 1, 
            filter.PageSize > 0 ? filter.PageSize : 10
        );
        
        var pagedReferences =  _context.Reference
            .Include(t => t.Tags)
            .Include(r => r.Creators)
            .Where(a => a.OwnerEmail == userEmail)
            .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
            .Take(validFilter.PageSize)
            .ToList();

        return pagedReferences;    
    }

    public async Task<Result<Reference>> GetReferenceByIdAsync(string userEmail, int id)
    {
        if (!_cache.TryGetValue(id, out Reference? reference))
        {
            reference = await _context.Reference
                .Where(r => r.OwnerEmail == userEmail)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reference != null)
            {
                _cache.Set(id, reference, TimeSpan.FromMinutes(5));
                return Result<Reference>.SuccessResult(reference);
            }
            
            return Result<Reference>.FailureResult("Reference not found.");
            
        }

        return Result<Reference>.SuccessResult(reference);
    }

    public async Task<Artwork> GetArtworkByIdAsync(string userEmail, int id)
    {
        if (!_cache.TryGetValue(id, out Artwork artwork))
        {
            artwork = await _context.Artworks
                .Where(r => r.OwnerEmail == userEmail)
                .FirstAsync(r => r.Id == id);

            if (artwork != null)
            {
                _cache.Set(id, artwork, TimeSpan.FromMinutes(5));
            }
        }

        return artwork;
    }

    public async Task<AudioRecording> GetAudioRecordingByIdAsync(string userEmail, int id)
    {
        if (!_cache.TryGetValue(id, out AudioRecording audioRecording))
        {
            audioRecording = await _context.AudioRecordings
                .Where(r => r.OwnerEmail == userEmail)
                .FirstAsync(r => r.Id == id);

            if (audioRecording != null)
            {
                _cache.Set(id, audioRecording, TimeSpan.FromMinutes(5));
            }
        }

        return audioRecording;
        
    }

    public async Task<Bill> GetBillByIdAsync(string userEmail, int id)
    {
        if (!_cache.TryGetValue(id, out Bill bill))
        {
            bill = await _context.Bills
                .Where(r => r.OwnerEmail == userEmail)
                .FirstAsync(r => r.Id == id);

            if (bill != null)
            {
                _cache.Set(id, bill, TimeSpan.FromMinutes(5));
            }
        }

        return bill;
    }

    public async Task<BlogPost> GetBlogPostByIdAsync(string userEmail, int id)
    {
        if (!_cache.TryGetValue(id, out BlogPost blogPost))
        {
            blogPost = await _context.BlogPosts
                .Where(r => r.OwnerEmail == userEmail)
                .FirstAsync(r => r.Id == id);

            if (blogPost != null)
            {
                _cache.Set(id, blogPost, TimeSpan.FromMinutes(5));
            }
        }

        return blogPost;
    }

    public async Task<Book> GetBookByIdAsync(string userEmail, int id)
    {
        if (!_cache.TryGetValue(id, out Book book))
        {
            book = await _context.Books
                .Where(r => r.OwnerEmail == userEmail)
                .FirstAsync(r => r.Id == id);

            if (book != null)
            {
                _cache.Set(id, book, TimeSpan.FromMinutes(5));
            }
        }

        return book;
    }

    public async Task<BookSection> GetBookSectionByIdAsync(string userEmail, int id)
    {
        if (!_cache.TryGetValue(id, out BookSection bookSection))
        {
            bookSection = await _context.BookSections
                .Where(r => r.OwnerEmail == userEmail)
                .FirstAsync(r => r.Id == id);

            if (bookSection != null)
            {
                _cache.Set(id, bookSection, TimeSpan.FromMinutes(5));
            }
        }

        return bookSection;
    }

    public async Task<LegalCases> GetCaseByIdAsync(string userEmail, int id)
    {
        if (!_cache.TryGetValue(id, out LegalCases cCase))
        {
            cCase = await _context.LegalCases
                .Where(r => r.OwnerEmail == userEmail)
                .FirstAsync(r => r.Id == id);

            if (cCase != null)
            {
                _cache.Set(id, cCase, TimeSpan.FromMinutes(5));
            }
        }

        return cCase;
    }

    public async Task<ConferencePaper> GetConferencePaperByIdAsync(string userEmail, int id)
    {
        if (!_cache.TryGetValue(id, out ConferencePaper conferencePaper))
        {
            conferencePaper = await _context.ConferencePapers
                .Where(r => r.OwnerEmail == userEmail)
                .FirstAsync(r => r.Id == id);

            if (conferencePaper != null)
            {
                _cache.Set(id, conferencePaper, TimeSpan.FromMinutes(5));
            }
        }

        return conferencePaper;
    }

    public async Task<DictionaryEntry> GetDictionaryEntryByIdAsync(string userEmail, int id)
    {
        if (!_cache.TryGetValue(id, out DictionaryEntry dictionaryEntry))
        {
            dictionaryEntry = await _context.DictionaryEntries
                .Where(r => r.OwnerEmail == userEmail)
                .FirstAsync(r => r.Id == id);

            if (dictionaryEntry != null)
            {
                _cache.Set(id, dictionaryEntry, TimeSpan.FromMinutes(5));
            }
        }

        return dictionaryEntry;
    }

    public async Task<Document> GetDocumentByIdAsync(string userEmail, int id)
    {
        if (!_cache.TryGetValue(id, out Document document))
        {
            document = await _context.Documents
                .Where(r => r.OwnerEmail == userEmail)
                .FirstAsync(r => r.Id == id);

            if (document != null)
            {
                _cache.Set(id, document, TimeSpan.FromMinutes(5));
            }
        }

        return document;
    }

    public async Task<Email> GetEmailByIdAsync(string userEmail, int id)
    {
        if (!_cache.TryGetValue(id, out Email email))
        {
            email = await _context.Emails
                .Where(r => r.OwnerEmail == userEmail)
                .FirstAsync(r => r.Id == id);

            if (email != null)
            {
                _cache.Set(id, email, TimeSpan.FromMinutes(5));
            }
        }

        return email;
    }

    public async Task<EncyclopediaArticle> GetEncyclopediaArticleByIdAsync(string userEmail, int id)
    {
        if (!_cache.TryGetValue(id, out EncyclopediaArticle encyclopediaArticle))
        {
            encyclopediaArticle = await _context.EncyclopediaArticles
                .Where(r => r.OwnerEmail == userEmail)
                .FirstAsync(r => r.Id == id);

            if (encyclopediaArticle != null)
            {
                _cache.Set(id, encyclopediaArticle, TimeSpan.FromMinutes(5));
            }
        }

        return encyclopediaArticle;
    }

    public async Task<Film> GetFilmByIdAsync(string userEmail, int id)
    {
        if (!_cache.TryGetValue(id, out Film film))
        {
            film = await _context.Films
                .Where(r => r.OwnerEmail == userEmail)
                .FirstAsync(r => r.Id == id);

            if (film != null)
            {
                _cache.Set(id, film, TimeSpan.FromMinutes(5));
            }
        }

        return film;
    }

    public async Task<ForumPost> GetForumPostByIdAsync(string userEmail, int id)
    {
        if (!_cache.TryGetValue(id, out ForumPost forumPost))
        {
            forumPost = await _context.ForumPosts
                .Where(r => r.OwnerEmail == userEmail)
                .FirstAsync(r => r.Id == id);

            if (forumPost != null)
            {
                _cache.Set(id, forumPost, TimeSpan.FromMinutes(5));
            }
        }

        return forumPost;
    }

    public async Task<Hearing> GetHearingByIdAsync(string userEmail, int id)
    {
        if (!_cache.TryGetValue(id, out Hearing hearing))
        {
            hearing = await _context.Hearings
                .Where(r => r.OwnerEmail == userEmail)
                .FirstAsync(r => r.Id == id);

            if (hearing != null)
            {
                _cache.Set(id, hearing, TimeSpan.FromMinutes(5));
            }
        }

        return hearing;
    }

    public async Task<InstantMessage> GetInstantMessageByIdAsync(string userEmail, int id)
    {
        if (!_cache.TryGetValue(id, out InstantMessage instantMessage))
        {
            instantMessage = await _context.InstantMessages
                .Where(r => r.OwnerEmail == userEmail)
                .FirstAsync(r => r.Id == id);

            if (instantMessage != null)
            {
                _cache.Set(id, instantMessage, TimeSpan.FromMinutes(5));
            }
        }

        return instantMessage;
    }

    public async Task<Interview> GetInterviewByIdAsync(string userEmail, int id)
    {
        if (!_cache.TryGetValue(id, out Interview interview))
        {
            interview = await _context.Interviews
                .Where(r => r.OwnerEmail == userEmail)
                .FirstAsync(r => r.Id == id);

            if (interview != null)
            {
                _cache.Set(id, interview, TimeSpan.FromMinutes(5));
            }
        }

        return interview;
    }

    public async Task<JournalArticle> GetJournalArticleByIdAsync(string userEmail, int id)
    {
        if (!_cache.TryGetValue(id, out JournalArticle journalArticle))
        {
            journalArticle = await _context.JournalArticles
                .Where(r => r.OwnerEmail == userEmail)
                .FirstAsync(r => r.Id == id);

            if (journalArticle != null)
            {
                _cache.Set(id, journalArticle, TimeSpan.FromMinutes(5));
            }
        }

        return journalArticle;
    }

    public async Task<Letter> GetLetterByIdAsync(string userEmail, int id)
    {
        if (!_cache.TryGetValue(id, out Letter letter))
        {
            letter = await _context.Letters
                .Where(r => r.OwnerEmail == userEmail)
                .FirstAsync(r => r.Id == id);

            if (letter != null)
            {
                _cache.Set(id, letter, TimeSpan.FromMinutes(5));
            }
        }

        return letter;
    }

    public async Task<MagazineArticle> GetMagazineArticleByIdAsync(string userEmail, int id)
    {
        if (!_cache.TryGetValue(id, out MagazineArticle magazineArticle))
        {
            magazineArticle = await _context.MagazineArticles
                .Where(r => r.OwnerEmail == userEmail)
                .FirstAsync(r => r.Id == id);

            if (magazineArticle != null)
            {
                _cache.Set(id, magazineArticle, TimeSpan.FromMinutes(5));
            }
        }

        return magazineArticle;
    }

    public async Task<Manuscript> GetManuscriptByIdAsync(string userEmail, int id)
    {
        if (!_cache.TryGetValue(id, out Manuscript manuscript))
        {
            manuscript = await _context.Manuscripts
                .Where(r => r.OwnerEmail == userEmail)
                .FirstAsync(r => r.Id == id);

            if (manuscript != null)
            {
                _cache.Set(id, manuscript, TimeSpan.FromMinutes(5));
            }
        }

        return manuscript;
    }

    public async Task<Map> GetMapByIdAsync(string userEmail, int id)
    {
        if (!_cache.TryGetValue(id, out Map map))
        {
            map = await _context.Maps
                .Where(r => r.OwnerEmail == userEmail)
                .FirstAsync(r => r.Id == id);

            if (map != null)
            {
                _cache.Set(id, map, TimeSpan.FromMinutes(5));
            }
        }

        return map;
    }

    public async Task<NewspaperArticle> GetNewspaperArticleByIdAsync(string userEmail, int id)
    {
        if (!_cache.TryGetValue(id, out NewspaperArticle newspaperArticle))
        {
            newspaperArticle = await _context.NewspaperArticles
                .Where(r => r.OwnerEmail == userEmail)
                .FirstAsync(r => r.Id == id);

            if (newspaperArticle != null)
            {
                _cache.Set(id, newspaperArticle, TimeSpan.FromMinutes(5));
            }
        }

        return newspaperArticle;
    }

    public async Task<Patent> GetPatentByIdAsync(string userEmail, int id)
    {
        if (!_cache.TryGetValue(id, out Patent patent))
        {
            patent = await _context.Patents
                .Where(r => r.OwnerEmail == userEmail)
                .FirstAsync(r => r.Id == id);

            if (patent != null)
            {
                _cache.Set(id, patent, TimeSpan.FromMinutes(5));
            }
        }

        return patent;
    }

    public async Task<Podcast> GetPodcastByIdAsync(string userEmail, int id)
    {
        if (!_cache.TryGetValue(id, out Podcast podcast))
        {
            podcast = await _context.Podcasts
                .Where(r => r.OwnerEmail == userEmail)
                .FirstAsync(r => r.Id == id);

            if (podcast != null)
            {
                _cache.Set(id, podcast, TimeSpan.FromMinutes(5));
            }
        }

        return podcast;
    }

    public async Task<Presentation> GetPresentationByIdAsync(string userEmail, int id)
    {
        if (!_cache.TryGetValue(id, out Presentation presentation))
        {
            presentation = await _context.Presentations
                .Where(r => r.OwnerEmail == userEmail)
                .FirstAsync(r => r.Id == id);

            if (presentation != null)
            {
                _cache.Set(id, presentation, TimeSpan.FromMinutes(5));
            }
        }

        return presentation;
    }

    public async Task<RadioBroadcast> GetRadioBroadcastByIdAsync(string userEmail, int id)
    {
        if (!_cache.TryGetValue(id, out RadioBroadcast radioBroadcast))
        {
            radioBroadcast = await _context.RadioBroadcasts
                .Where(r => r.OwnerEmail == userEmail)
                .FirstAsync(r => r.Id == id);

            if (radioBroadcast != null)
            {
                _cache.Set(id, radioBroadcast, TimeSpan.FromMinutes(5));
            }
        }

        return radioBroadcast;
    }

    public async Task<Report> GetReportByIdAsync(string userEmail, int id)
    {
        if (!_cache.TryGetValue(id, out Report report))
        {
            report = await _context.Reports
                .Where(r => r.OwnerEmail == userEmail)
                .FirstAsync(r => r.Id == id);

            if (report != null)
            {
                _cache.Set(id, report, TimeSpan.FromMinutes(5));
            }
        }

        return report;
    }

    public async Task<Software> GetSoftwareByIdAsync(string userEmail, int id)
    {
        if (!_cache.TryGetValue(id, out Software software))
        {
            software = await _context.Softwares
                .Where(r => r.OwnerEmail == userEmail)
                .FirstAsync(r => r.Id == id);

            if (software != null)
            {
                _cache.Set(id, software, TimeSpan.FromMinutes(5));
            }
        }

        return software;
    }

    public async Task<Statute> GetStatuteByIdAsync(string userEmail, int id)
    {
        if (!_cache.TryGetValue(id, out Statute statute))
        {
            statute = await _context.Statutes
                .Where(r => r.OwnerEmail == userEmail)
                .FirstAsync(r => r.Id == id);

            if (statute != null)
            {
                _cache.Set(id, statute, TimeSpan.FromMinutes(5));
            }
        }

        return statute;
    }

    public async Task<Thesis> GetThesisByIdAsync(string userEmail, int id)
    {
        if (!_cache.TryGetValue(id, out Thesis thesis))
        {
            thesis = await _context.Theses
                .Where(r => r.OwnerEmail == userEmail)
                .FirstAsync(r => r.Id == id);

            if (thesis != null)
            {
                _cache.Set(id, thesis, TimeSpan.FromMinutes(5));
            }
        }

        return thesis;
    }

    public async Task<TVBroadcast> GetTVBroadcastByIdAsync(string userEmail, int id)
    {
        if (!_cache.TryGetValue(id, out TVBroadcast tvBroadcast))
        {
            tvBroadcast = await _context.TVBroadcasts
                .Where(r => r.OwnerEmail == userEmail)
                .FirstAsync(r => r.Id == id);

            if (tvBroadcast != null)
            {
                _cache.Set(id, tvBroadcast, TimeSpan.FromMinutes(5));
            }
        }

        return tvBroadcast;
    }

    public async Task<VideoRecording> GetVideoRecordingByIdAsync(string userEmail, int id)
    {
        if (!_cache.TryGetValue(id, out VideoRecording videoRecording))
        {
            videoRecording = await _context.VideoRecordings
                .Where(r => r.OwnerEmail == userEmail)
                .FirstAsync(r => r.Id == id);

            if (videoRecording != null)
            {
                _cache.Set(id, videoRecording, TimeSpan.FromMinutes(5));
            }
        }

        return videoRecording;
    }

    public async Task<Website> GetWebsiteByIdAsync(string userEmail, int id)
    {
        if (!_cache.TryGetValue(id, out Website website))
        {
            website = await _context.Websites
                .Where(r => r.OwnerEmail == userEmail)
                .FirstAsync(r => r.Id == id);

            if (website != null)
            {
                _cache.Set(id, website, TimeSpan.FromMinutes(5));
            }
        }

        return website;
    }

    public async Task<Result<bool>> UpdateReferenceAsync(Reference reference)
    {
        try
        {
            _context.Reference.Update(reference);
            await _context.SaveChangesAsync();
            return Result<bool>.SuccessResult(true);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result<bool>.FailureResult("Unable to update reference");
        }
    }

    public async Task<bool> AddReferenceAsync(Reference reference)
    {
        throw new NotImplementedException();
    }


    public async Task<bool> UpdateAsync(Reference reference)
    {
        try
        {
            _context.Reference.Update(reference);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> AddAsync(Reference reference)
    {
        try
        {
            switch (reference)
            {
                case Artwork artwork:
                    _context.Artworks.Add(artwork);
                    break;
                case AudioRecording audioRecording:
                    _context.AudioRecordings.Add(audioRecording);
                    break;
                case Book book:
                    _context.Books.Add(book);
                    break;
                case BlogPost blogPost:
                    _context.BlogPosts.Add(blogPost);
                    break;
                case BookSection bookSection:
                    _context.BookSections.Add(bookSection);
                    break;
                case LegalCases cCase:
                    _context.LegalCases.Add(cCase);
                    break;
                case ConferencePaper conferencePaper:
                    _context.ConferencePapers.Add(conferencePaper);
                    break;
                case DictionaryEntry dictionaryEntry:
                    _context.DictionaryEntries.Add(dictionaryEntry);
                    break;
                case Document document:
                    _context.Documents.Add(document);
                    break;
                case Email email:
                    _context.Emails.Add(email);
                    break;
                case EncyclopediaArticle encyclopediaArticle:
                    _context.EncyclopediaArticles.Add(encyclopediaArticle);
                    break;
                case Film film:
                    _context.Films.Add(film);
                    break;
                case ForumPost forumPost:
                    _context.ForumPosts.Add(forumPost);
                    break;
                case Hearing hearing:
                    _context.Hearings.Add(hearing);
                    break;
                case InstantMessage instantMessage:
                    _context.InstantMessages.Add(instantMessage);
                    break;
                case Interview interview:
                    _context.Interviews.Add(interview);
                    break;
                case JournalArticle journalArticle:
                    _context.JournalArticles.Add(journalArticle);
                    break;
                case Letter letter:
                    _context.Letters.Add(letter);
                    break;
                case MagazineArticle magazineArticle:
                    _context.MagazineArticles.Add(magazineArticle);
                    break;
                case Manuscript manuscript:
                    _context.Manuscripts.Add(manuscript);
                    break;
                case Map map:
                    _context.Maps.Add(map);
                    break;
                case NewspaperArticle newspaperArticle:
                    _context.NewspaperArticles.Add(newspaperArticle);
                    break;
                case Patent patent:
                    _context.Patents.Add(patent);
                    break;
                case Podcast podcast:
                    _context.Podcasts.Add(podcast);
                    break;
                case Presentation presentation:
                    _context.Presentations.Add(presentation);
                    break;
                case RadioBroadcast radioBroadcast:
                    _context.RadioBroadcasts.Add(radioBroadcast);
                    break;
                case Report report:
                    _context.Reports.Add(report);
                    break;
                case Software software:
                    _context.Softwares.Add(software);
                    break;
                case Statute statute:
                    _context.Statutes.Add(statute);
                    break;
                case Thesis thesis:
                    _context.Theses.Add(thesis);
                    break;
                case TVBroadcast tvBroadcast:
                    _context.TVBroadcasts.Add(tvBroadcast);
                    break;
                case VideoRecording videoRecording:
                    _context.VideoRecordings.Add(videoRecording);
                    break;
                case Website website:
                    _context.Websites.Add(website);
                    break;
                default:
                    _context.Reference.Add(reference);
                    break;
                    
            }
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(string userEmail, int id)
    {
        try
        {
            var reference = await _context.Reference.FindAsync(id);
            if (reference == null) return false;

            _context.Reference.Remove(reference);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> DeleteRangeAsync(string userEmail, List<int> refIdList)
    {
        try
        {
            List<Reference> references = new List<Reference>();
            
            var ownedRefs = await GetAllOwnedReferencesAsync(userEmail);
            
            foreach (var refId in refIdList)
            {

                var r = ownedRefs.Data
                    .Find(r => r.Id == refId);

                if (r != null)
                {
                    references.Add(r);
                }
            }
            
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<int> GetOwnedReferenceCount(string userEmail)
    {
        try
        {
            return await _context.Reference.Where(r => r.OwnerEmail == userEmail).CountAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> DoesReferenceExist(int id)
    {
        try
        {
            return await _context.Reference.AllAsync(r => r.Id == id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}