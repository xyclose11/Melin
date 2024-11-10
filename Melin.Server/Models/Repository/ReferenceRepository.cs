using System.Linq.Expressions;
using System.Text.Json;
using Melin.Server.Filter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Melin.Server.Models.Repository;

public class ReferenceRepository : GenericRepository<Reference>, IReferenceRepository
{
    public ReferenceRepository(ReferenceContext referenceContext) : base(referenceContext)
    {
        
    }

    public async Task<List<Reference>> GetOwnedReferencesAsync(PaginationFilter filter, string userEmail)
    {
        var validFilter = new PaginationFilter(
            filter.PageNumber > 0 ? filter.PageNumber : 1, 
            filter.PageSize > 0 ? filter.PageSize : 10
        );
        var pagedReferences = await _context.Reference
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

    public async Task<Reference> GetByIdAsync(int id)
    {
        var cacheKey = $"Reference_{id}";
        var cachedReference = await _cache.GetStringAsync(cacheKey);
    
        if (cachedReference != null)
        {
            return JsonSerializer.Deserialize<Reference>(cachedReference);
        }

        var reference = await _context.Reference.FindAsync(id);
        if (reference != null)
        {
            await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(reference));
        }

        return reference;
    }

    public async Task<Reference> UpdateAsync(Reference reference)
    {
        try
        {
            _context.Reference.Update(reference);
            await _context.SaveChangesAsync();
            return reference;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Reference> AddAsync(Reference reference)
    {
        try
        {
            await _context.Reference.AddAsync(reference);
            await _context.SaveChangesAsync();
            return reference;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(int id)
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
}