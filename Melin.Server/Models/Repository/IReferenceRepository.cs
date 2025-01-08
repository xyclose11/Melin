using System.Linq.Expressions;
using Melin.Server.Filter;
using Melin.Server.Models.References;
using Melin.Server.Wrappers;

namespace Melin.Server.Models.Repository;

public interface IReferenceRepository : IGenericRepository<Reference>
{
    #region CRUD

        Task<Result<List<Reference>>> GetAllOwnedReferencesAsync(string userEmail);
        Task<Result<List<Reference>>> GetOwnedPaginatedReferencesAsync(PaginationFilter paginationFilter, string userEmail);

        Task<Result<T>> GetReferenceByIdAsync<T>(string userEmail, int id) where T : Reference;

        Task<Result<Reference>> GetReferenceByIdAsync(string userEmail, int id);
        Task<Result<Reference>> GetReferenceAllDetailsByIdAsync(string userEmail, int id);
        bool UpdateCreatorsAsync(Reference reference);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reference"></param>
        /// <returns></returns>
        Task<Result<bool>> UpdateReferenceAsync(Reference reference);
        Task<bool> AddAsync(Reference reference);

        Task<bool> DeleteAsync(string userEmail, int id);
        Task<bool> DeleteRangeAsync(string userEmail, List<int> ids);

        bool DeleteCreator(Creator creator);
        #endregion

        Task<int> GetOwnedReferenceCount(string userEmail);

        Task<bool> DoesReferenceExist(int id);
}