using System.Linq.Expressions;

namespace Melin.Server.Models.Repository;

public interface IGenericRepository<T> where T : class
{
    T GetById(int id);
    IEnumerable<T> GetAll();
    IEnumerable<T> Find(Expression<Func<T, bool>> expression);
    void Add(T entity);
    void AddRange(IEnumerable<T> entities);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
    public void Update(T entity);
    public bool SaveChanges();
}