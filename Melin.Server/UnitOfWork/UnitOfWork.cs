using Melin.Server.Interfaces;
using Melin.Server.Models;
using Melin.Server.Models.Repository;

namespace Melin.Server.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ReferenceContext _referenceContext;

    public UnitOfWork(ReferenceContext referenceContext)
    {
        _referenceContext = referenceContext;
        References = new ReferenceRepository(_referenceContext);
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