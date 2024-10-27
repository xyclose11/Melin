using Melin.Server.Models;
using Melin.Server.Models.Context;
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
    public async Task<ActionResult<Tag>> GetOwnedTags()
    {
        try
        {
            var curUser = User.Identity.Name;
            if (curUser == null)
            {
                return NotFound("User not logged in or User not found");
            }
            
            var t = await _referenceContext.Tags
                .Where(t => t.CreatedBy == curUser)
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
    
    
    // POST: Single Tag creation
    [HttpPost("create-tag")]
    [Authorize]
    public async Task<ActionResult<Tag>> PostTag([FromBody] Tag tag)
    {

        try
        {
            // check if tag already exists
            var t = await _referenceContext.Tags.ContainsAsync(tag); // TODO test this for functionality
            if (t)
            {
                return NoContent(); // TODO test this and replace with duplicate thing instead
            }

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