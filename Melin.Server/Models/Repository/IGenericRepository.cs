using System.Linq.Expressions;

namespace Melin.Server.Models.Repository;

public interface IGenericRepository<T> where T : class
{
    /// <summary>
    /// Attempts to retrieve data object from database
    /// Nullable
    /// </summary>
    /// <param name="id">Integer ID for the object</param>
    /// <returns>Generic 'T' Object or null</returns>
    T? GetById(int id);
    IEnumerable<T> GetAll();
    IEnumerable<T> Find(Expression<Func<T, bool>> expression);
    IQueryable<T>? FindQueryable(Expression<Func<T, bool>> expression);
    void Add(T entity);
    void AddRangeAsync(IEnumerable<T> entities);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
    public void Update(T entity);
    public bool SaveChanges();
}