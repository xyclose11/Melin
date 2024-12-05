using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Melin.Server.Models.Repository;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly ReferenceContext _context;
    public GenericRepository(ReferenceContext context)
    {
        _context = context;
    }

    public void Add(T entity)
    {
        _context.Set<T>().Add(entity);
    }

    public void AddRange(IEnumerable<T> entities)
    {
        _context.Set<T>().AddRange(entities);
    }

    public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
    {
        return _context.Set<T>().Where(expression);
    }

    public IEnumerable<T> GetAll()
    {
        return _context.Set<T>().ToList();
    }

    public T GetById(int id)
    {
        return _context.Set<T>().Find(id);
    }

    public void Remove(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        _context.Set<T>().RemoveRange(entities);
    }

    public void Update(T entity)
    {
        try
        {
            _context.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public bool SaveChanges()
    {
        try
        {
            _context.SaveChanges();
            return true;
        }
        catch (DbUpdateException e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
}