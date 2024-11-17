using System.Data;
using Melin.Server.Filter;
using Melin.Server.Models;
using Melin.Server.Models.DTO;
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
    public async Task<ActionResult<ICollection<Reference>>> GetGroup(string groupName)
    {
        try
        {
            var userName = User.Identity.Name;
            if (userName == null)
            {
                return Unauthorized("user ");
            }

            // GET owned groups
            var group = await _referenceContext.Group
                .Where(g => g.CreatedBy == userName)
                .Where(g => g.Name == groupName)
                .FirstAsync();

            if (group is { References.Count: >= 1 })
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
    
    // GET: All owned groups
    [HttpGet("get-owned-groups")]
    [Authorize]
    public async Task<ActionResult<List<Group>>> GetOwnedGroups([FromQuery] PaginationFilter filter)
    {
        try
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            
            var userName = User.Identity.Name;
            if (userName == null)
            {
                return Unauthorized("user ");
            }

            // GET owned groups
            var groups = await _referenceContext.Group
                .Where(g => g.CreatedBy == userName)
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Include(g => g.References)
                .Include(g => g.Groups)
                .Take(validFilter.PageSize)
                .OrderBy(g => g.UpdatedAt)
                .ToListAsync();

            if (groups == null)
            {
                return NotFound("No Groups Found");
            }

            return groups;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPost("create-group")]
    [Authorize]
    public async Task<ActionResult> CreateGroup([FromBody] Group group)
    {
        try
        {
            // updated createdBy
            if (group.CreatedBy == null)
            {
                group.CreatedBy = User.Identity.Name;
            }
            
            // check if group already exists
            var g = await _referenceContext.Group.ContainsAsync(group); // TODO test this
            if (g)
            {
                return NoContent(); // TODO put duplicate reply here
            }
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
    
    // UPDATE: group related details, not contents
    [HttpPost("update-group-details")]
    [Authorize]
    public async Task<ActionResult<Group>> UpdateGroupDetails(string prevGroupName, [FromBody] Group updatedGroup)
    {
        try
        {
            // find group
            var group = await _referenceContext.Group
                .Where(g => g.Name == prevGroupName)
                .FirstAsync();
            
            // update Group details
            group.Name = updatedGroup.Name;
            group.UpdatedAt = DateTime.UtcNow;
            group.Description = updatedGroup.Description;

            await _referenceContext.SaveChangesAsync();
            
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return NotFound("Group Not Found. Cannot update");
        }
    }
    
    // POST: add references to group
    [HttpPost("add-refs-to-group")]
    [Authorize]
    public async Task<ActionResult<Group>> AddReferenceToGroup(string groupName, [FromBody] List<int> referenceIds)
    {
        try
        {
            // find group
            var group = await _referenceContext.Group
                .Where(g => g.Name == groupName)
                .FirstAsync();

            if (group == null)
            {
                return NotFound("Group Not Found");
            }



            List<Reference> references = new List<Reference>();

            foreach (var referenceId in referenceIds)
            {
                var r = await _referenceContext.Reference.FindAsync(referenceId);
                if (r != null)
                {
                    // see if reference is already in the group
                    if (!group.References.Contains(r))
                    {
                        references.Add(r);
                    }
                }
            }
            
            group.References.AddRange(references);

            await _referenceContext.SaveChangesAsync();
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    // POST: add references to group
    [HttpPost("add-group-to-group")]
    [Authorize]
    public async Task<ActionResult<Group>> AddGroupToGroup([FromBody] AddGroupToGroup addGroupToGroup)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var parent = addGroupToGroup.parent;
            var child = addGroupToGroup.child;
            // find group
            var group = await _referenceContext.Group
                .Where(g => g.Name == parent)
                .FirstAsync();

            if (group == null)
            {
                return NotFound("Group Not Found");
            }
            
     
            var r = await _referenceContext.Group.Where(g => g.Name == child).FirstAsync();
            if (r != null)
            {
                // see if group is already in the group
                if (group.Groups != null)
                {
                    if (!group.Groups.Contains(r))
                    {
                        group.Groups.Add(r);
                    }
                }

            }
            
            await _referenceContext.SaveChangesAsync();
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    
    // DELETE
    [HttpDelete("delete-group")]
    [Authorize]
    public async Task<ActionResult> DeleteGroup(string groupName)
    {
        try
        {
            await _referenceContext.Group
                .Where(g => g.Name == groupName)
                .ExecuteDeleteAsync();

            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
}