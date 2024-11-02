using Melin.Server.Models;

namespace Melin.Server.Services;

public class GroupService
{
    private readonly ReferenceContext _referenceContext;

    public GroupService(ReferenceContext referenceContext)
    {
        _referenceContext = referenceContext;
    }
    
}