using System.Data;
using Melin.Server.Filter;
using Melin.Server.Models;
using Melin.Server.Models.DTO;
using Melin.Server.Models.Repository;
using Melin.Server.Services;
using Melin.Server.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Melin.Server.Controllers;

public class GroupController : ControllerBase
{
    private readonly ReferenceContext _referenceContext;
    private readonly IReferenceService _referenceService;
    private readonly IGroupService _groupService;
    public GroupController(ReferenceContext referenceContext, IReferenceService referenceService, IGroupService groupService)
    {
        _referenceContext = referenceContext;
        _referenceService = referenceService;
        _groupService = groupService;
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

    /// <summary>
    /// Retrieves a single group, with References
    /// </summary>
    /// <param name="groupId">String value for group name</param>
    /// <returns>A Group with any related References</returns>
    [HttpGet("single-group")]
    [Authorize]
    [ProducesResponseType(typeof(List<Reference>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult RetrieveSingleGroup([FromQuery] int groupId)
    {
        try
        {
            if (User.Identity?.Name == null)
            {
                Log.Information("Unauthorized attempt to Retrieve Single Group of ID: {GroupID}", groupId);
                return Unauthorized();
            }
            
            var g = _groupService.GetGroupById(User.Identity.Name, groupId);

            if (!g.Success)
            {
                Log.Information("NotFound request sent when trying to retrieve group ID: {GroupID}", groupId);
                return NotFound();
            }

            return Ok(g.Data);
        }
        catch (Exception e)
        {
            Log.Warning("Attempted to retrieve single group: {GroupName}", groupId);
            return BadRequest();
        }
    }

    /// <summary>
    /// Creates a Group with the current logged-in User as its Owner
    /// </summary>
    /// <param name="group">A <see cref="Group"/> Object</param>
    /// <returns><see cref="ActionResult{TValue}"/></returns>
    [HttpPost("create-group")]
    [Authorize]
    [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreateGroup([FromBody] Group group)
    {
        try
        {
            if (User.Identity?.Name == null)
            {
                Log.Information("Unauthorized attempt to create a new group");
                return Unauthorized();
            }
            
            group.CreatedBy = User.Identity.Name;
            
            // check if group already exists
            var g = await _referenceContext.Group.ContainsAsync(group);
            if (g)
            {
                return NoContent();
            }
            _referenceContext.Group.Add(group);

            await _referenceContext.SaveChangesAsync();
            return Ok();
        }
        catch (Exception e)
        {
            Log.Warning("Attempt to create Group failed");
            return BadRequest();
        }
    }
    
    /// <summary>
    /// Updates a Groups details via HTTP PUT
    /// </summary>
    /// <param name="prevGroupName">String Query value for the Group ID</param>
    /// <param name="updatedGroup">A <see cref="Group"/> Object</param>
    /// <returns><see cref="ActionResult{Group}"/></returns>
    // UPDATE: group related details, not contents
    [HttpPut("update-group-details")]
    [Authorize]
    [ProducesResponseType(typeof(List<Reference>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Group>> UpdateGroupDetails([FromQuery] string prevGroupName, [FromBody] Group updatedGroup)
    {
        try
        {
            if (User.Identity?.Name == null)
            {
                Log.Information("Unauthorized attempt to update Group with ID: {GroupID}", prevGroupName);
                return Unauthorized();
            }
            
            // find group
            var group = await _referenceContext.Group
                .Where(g => g.Name == prevGroupName && g.CreatedBy == User.Identity.Name)
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
            Log.Warning("Update Group failed");
            return NotFound("Group Not Found. Cannot update");
        }
    }
    
    /// <summary>
    /// Adds a list of Reference ID's to a specified Group owned by the current user
    /// </summary>
    /// <param name="groupName">String value for the group name</param>
    /// <param name="referenceIds">A <see cref="List{T}"/> of reference ID's</param>
    /// <returns>A <see cref="ActionResult{TValue}"/></returns>
    // POST: add references to group
    [HttpPost("add-refs-to-group")]
    [Authorize]
    [ProducesResponseType(typeof(ActionResult<Group>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Group>> AddReferenceToGroup([FromQuery] string groupName, [FromBody] List<int> referenceIds)
    {
        try
        {
            if (User.Identity?.Name == null)
            {
                Log.Information("Unauthorized Attempt to Add Reference(s) to a Group: {GroupName}", groupName);
                return Unauthorized();
            }
            
            // find group
            var group = await _referenceContext.Group
                .Where(g => g.Name == groupName && g.CreatedBy == User.Identity.Name)
                .FirstAsync();

            if (group.References == null)
            {
                Log.Information("Group with Name: {GroupName} has no References", groupName);
                return NotFound("Group has no References");
            }

            List<Reference> references = [];

            foreach (var referenceId in referenceIds)
            {
                var r = await _referenceContext.Reference.FindAsync(referenceId);
                
                if (r == null) continue;
                
                // see if reference is already in the group
                if (!group.References.Contains(r))
                {
                    references.Add(r);
                }
            }
            
            group.References.AddRange(references);

            await _referenceContext.SaveChangesAsync();
            return Ok();
        }
        catch (Exception e)
        {
            Log.Warning("Unable to Add References to Group");
            return BadRequest();
        }
    }
    
    /// <summary>
    /// Add a Group to a Group
    /// </summary>
    /// <param name="addGroupToGroup">A <see cref="AddGroupToGroup"/> Data Transfer Object (DTO)</param>
    /// <returns><see cref="ActionResult{TValue}"/></returns>
    // POST: add references to group
    [HttpPost("add-group-to-group")]
    [Authorize]
    [ProducesResponseType(typeof(List<Reference>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Group>> AddGroupToGroup([FromBody] AddGroupToGroup addGroupToGroup)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            if (User.Identity?.Name == null)
            {
                Log.Information("Unauthorized Attempt to Add a Group to a Group");
                return Unauthorized();
            }
            
            var parent = addGroupToGroup.Parent;
            var child = addGroupToGroup.Child;
            if (parent == child)
            {
                Log.Warning("Duplicate Group Detected when attempting to add group to a group");
                return BadRequest("Duplicate groups detected");
            }
            
            var group = await _referenceContext.Group
                .Where(g => g.Name == parent && g.CreatedBy == User.Identity.Name)
                .FirstAsync();
     
            var r = await _referenceContext.Group.Where(g => g.Name == child && g.CreatedBy == User.Identity.Name).FirstAsync();

            r.IsRoot = false;
            // see if group is already in the group
            if (group.Groups != null)
            {
                if (!group.Groups.Contains(r))
                {
                    group.Groups.Add(r);
                }
            }
            
            await _referenceContext.SaveChangesAsync();
            return Ok();
        }
        catch (Exception e)
        {
            Log.Information("Caught Exception when attempting to add a Group to another Group");
            return BadRequest();
        }
    }
    
    /// <summary>
    /// Removes References from a Group
    /// </summary>
    /// <param name="groupName">String Group-Name</param>
    /// <param name="referenceId">Integer for the Reference ID</param>
    /// <returns><see cref="ActionResult{TValue}"/></returns>
    // POST: add references to group
    [HttpPut("remove-refs-from-group")]
    [Authorize]
    [ProducesResponseType(typeof(List<Reference>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<bool>> RemoveReferencesFromGroup([FromQuery] string groupName, [FromQuery] int referenceId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            if (User.Identity?.Name == null)
            {
                Log.Information("Unauthorized Attempt to Remove References from Group");
                return Unauthorized();
            }
            
            var group = await _referenceContext.Group
                .Where(g => g.CreatedBy == User.Identity.Name)
                .Where(g => g.Name == groupName)
                .Include(g => g.References)
                .FirstAsync();

            var r = await _referenceContext.Reference.FindAsync(referenceId);
            if (r != null)
            {
                group.References?.Remove(r);
            }
            
            Log.Information("Successfully Removed Reference: {ReferenceID} from Group: {GroupName}", referenceId, groupName);
            await _referenceContext.SaveChangesAsync();
            return Ok();
        }
        catch (Exception e)
        {
            Log.Warning("Unable to Remove References from Group {GroupName}", groupName);
            return BadRequest();
        }
    }
    
    /// <summary>
    /// Delete a User's Group
    /// </summary>
    /// <param name="groupName">String Query Parameter for the Group-Name</param>
    /// <returns><see cref="ActionResult"/></returns>
    // DELETE
    [HttpDelete("delete-group")]
    [Authorize]
    [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> DeleteGroup([FromQuery] string groupName)
    {
        try
        {
            if (User.Identity?.Name == null)
            {
                Log.Information("Unauthorized Attempt to Delete Group: {GroupName}", groupName);
                return Unauthorized();
            }
            
            await _referenceContext.Group
                .Where(g => g.CreatedBy == User.Identity.Name && g.Name == groupName)
                .ExecuteDeleteAsync();

            Log.Information("User: {UserEmail}, Successfully Deleted Group: {GroupName}", User.Identity.Name, groupName);
            return Ok();
        }
        catch (Exception e)
        {
            Log.Warning("Unable to Delete Group: {GroupName}", groupName);
            return BadRequest();
        }
    }
    
}