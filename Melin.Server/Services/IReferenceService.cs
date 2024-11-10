using Melin.Server.Filter;
using Melin.Server.Wrappers;

namespace Melin.Server.Services;
using Melin.Server.Models;

public interface IReferenceService
{
    Task<Result<Reference>> GetOwnedReference(string userEmail, int id);
    Task<Artwork> GetArtworkAsync(string userEmail, int id);
    Task<Book> GetBookAsync(string userEmail, int id);

    Task<ICollection<Reference>> GetOwnedReferencesAsync(PaginationFilter paginationFilter, string userEmail);
    Task<bool> AddReferenceAsync(Reference newReference);
    Task<bool> AddArtworkAsync(Artwork newArtwork);
    Task<bool> AddBookAsync(Book newBook);

    Task<bool> UpdateReferenceAsync(string userEmail, int referenceId, Reference updatedReference);
    Task<bool> UpdateArtworkAsync(string userEmail, int referenceId, Artwork updatedArtwork);
    Task<bool> UpdateBookAsync(string userEmail, int referenceId, Book updatedBook);

    Task<bool> DeleteReferenceAsync(string userEmail, int referenceId);
    Task<bool> DeleteReferenceRangeAsync(string userEmail, List<int> referenceIds);

    Task<Reference> GetReferenceByIdAsync(int referenceId);
    Task<int> GetReferencesCountAsync();
    Task<bool> ReferenceExistsAsync(int referenceId);

    Task<int> GetOwnedReferenceCountAsync(string userEmail);


}