using Melin.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Melin.Server.Services;

public class ReferenceService : IReferenceService
{
    private readonly ReferenceContext _referenceContext;

    public ReferenceService(ReferenceContext context)
    {
        _referenceContext = context;
    }
    
    public async Task<Reference[]> GetReferencesAsync()
    {
        var references = await _referenceContext.Reference
            .ToArrayAsync();

        return references;
    }
}