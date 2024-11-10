using System.Linq.Expressions;
using System.Text.Json;
using Melin.Server.Filter;
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

    public async Task<List<Reference>> GetAllOwnedReferencesAsync(string userEmail)
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
                return pagedReferences;
            }
            else
            {
                return new List<Reference>();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<List<Reference>> GetOwnedPaginatedReferencesAsync(PaginationFilter filter, string userEmail)
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
        

        return pagedReferences;

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
                case Book book:
                    _context.Books.Add(book);
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

                var r = ownedRefs
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