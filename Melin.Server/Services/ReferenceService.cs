using Melin.Server.Filter;
using Melin.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Melin.Server.Services;

public class ReferenceService : IReferenceService
{
    private readonly ReferenceContext _referenceContext;

    public ReferenceService(ReferenceContext context)
    {
        _referenceContext = context;
    }
    
    public async Task<Reference[]> GetReferencesAsync()
    {
        var references = await _referenceContext.Reference
            .ToArrayAsync();
        
        return references;
    }

    public IEnumerable<Task<Reference[]>> GetAllReferencesAsync()
    {
        throw new NotImplementedException();
    }

    public List<Reference> GetAllReferences()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Task<Reference[]>> GetAllOwnedReferencesAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Reference[]> GetAllOwnedReferences(int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<Reference> AddReferenceAsync(Reference newReference)
    {
        throw new NotImplementedException();
    }

    public Reference AddReference(Reference newReference)
    {
        throw new NotImplementedException();
    }

    public async Task<Reference> UpdateReferenceAsync(int referenceId, Reference updatedReference)
    {
        throw new NotImplementedException();
    }

    public Reference UpdateReference(int referenceId, Reference updatedReference)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteReferenceAsync(int referenceId)
    {
        throw new NotImplementedException();
    }

    public bool DeleteReference(int referenceId)
    {
        throw new NotImplementedException();
    }

    public async Task<Reference> GetReferenceByIdAsync(int referenceId)
    {
        throw new NotImplementedException();
    }

    public Reference GetReferenceById(int referenceId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Reference>> GetOwnedReferencesByUserIdAsync(int userId, ReferenceFilter filter = null)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Reference> GetOwnedReferencesByUserId(int userId, ReferenceFilter filter = null)
    {
        throw new NotImplementedException();
    }

    public async Task<int> GetReferencesCountAsync()
    {
        throw new NotImplementedException();
    }

    public int GetReferencesCount()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ReferenceExistsAsync(int referenceId)
    {
        throw new NotImplementedException();
    }

    public bool ReferenceExists(int referenceId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Reference>> GetReferencesPaginatedAsync(int page, PaginationFilter paginationFilter)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Reference> GetReferencesPaginated(int page, PaginationFilter paginationFilter)
    {
        throw new NotImplementedException();
    }
}