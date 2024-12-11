using System.Data;
using Melin.Server.Filter;
using Melin.Server.Models;
using Melin.Server.Models.DTO;
using Melin.Server.Services;
using Melin.Server.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
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

    /// <summary>
    /// Gets a specific groups References.
    /// User must be authorized to access.
    /// </summary>
    /// <param name="groupName">The name of the specific group</param>
    /// <returns> A collection of References </returns>
    [HttpGet("get-group-references")]
    [Authorize]
    [ProducesResponseType(typeof(ICollection<Reference>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<ICollection<Reference>>> GetGroupReferences(string groupName)
    {
        try
        {
            if (User.Identity == null)
            {
                Log.Information("[GroupController][GetGroup()] Unauthorized User Attempted to get {Group}", groupName);
                return Unauthorized();
            }
            
            var userName = User.Identity.Name;
            
            // GET owned groups
            var group = await _referenceContext.Group
                .Where(g => g.CreatedBy == userName)
                .Where(g => g.Name == groupName)
                .FirstAsync();

            if (group is { References.Count: >= 1 })
            {
                Log.Information("[GroupController][GetGroup()] {UserEmail} retrieved {Group}", userName, groupName);
                return Ok(group.References);
            }
        }
        catch (Exception e)
        {
            Log.Warning("[GroupController][GetGroup()] Exception {Exception} thrown", e.GetBaseException());
            return BadRequest();
        }

        return NoContent();
    }
    
    /// <summary>
    /// Get all owned groups for a user. With Pagination.
    /// </summary>
    /// <param name="filter"> A PaginationFilter: { pageNumber: int, pageSize: int }</param>
    /// <returns>A List of Groups</returns>
    [HttpGet("get-owned-groups")]
    [Authorize]
    [ProducesResponseType(typeof(List<Group>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<Group>>> GetOwnedGroups([FromQuery] PaginationFilter filter)
    {
        try
        {
            if (User.Identity == null)
            {
                Log.Information("[GroupController][GetOwnedGroups()] Unauthorized User");
                return Unauthorized();
            }
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            
            var userName = User.Identity.Name;
            
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
                Log.Information("[GroupController][GetOwnedGroups()] No Groups found for {UserEmail}", userName);
                return NotFound("No Groups Found");
            }

            Log.Information("[GroupController][GetOwnedGroups()] {GroupAmount} found for {UserEmail}", groups.Count,userName);

            return Ok(groups);
        }
        catch (Exception e)
        {
            Log.Warning("[GroupController][GetOwnedGroups()] Exception {Exception} Thrown. Returning 500 BadRequest", e.GetBaseException());
            return BadRequest();
        }
    }

    /// <summary>
    /// Retrieves References from a specific Group
    /// </summary>
    /// <param name="groupName">A string specifying groupName</param>
    /// <returns>A List of References</returns>
    [HttpGet("get-references-from-group")]
    [Authorize]
    [ProducesResponseType(typeof(List<Reference>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // TODO see why this method is duplicated
    public async Task<ActionResult<List<Reference>>> GetReferencesFromGroup(string groupName)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            if (User.Identity == null)
            {
                Log.Information("User Identity is null");
                return Unauthorized();
            }
            
            Log.Information("Getting References from Group {GroupName}", groupName);
            var group = await _referenceContext.Group
                .Where(g => g.CreatedBy == User.Identity.Name)
                .Where(g => g.Name == groupName)
                .Include(r => r.References)
                .FirstAsync();

            if (group.References is not { Count: > 0 })
            {
                Log.Information("Group: {GroupName} has no References", groupName);
                return NoContent();
            }
            
            Log.Information("User: {UserName} Retrieved {ReferenceCount} References from group: {GroupName}", User.Identity.Name, group.References.Count(), groupName);
            return Ok(new Response<ICollection<Reference>>(group.References));
        }
        catch (Exception e)
        {
            Log.Warning("Exception caught when attempting to Retrieve References from Group: {GroupName}", groupName);
            return BadRequest();
        }
    }
    
    /// <summary>
    /// Retrieves References from a List of Groups
    /// </summary>
    /// <param name="groupNames">A String Array of groupNames</param>
    /// <returns>A List of References</returns>
    [HttpGet("get-references-from-multiple-groups")]
    [Authorize]
    [ProducesResponseType(typeof(List<Reference>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

            List<Reference> references = [];

            foreach (var groupName in groupNames)
            {
                var g = userOwnedGroups
                    .First(g => g.Name == groupName);

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
    
    // POST: add references to group
    [HttpPut("remove-refs-from-group")]
    [Authorize]
    public async Task<ActionResult<bool>> RemoveReferencesFromGroup(string groupName, int referenceId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            // find group
            var group = await _referenceContext.Group
                .Where(g => g.CreatedBy == User.Identity.Name)
                .Where(g => g.Name == groupName)
                .Include(g => g.References)
                .FirstAsync();

            if (group == null)
            {
                return NotFound("Group Not Found");
            }
            

            var r = await _referenceContext.Reference.FindAsync(referenceId);
            if (r != null)
            {
                if (group.References != null)
                {
                    group.References.Remove(r);
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