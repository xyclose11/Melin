using Melin.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Melin.Server.Controllers;

public class GroupController : ControllerBase
{
    private readonly ReferenceContext _referenceContext;

    public GroupController(ReferenceContext referenceContext)
    {
        _referenceContext = referenceContext;
    }

    // GET: gets a specifc groups references
    [HttpGet("get-group-references")]
    [Authorize]
    public async Task<ICollection<Reference>> GetGroup(string groupName)
    {
        try
        {
            var group = await _referenceContext.Group
                .Where(g => g.Name == groupName)
                .FirstAsync();

            if (group != null && group.References.Count >= 1)
            {
                return group.References;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return null;
    }

    [HttpPost("create-group")]
    [Authorize]
    public async Task<ActionResult> CreateGroup([FromBody] Group group)
    {
        try
        {
            _referenceContext.Group.Add(group);

            await _referenceContext.SaveChangesAsync();
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}