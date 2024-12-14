using System.Globalization;
using Melin.Server.Filter;
using Melin.Server.Models;
using Melin.Server.Models.Context;
using Melin.Server.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Melin.Server.Controllers;

public class TagController : ControllerBase
{
    private readonly ReferenceContext _referenceContext;
    private readonly UserManager<IdentityUser> _userManager;

    public TagController(ReferenceContext referenceContext, UserManager<IdentityUser> userManager)
    {
        _referenceContext = referenceContext;
        _userManager = userManager;
    }
    
    /// <summary>
    /// Retrieves a specific Tag from a Users list
    /// </summary>
    /// <param name="tagName">A String Tag Name</param>
    /// <returns>A <see cref="ActionResult"/> Object</returns>
    // GET: Single Tag
    [HttpGet("get-tag")]
    [Authorize]
    [ProducesResponseType(typeof(ActionResult<Tag>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Tag>> GetSpecificTag([FromQuery] string tagName)
    {
        try
        {
            if (User.Identity?.Name == null)
            {
                Log.Information("Unauthorized attempt to get a specific Tag");
                return Unauthorized();
            }
            
            var t = await _referenceContext.Tags
                .Where(t => t.Text == tagName && t.CreatedBy == User.Identity.Name)
                .FirstAsync();
            
            return Ok(t);
        }
        catch (Exception e)
        {
            Log.Warning("Unable to find Tag: {TagName}", tagName);
            return NotFound();
        }
    }
    
    /// <summary>
    /// Retrieves a Paginated List of Tags from a Users Tag List
    /// </summary>
    /// <param name="paginationFilter">A <see cref="PaginationFilter"/></param>
    /// <returns>A List of Tag Objects</returns>
    // GET: Get owned tags
    [HttpGet("get-owned-tags")]
    [Authorize]
    [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<Tag>>> GetOwnedTags([FromQuery] PaginationFilter paginationFilter)
    {
        try
        {
            if (User.Identity?.Name == null)
            {
                Log.Information("Unauthorized Attempt to get owned tags");
                return Unauthorized();
            }
            
            var validFilter = new PaginationFilter(paginationFilter.PageNumber, paginationFilter.PageSize);

            var curUser = User.Identity.Name;
            if (curUser == null)
            {
                return NotFound("User not logged in or User not found");
            }
            
            var t = await _referenceContext.Tags
                .Where(t => t.CreatedBy == curUser)
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .OrderBy(t => t.UpdatedAt)
                .ToListAsync();
            

            return Ok(t);
        }
        catch (Exception e)
        {
            Log.Warning("Unable to find any Tags for User");
            return NotFound();
        }
    }
    
    /// <summary>
    /// Retrieves Tags for a Reference
    /// </summary>
    /// <param name="paginationFilter">A <see cref="PaginationFilter"/></param>
    /// <param name="refId">String value: ReferenceID</param>
    /// <returns>A List of Tags</returns>
    // GET: Get owned tags for a reference
    [HttpGet("get-owned-tags-for-reference")]
    [Authorize]
    [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Tag>> GetOwnedTagsForReference([FromQuery] PaginationFilter paginationFilter, [FromQuery] int refId)
    {
        try
        {
            if (User.Identity?.Name == null)
            {
                Log.Information("Unauthorized Attempt to Get owned tags for Reference: {ReferenceID}", refId);
                return Unauthorized();
            }
            
            var validFilter = new PaginationFilter(paginationFilter.PageNumber, paginationFilter.PageSize);

            var curUser = User.Identity.Name;
            if (curUser == null)
            {
                return NotFound("User not logged in or User not found");
            }

            var reference = await _referenceContext.Reference
                .Where(r => r.OwnerEmail == curUser)
                .Where(r => r.Id == refId)
                .Include(t => t.Tags)
                .FirstAsync();

            if (reference.Tags is not { Count: > 0 })
            {
                return NotFound($"Reference with ID: {refId} has no corresponding Tags");
            }
            
            var t = reference.Tags
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToList();

            Log.Information("Successfully retrieved {TagCount} Tags from Reference: {ReferenceID}", t.Count, refId);
            return Ok(t);
        }
        catch (Exception e)
        {
            Log.Warning("Unable to retrieve Tags from Reference");
            return BadRequest();
        }
    }
    
    /// <summary>
    /// Creates a new Tag for the current User
    /// </summary>
    /// <param name="tag">A <see cref="Tag"/> Object</param>
    /// <returns>A List of <see cref="IActionResult"/></returns>
    // POST: Single Tag creation
    [HttpPost("create-tag")]
    [Authorize]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateTag([FromBody] Tag tag)
    {
        try
        {
            if (User.Identity?.Name == null)
            {
                Log.Information("Unauthorized Attempt to Create Tag");
                return Unauthorized();
            }
            
            var t = await _referenceContext.Tags
                .AnyAsync(t => t.Text == tag.Text);
            
            // if tag exists return
            if (t)
            {
                return NoContent();
            }

            tag.CreatedBy = User.Identity.Name;

            _referenceContext.Tags.Add(tag);

            await _referenceContext.SaveChangesAsync();
            
            Log.Information("User {UserEmail} has Successfully Created a new Tag", User.Identity.Name);
            return Ok();
        }
        catch (Exception e)
        {
            Log.Warning("Unable to Create Tag");
            Console.WriteLine(e);
            return BadRequest();
        }
    }
    
    /// <summary>
    /// Creates Multiple Tags
    /// </summary>
    /// <param name="tags">A List of Tags</param>
    /// <returns><see cref="ActionResult"/></returns>
    // POST: Create multiple Tags
    [HttpPost("create-multiple-tags")]
    [Authorize]
    [ProducesResponseType(typeof(ActionResult<Tag>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<Tag>> PostMultipleTags([FromBody] List<Tag> tags)
    {
        try
        {
            if (User.Identity?.Name == null)
            {
                Log.Information("Unauthorized attempt to create multiple Tags");
                return Unauthorized();
            }
            
            var userEmail = User.Identity.Name;
            if (userEmail == null)
            {
                Log.Information("User Email is Null");
                return BadRequest();
            }
            
            foreach (var tag in tags)
            {
                var t = await _referenceContext.Tags.ContainsAsync(tag); // TODO test this for functionality
                if (t)
                {
                    // duplicate tag detected, move to next tag iteration
                    continue;
                }

                tag.CreatedBy = userEmail;
                _referenceContext.Tags.Add(tag);
            }

            Log.Information("{TagCount} Tags Successfully Created", tags.Count);
            await _referenceContext.SaveChangesAsync();
            return Ok();
        }
        catch (Exception e)
        {
            Log.Warning("Caught exception when attempting to create multiple Tags");
            Console.WriteLine(e);
            return BadRequest();
        }

    }
    
    /// <summary>
    /// Adds a Tag to a Reference
    /// </summary>
    /// <param name="tagId">Integer value for TagID</param>
    /// <param name="refId">Integer value for ReferenceID</param>
    /// <returns>A boolean value for success result: <see cref="ActionResult{TValue}"/></returns>
    // POST: Add Tag to Reference
    [HttpPost("add-tag-on-reference")]
    [Authorize]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<bool>> AddTagToReference(int tagId, int refId)
    {
        try
        {
            if (User.Identity?.Name == null)
            {
                Log.Information("Unauthorized attempt to add Tag: {TagID} to Reference {ReferenceID}", tagId, refId);
                return Unauthorized();
            } 
            
            // validate Tag
            var t = await _referenceContext.Tags
                .Where(t => t.CreatedBy== User.Identity.Name && t.Id == tagId)
                .FirstAsync();
            
            // validate Reference
            var r = await _referenceContext.Reference
                .Where(r => r.OwnerEmail == User.Identity.Name && r.Id == refId).Include(reference => reference.Tags)
                .FirstAsync();

            // create a new List of tags since it would be null otherwise
            r.Tags ??= new List<Tag>();
            
            // add tag to reference
            r.Tags.Add(t);

            await _referenceContext.SaveChangesAsync();

            Log.Information("Added Tag: {TagID} to Reference: {ReferenceID}", tagId, refId);
            return Ok("Tag added to reference successfully");
        }
        catch (Exception e)
        {
            switch (e)
            {
                case ArgumentNullException:
                    Log.Information("Argument Null Exception Caught. Reference or Tag is Null");
                    return NotFound();
                case InvalidOperationException:
                    Log.Information("Invalid Caught. Reference or Tag is Null");
                    return NotFound();
            }

            Console.WriteLine(e);
            Log.Warning("Unable to Add Tag: {TagID} to Reference: {ReferenceID}", tagId, refId);
            return BadRequest();
        }
    }
    
    /// <summary>
    /// Adds multiple Tags to a Reference
    /// </summary>
    /// <param name="request">A <see cref="AddTagsRequest"/> DTO</param>
    /// <returns>A boolean result: <see cref="ActionResult"/></returns>
    [HttpPost("add-tags-to-reference")]
    [Authorize]
    [ProducesResponseType(typeof(ActionResult<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<bool>> AddTagsToReference([FromBody] AddTagsRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            if (User.Identity?.Name == null)
            {
                Log.Information("Unauthorized Attempt to Add Tags to a Reference");
                return Unauthorized();
            }
            
            Log.Information("Adding Tags to Reference: AddTagsRequest DTO: {AddTagsReq}", request);
            if (request.Tags.Count <= 0)
            {
                return NoContent();
            }
            
            // validate Reference
            var r = await _referenceContext.Reference
                .Where(r => r.OwnerEmail == User.Identity.Name && r.Id == request.RefId)
                .Include(t => t.Tags)
                .FirstAsync();

            if (r.Tags == null)
            {
                return NoContent();
            }
            
            var currentTags = r.Tags.ToList();
            
            // add tag to reference
            foreach (var tag in request.Tags)
            {
                // see if tag exists
                var tagExist = await _referenceContext.Tags
                    .Where(t => t.CreatedBy == User.Identity.Name)
                    .AnyAsync(t => t.Text == tag.Text);
                
                tag.CreatedBy = User.Identity.Name;

                if (!tagExist)
                {
                    // add it to the Tag DB first
                    _referenceContext.Tags.Add(tag);
                    r.Tags.Add(tag);
                }
                else
                {
                    var existingTag = await _referenceContext.Tags
                        .Where(t => t.CreatedBy == User.Identity.Name)
                        .FirstAsync(t => t.Text == tag.Text);

                    if (!r.Tags.Contains(existingTag))
                    {
                        r.Tags.Add(existingTag);

                    }
                }
            }

            foreach (var currentTag in currentTags.Where(currentTag => request.Tags.All(t => t.Text != currentTag.Text)))
            {
                r.Tags.Remove(currentTag);
            }

            await _referenceContext.SaveChangesAsync();
            Log.Information("Successfully added Tags to Reference: {ReferenceID}", request.RefId);

            return Ok("Tag added to reference successfully");
        }
        catch (Exception e)
        {
            switch (e)
            {
                case ArgumentNullException:
                    Log.Information("Argument Null Exception Caught. Reference or Tag is Null");
                    return NotFound();
                case InvalidOperationException:
                    Log.Information("Invalid Caught. Reference or Tag is Null");
                    return NotFound();
            }

            Console.WriteLine(e);
            Log.Warning("Unable to Add Tags to Reference: Request OBJ: {ReqObj}", request);
            return BadRequest();
        }
    }
    
    /// <summary>
    /// Removes a Tag from a Reference
    /// </summary>
    /// <param name="tagId">Integer Query Parameter for TagID to remove</param>
    /// <param name="refId">Integer Query Parameter for the Reference to remove from</param>
    /// <returns>A boolean result value: <see cref="ActionResult{TValue}"/></returns>
    // POST: Remove Tag from Reference
    [HttpPost("remove-tag-on-reference")]
    [Authorize]
    [ProducesResponseType(typeof(ActionResult<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<bool>> RemoveTagFromReference([FromQuery] int tagId, [FromQuery] int refId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            if (User.Identity?.Name == null)
            {
                Log.Information("Unauthorized Attempt to Remove Tag: {TagID} from Reference: {ReferenceID}", tagId, refId);
                return Unauthorized();
            }
            
            // validate Tag
            var t = await _referenceContext.Tags
                .Where(t => t.CreatedBy== User.Identity.Name && t.Id == tagId).Include(t => t.References)
                .FirstAsync();
            
            // validate Reference
            var r = await _referenceContext.Reference
                .Where(r => r.OwnerEmail == User.Identity.Name && r.Id == refId)
                .Include(r => r.Tags)
                .FirstAsync();

            if (r.Tags == null)
            {
                return NotFound("Reference Has No Tags to Remove");
            }


            // remove Tag from Reference
            r.Tags.Remove(t);
            
            // remove Reference from Tag if it exists, since a Tag's Reference List may be null
            t.References?.Remove(r);

            await _referenceContext.SaveChangesAsync();

            Log.Information("Successfully Removed Tag: {TagID} from Reference: {ReferenceID}", tagId, refId);
            return Ok("Tag removed from reference successfully");
        }
        catch (Exception e)
        {
            switch (e)
            {
                case ArgumentNullException:
                    Log.Information("Argument Null Exception Caught. Reference or Tag is Null");
                    return NotFound();
                case InvalidOperationException:
                    Log.Information("Invalid Caught. Reference or Tag is Null");
                    return NotFound();
            }

            Console.WriteLine(e);
            Log.Warning("Unable to Remove Tag: {TagID} from Reference: {referenceId}", tagId, refId);
            return BadRequest();
        }
    }
    
    /// <summary>
    /// Deletes a Tag from a users list
    /// NOTE: Different from <see cref="RemoveTagFromReference"/>. Delete is destructive. Remove simply removes the reference
    /// </summary>
    /// <param name="tagId">Integer Query Parameter for the TagID</param>
    /// <returns><see cref="ActionResult"/></returns>
    // DELETE: Delete single Tag
    [HttpPost("delete-tag")]
    [Authorize]
    [ProducesResponseType(typeof(ActionResult<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<bool>> DeleteTag(int tagId)
    {
        try
        {
            var ownedTags = await GetCurrentUserTagList();

            var t = ownedTags.Find(t => t.Id == tagId);
            
            if (t == null)
            {
                return NotFound();
            }
            
            _referenceContext.Tags.Remove(t);
            
            await _referenceContext.SaveChangesAsync();
            
            Log.Information("Successfully deleted Tag: {TagID}", tagId);
            return Ok(true);
        }
        catch (Exception e)
        {
            Log.Warning("Exception Caught when attempting to Delete Tag: {TagID}", tagId);
            Console.WriteLine(e);
            return BadRequest();
        }
    }
    
    /// <summary>
    /// Deletes multiple Tags
    /// NOTE: Different from <see cref="RemoveTagFromReference"/>. Delete is destructive. Remove simply removes the reference
    /// </summary>
    /// <param name="tagIdList">Integer Array for Tag ID's, Query Parameter</param>
    /// <returns>A boolean result: <see cref="ActionResult{TValue}"/></returns>
    // DELETE: Delete multiple Tags
    [HttpDelete("delete-multiple-tags")]
    [Authorize]
    [ProducesResponseType(typeof(ActionResult<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<bool>> DeleteTagRange([FromQuery] int[] tagIdList)
    {
        try
        {
            // validate ID list length
            if (tagIdList.Length < 1)
            {
                return NotFound();
            }

            var tags = new List<Tag>();
            var ownedTags = await GetCurrentUserTagList();
            foreach (var tagId in tagIdList)
            {
                var t = ownedTags
                    .Find(t => t.Id == tagId);
                
                if (t == null)
                {
                    return NotFound("Tag not found with ID: " + tagId);
                }
                
                tags.Add(t);
            }

            _referenceContext.Tags.RemoveRange(tags);
            
            
            await _referenceContext.SaveChangesAsync();
            
            Log.Information("Successfully Deleted Multiple Tags: {TagIDs}", tagIdList);
            return Ok(true);
        }
        catch (Exception e)
        {
            Log.Warning("Unable to Delete Multiple Tags with TagIDs: {TagIDs}", tagIdList);
            Console.WriteLine(e);
            return BadRequest();
        }
    }
    
    /// <summary>
    /// PUT request to update a tags details.
    /// NOTE: Will not update Creator, since that field should remain constant.
    /// </summary>
    /// <param name="updatedTag">A Tag object to be updated</param>
    /// <param name="curTagName">String Query Parameter for the "existing" Tag to be modified</param>
    /// <returns>An action result with the updated Tag <see cref="ActionResult"/></returns>
    // UPDATE: update tag
    [HttpPut("update-tag")]
    [Authorize]
    [ProducesResponseType(typeof(ActionResult<Tag>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Tag>> UpdateTag([FromBody] Tag updatedTag, string curTagName)
    {
        try
        {
            if (User.Identity?.Name == null)
            {
                Log.Information("Unauthorized attempt to update tag: {TagName}", curTagName);
                return Unauthorized();
            }
            
            var t = await _referenceContext.Tags
                .Where(t => t.CreatedBy == User.Identity.Name)
                .FirstAsync(t => t.Text == curTagName);

            if (!t.Text.Equals(updatedTag.Text))
            {
                t.Text = updatedTag.Text;
            }

            t.Description ??= updatedTag.Description;
            
            t.UpdatedAt = DateTime.UtcNow;

            await _referenceContext.SaveChangesAsync();

            Log.Information("Tag: {TagName} Successfully Updated", t.Text);
            return Ok("Tag updated successfully");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            Log.Warning("Exception Caught when Attempting to update tag: {CurrentTagName}, with new Tag details: {NewTag}", curTagName, updatedTag);
            return NotFound();
        }
    }
    
    /// <summary>
    /// An Asynchronous function that will verify the current users Authentication
    /// status and retrieve their owned Tags.
    /// </summary>
    /// <returns>A List of Tags</returns>
    [Authorize]
    private async Task<List<Tag>> GetCurrentUserTagList()
    {
        try
        {
            if (User.Identity?.Name == null)
            {
                Log.Information("Unauthorized Attempt to GetCurrentUserTagList");
                return [];
            }
            
            var ownedTags = await _referenceContext.Tags
                .Where(t => t.CreatedBy == User.Identity.Name)
                .OrderBy(t => t.CreatedAt)
                .ToListAsync();
            
            Log.Information("{UserEmail} Retrieved {TagAmount} Tags", User.Identity.Name, ownedTags.Count);
            return ownedTags;
        }
        catch (Exception e)
        {
            Log.Warning("{ExceptionName} Exception Caught when Retrieving Current Users Tag List", e.GetType().Name);
            return [];
        }
    }
    
}