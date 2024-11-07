namespace Melin.Server.Services;
using Melin.Server.Models;

public interface IReferenceService
{
    Task<Reference[]> GetReferencesAsync();
}