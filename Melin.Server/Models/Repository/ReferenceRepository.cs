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

    public async Task<Result<Reference>> GetReferenceAllDetailsByIdAsync(string userEmail, int id)
    {
        if (!_cache.TryGetValue(id, out Reference? reference))
        {
            try
            {
                reference = await _context.Reference
                    .Where(r => r.OwnerEmail == userEmail)
                    .Include(r => r.Creators)
                    .Include(r => r.Tags)
                    // .Include(r => r.Groups)
                    .FirstOrDefaultAsync(r => r.Id == id);

                if (reference != null)
                {
                    _cache.Set(id, reference, TimeSpan.FromMinutes(5));
                    return Result<Reference>.SuccessResult(reference);
                }

                return Result<Reference>.FailureResult("Reference not found.");
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e);
                throw;
            }
            catch (OperationCanceledException operationCanceledException)
            {
                Console.WriteLine(operationCanceledException);
                throw;
            }

            
        }

        return Result<Reference>.SuccessResult(reference);
        
    }

    public async Task<bool> UpdateCreatorsAsync(Reference reference)
    {
        try
        {
            if (reference.Creators != null)
            {
                foreach (var creator in reference.Creators)
                {
                    _context.Creators.Update(creator);
                }
            }

            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
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

    public bool DeleteCreator(Creator creator)
    {
        try
        {
            _context.Creators.Remove(creator);
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