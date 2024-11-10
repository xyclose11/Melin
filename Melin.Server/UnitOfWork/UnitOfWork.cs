using Melin.Server.Interfaces;
using Melin.Server.Models;
using Melin.Server.Models.Repository;
using Microsoft.Extensions.Caching.Memory;

namespace Melin.Server.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ReferenceContext _referenceContext;
    private readonly IMemoryCache _cache;
    public UnitOfWork(ReferenceContext referenceContext)
    {
        _referenceContext = referenceContext;
        _cache = new MemoryCache(new MemoryCacheOptions());

        References = new ReferenceRepository(_referenceContext, _cache);
    }
    
    public IReferenceRepository References { get; private set; }

    public int Complete()
    {
        return _referenceContext.SaveChanges();
    }

    public void Dispose()
    {
        _referenceContext.Dispose();
    }
}