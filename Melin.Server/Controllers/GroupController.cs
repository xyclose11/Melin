using System.Data;
using Melin.Server.Filter;
using Melin.Server.Models;
using Melin.Server.Models.DTO;
using Melin.Server.Services;
using Melin.Server.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task = Melin.Server.Models.Task;

namespace Melin.Server.Controllers;

public class GroupController : ControllerBase
{
    private readonly ReferenceContext _referenceContext;
    private readonly IReferenceService _referenceService;

    public GroupController(ReferenceContext referenceContext, IReferenceService referenceService)
    {
        _referenceContext = referenceContext;
        _referenceService = referenceService;
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

    [HttpGet("get-references-from-group")]
    [Authorize]
    public async Task<ActionResult<List<Reference>>> GetReferencesFromGroup(string groupName)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var group = await _referenceContext.Group
                .Where(g => g.CreatedBy == User.Identity.Name)
                .Where(g => g.Name == groupName)
                .Include(r => r.References)
                .FirstAsync();

            if (group.References == null)
            {
                return StatusCode(204, "Group" + groupName + " does not exist.");
            }
            
            if (group.References.Count <= 0)
            {
                return StatusCode(204, "Group" + groupName + " has no references.");
            }
            
            return Ok(new Response<ICollection<Reference>>(group.References));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e);
        }
    }
    
    [HttpGet("get-references-from-multiple-groups")]
    [Authorize]
    public async Task<ActionResult<List<Reference>>> GetReferencesFromMultipleGroups([FromQuery(Name = "groupNames")]string[] groupNames)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            if (groupNames.Length <= 0)
            {
                return BadRequest("GroupNames length is: " + groupNames.Length + " and it must by >= 1");
            }

            var userOwnedGroups = await _referenceContext.Group
                .Where(g => g.CreatedBy == User.Identity.Name)
                .Include(g => g.References)
                .ToListAsync();

            List<Reference> references = new List<Reference>();

            foreach (var groupName in groupNames)
            {
                var g = userOwnedGroups
                    .Where(g => g.Name == groupName)
                    .First();

                if (g.References != null)
                {
                    references.AddRange(g.References);
                }
                
            }
            
            return Ok(new Response<ICollection<Reference>>(references));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e);
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
            if (parent == child)
            {
                return BadRequest("Duplicate groups detected");
            }
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
                r.IsRoot = false;
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
                .Where(g => g.CreatedBy == User.Identity.Name)
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