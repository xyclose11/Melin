using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Melin.Server.Models.Repository;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly ReferenceContext _context;
    public GenericRepository(ReferenceContext context)
    {
        _context = context;
    }

    public IQueryable<T>? FindQueryable(Expression<Func<T, bool>> expression)
    {
        try
        {
            var res = _context.Set<T>()
                .Where(expression);

            Log.Information("Successfully Retrieved List of Objects of Type: {ObjectType}", typeof(T));
            return res;
        }
        catch (Exception e)
        {
            Log.Error("FindAsync Unable to retrieve with Expression: {EXPRESSION} :: Returning Empty Array", expression);
            return null;
        }
    }

    public void Add(T entity)
    {
        _context.Set<T>().Add(entity);
    }

    public async Task<bool> AddAsync(T entity)
    {
        try
        {
            await _context.Set<T>().AddAsync(entity);
            // TODO ensure that this method is implemented correctly and checks for the correct response type
            return true;
        }
        catch (Exception e)
        {
            Log.Warning("Exception Caught Attempting to AddAsync");
            Console.WriteLine(e);
            return false;
        }
    }

    public async void AddRangeAsync(IEnumerable<T> entities)
    {
        await _context.Set<T>().AddRangeAsync(entities);
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

    public T? GetById(int id)
    {
        try
        {
            Log.Information("Getting Data Object with ID: {ID}. Of type {GenericType}", id, typeof(T));
            var r = _context.Set<T>().Find(id);

            return r;
        }
        catch (Exception e)
        {
            Log.Warning("[GenericRepository][GetById] Unable to get data object with ID: {ID} of type: {GenericType}", id, typeof(T));
            return null;
        }
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