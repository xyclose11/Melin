﻿using Melin.Server.Filter;
using Melin.Server.Models;
using Melin.Server.Models.Context;
using Melin.Server.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    
    // GET: Single Tag
    [HttpGet("get-tag")]
    [Authorize]
    public async Task<ActionResult<Tag>> GetSpecificTag(string tagName)
    {
        try
        {
            var t = await _referenceContext.Tags
                .Where(t => t.Text == tagName)
                .FirstAsync();

            if (t != null)
            {
                return Ok(t);
            }
            
            return NotFound();
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
        
    // GET: All owned tags
    [HttpGet("get-owned-tags")]
    [Authorize]
    public async Task<ActionResult<Tag>> GetOwnedTags([FromQuery] PaginationFilter paginationFilter)
    {
        try
        {
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
            

            if (t != null)
            {
                return Ok(t);
            }

            return NotFound();
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    // GET: All owned tags
    [HttpGet("get-owned-tags-for-reference")]
    [Authorize]
    public async Task<ActionResult<Tag>> GetOwnedTagsForReference([FromQuery] PaginationFilter paginationFilter, [FromQuery] int refId)
    {
        try
        {
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
                

            var t = reference.Tags
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToList();


            if (t != null)
            {
                return Ok(t);
            }

            return NotFound();
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    
    // POST: Single Tag creation
    [HttpPost("create-tag")]
    [Authorize]
    public async Task<ActionResult<Tag>> CreateTag([FromBody] Tag tag)
    {

        try
        {
            // check if tag already exists
            // var t = await _referenceContext.Tags.ContainsAsync(tag);
            var t = await _referenceContext.Tags
                .AnyAsync(t => t.Text == tag.Text);
            
            if (t)
            {
                return NoContent();
            }

            tag.CreatedBy = User.Identity.Name;

            _referenceContext.Tags.Add(tag);

            await _referenceContext.SaveChangesAsync();
            
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    // POST: Create multiple Tags
    [HttpPost("create-multiple-tags")]
    [Authorize]
    public async Task<ActionResult<Tag>> PostMultipleTags([FromBody] List<Tag> tags)
    {
        try
        {
            string userEmail = User.Identity.Name;
            if (userEmail == null)
            {
                return Problem("User not currently found: LOCATION: POST-MULTIPLE-TAGS TAG_CONTROLLER");
            }
            
            foreach (var tag in tags)
            {
                var t = await _referenceContext.Tags.ContainsAsync(tag); // TODO test this for functionality
                if (t)
                {
                    return NoContent(); // TODO test this and replace with duplicate thing instead
                }

                tag.CreatedBy = userEmail;

                _referenceContext.Tags.Add(tag);
            }

            await _referenceContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return Problem("Unable to create Tags");
    }
    
    // POST: Add Tag to Reference
    [HttpPost("add-tag-on-reference")]
    [Authorize]
    public async Task<ActionResult<bool>> AddTagToReference(int tagId, int refId)
    {
        try
        {
            // validate Tag
            var t = await _referenceContext.Tags
                .Where(t => t.CreatedBy== User.Identity.Name)
                .Where(r => r.Id == tagId)
                .FirstAsync();
            
            if (t == null)
            {
                return NotFound("Tag with ID: " + tagId + " not found.");
            }
            
            // validate Reference
            var r = await _referenceContext.Reference
                .Where(r => r.OwnerEmail == User.Identity.Name)
                .Where(r => r.Id == refId)
                .FirstAsync();
            
            if (r == null)
            {
                return NotFound("Reference with ID: " + refId + " not found.");
            }
            
            // add tag to reference
            r.Tags.Add(t);

            await _referenceContext.SaveChangesAsync();

            return Ok("Tag added to reference successfully");

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    [HttpPost("add-tags-to-reference")]
    [Authorize]
    public async Task<ActionResult<bool>> AddTagsToReference([FromBody] AddTagsRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            if (request.Tags.Count <= 0)
            {
                return NoContent();
            }
            // validate Reference
            var r = await _referenceContext.Reference
                .Include(t => t.Tags)
                .Where(r => r.OwnerEmail == User.Identity.Name)
                .Where(r => r.Id == request.RefId)
                .FirstAsync();
            
            if (r == null)
            {
                return NotFound("Reference with ID: " + request.RefId + " not found.");
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
                
                // if not create new tag
            }

            foreach (var currentTag in currentTags)
            {
                if (!request.Tags.Any(t => t.Text == currentTag.Text))
                {
                    r.Tags.Remove(currentTag);
                }
            }

            await _referenceContext.SaveChangesAsync();

            return Ok("Tag added to reference successfully");

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    // POST: Add Tag to Reference
    [HttpPost("remove-tag-on-reference")]
    [Authorize]
    public async Task<ActionResult<bool>> RemoveTagFromReference([FromQuery] int tagId, [FromQuery] int refId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            // validate Tag
            var t = await _referenceContext.Tags
                .Where(t => t.CreatedBy== User.Identity.Name)
                .Where(r => r.Id == tagId)
                .FirstAsync();
            
            if (t == null)
            {
                return NotFound("Tag with ID: " + tagId + " not found.");
            }
            
            // validate Reference
            var r = await _referenceContext.Reference
                .Where(r => r.OwnerEmail == User.Identity.Name)
                .Where(r => r.Id == refId)
                .Include(t => t.Tags)
                .FirstAsync();
            
            if (r == null)
            {
                return NotFound("Reference with ID: " + refId + " not found.");
            }

            r.Tags.Remove(t);

            await _referenceContext.SaveChangesAsync();

            return Ok("Tag removed from reference successfully");

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    // DELETE: Delete single Tag
    [HttpPost("delete-tag")]
    [Authorize]
    public async Task<ActionResult<bool>> DeleteTag(int tagId)
    {
        try
        {
            var ownedTags = await GetCurrentUserTagList();

            Tag t = ownedTags.Find(t => t.Id == tagId);
            
            if (t == null)
            {
                return NotFound();
            }
            
            _referenceContext.Tags.Remove(t);
            
            await _referenceContext.SaveChangesAsync();
            
            return Ok(true);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    // DELETE: Delete multiple Tags
    [HttpDelete("delete-multiple-tags")]
    [Authorize]
    public async Task<ActionResult<bool>> DeleteTagRange(int[] tagIdList)
    {
        try
        {
            // validate ID list length
            if (tagIdList.Length < 1)
            {
                return Problem("ERROR: Tag ID List has < 1 ids");
            }

            List<Tag> tags = new List<Tag>();
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
            
            
            return Ok(true);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    // UPDATE: update tag
    [HttpPut("update-tag")]
    [Authorize]
    public async Task<ActionResult<Tag>> UpdateTag([FromBody] Tag updatedTag, string curTagName)
    {
        try
        {
            // find tag
            var t = await _referenceContext.Tags
                .Where(t => t.CreatedBy == User.Identity.Name)
                .FirstAsync(t => t.Text == curTagName);

            if (t == null)
            {
                return NotFound("Tag Not Found. Cannot update");
            }

            if (!t.Text.Equals(updatedTag.Text))
            {
                t.Text = updatedTag.Text;
            }
            
            t.UpdatedAt = DateTime.UtcNow;

            await _referenceContext.SaveChangesAsync();

            return Ok("Tag updated successfully");

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    

    [Authorize]
    private async Task<List<Tag>> GetCurrentUserTagList()
    {
        try
        {
            var ownedTags = await _referenceContext.Tags
                .Where(t => t.CreatedBy == User.Identity.Name)
                .OrderBy(t => t.CreatedAt)
                .ToListAsync();

            if (ownedTags != null)
            {
                return ownedTags;
            }

            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
}