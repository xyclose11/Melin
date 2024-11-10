using System.Linq.Expressions;
using Melin.Server.Filter;
using Melin.Server.Wrappers;

namespace Melin.Server.Models.Repository;

public interface IReferenceRepository : IGenericRepository<Reference>
{
    #region CRUD

        Task<List<Reference>> GetAllOwnedReferencesAsync(string userEmail);
        Task<List<Reference>> GetOwnedPaginatedReferencesAsync(PaginationFilter paginationFilter, string userEmail);

        Task<Result<Reference>> GetReferenceByIdAsync(string userEmail, int id);
        Task<Artwork> GetArtworkByIdAsync(string userEmail, int id);
        Task<Book> GetBookByIdAsync(string userEmail, int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reference"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(Reference reference);

        Task<bool> AddAsync(Reference reference);

        Task<bool> DeleteAsync(string userEmail, int id);
        Task<bool> DeleteRangeAsync(string userEmail, List<int> ids);

        #endregion

        Task<int> GetOwnedReferenceCount(string userEmail);

        Task<bool> DoesReferenceExist(int id);
}