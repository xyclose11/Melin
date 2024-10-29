using System.Linq.Expressions;
using Melin.Server.Filter;

namespace Melin.Server.Models.Repository;

public interface IReferenceRepository : IGenericRepository<Reference>
{
    Task<ICollection<Reference>> GetOwnedReferences(PaginationFilter paginationFilter, string userEmail);
}