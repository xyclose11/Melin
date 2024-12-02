using Melin.Server.Filter;
using Melin.Server.Models.References;
using Melin.Server.Wrappers;

namespace Melin.Server.Services;
using Melin.Server.Models;

public interface IReferenceService
{
    Task<Result<Reference>> GetReferenceByIdAsync(string userEmail, int id);
    Task<Result<Reference>> GetReferenceWithTagsById(string userEmail, int id);
    Task<Result<Reference>> GetReferenceWithGroupsById(string userEmail, int id);
    Task<Result<Reference>> GetReferenceWithAllDetailsById(string userEmail, int id);

    Task<ICollection<Reference>> GetOwnedReferencesAsync(PaginationFilter paginationFilter, string userEmail);
    Task<ICollection<Reference>> GetReferencesFromGroupAsync(PaginationFilter paginationFilter, string userEmail, string groupName);

    Task<bool> AddReferenceAsync(Reference newReference);
    
    Task<Result<bool>> UpdateReferenceAsync(string userEmail, int referenceId, Reference updatedReference);
   
    Task<bool> DeleteReferenceAsync(string userEmail, int referenceId);
    Task<bool> DeleteReferenceRangeAsync(string userEmail, List<int> referenceIds);

    Task<int> GetReferencesCountAsync();
    Task<bool> ReferenceExistsAsync(int referenceId);

    Task<int> GetOwnedReferenceCountAsync(string userEmail);


}