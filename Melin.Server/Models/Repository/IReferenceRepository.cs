using System.Linq.Expressions;
using Melin.Server.Filter;

namespace Melin.Server.Models.Repository;

public interface IReferenceRepository : IGenericRepository<Reference>
{
    #region CRUD
        Task<List<Reference>> GetOwnedReferencesAsync(PaginationFilter paginationFilter, string userEmail);

        Task<Reference> GetByIdAsync(int id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reference"></param>
        /// <returns></returns>
        Task<Reference> UpdateAsync(Reference reference);

        Task<Reference> AddAsync(Reference reference);

        Task<bool> DeleteAsync(int id);

        #endregion
}