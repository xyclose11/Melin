using Melin.Server.Models.Repository;

namespace Melin.Server.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IReferenceRepository References { get; }
    int Complete();
}