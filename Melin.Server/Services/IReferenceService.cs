using Melin.Server.Filter;

namespace Melin.Server.Services;
using Melin.Server.Models;

public interface IReferenceService
{
    IEnumerable<Task<Reference[]>> GetAllReferencesAsync();
    List<Reference> GetAllReferences();
    
    IEnumerable<Task<Reference[]>> GetAllOwnedReferencesAsync(int userId);
    IEnumerable<Reference[]> GetAllOwnedReferences(int userId);
    Task<Reference> AddReferenceAsync(Reference newReference);
    Reference AddReference(Reference newReference);
    Task<Reference> UpdateReferenceAsync(int referenceId, Reference updatedReference);
    Reference UpdateReference(int referenceId, Reference updatedReference);
    Task<bool> DeleteReferenceAsync(int referenceId);
    bool DeleteReference(int referenceId);

    Task<Reference> GetReferenceByIdAsync(int referenceId);
    Reference GetReferenceById(int referenceId);
    Task<IEnumerable<Reference>> GetOwnedReferencesByUserIdAsync(int userId, ReferenceFilter filter = null);
    IEnumerable<Reference> GetOwnedReferencesByUserId(int userId, ReferenceFilter filter = null);
    Task<int> GetReferencesCountAsync();
    int GetReferencesCount();
    Task<bool> ReferenceExistsAsync(int referenceId);
    bool ReferenceExists(int referenceId);
    Task<IEnumerable<Reference>> GetReferencesPaginatedAsync(int page, PaginationFilter paginationFilter);
    IEnumerable<Reference> GetReferencesPaginated(int page, PaginationFilter paginationFilter);



}