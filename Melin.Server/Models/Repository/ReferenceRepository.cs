using System.Linq.Expressions;
using Melin.Server.Filter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Melin.Server.Models.Repository;

public class ReferenceRepository : GenericRepository<Reference>, IReferenceRepository
{
    public ReferenceRepository(ReferenceContext referenceContext) : base(referenceContext)
    {
        
    }

    public async Task<ICollection<Reference>> GetOwnedReferences([FromQuery] PaginationFilter filter, string userEmail)
    {
        var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
        var pagedReferences = await _context.Reference
            .Include(t => t.Tags)
            .Include(r => r.Creators)
            .Where(a => a.OwnerEmail == userEmail)
            .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
            .Take(validFilter.PageSize)
            .ToListAsync();

        return pagedReferences;

    }
}