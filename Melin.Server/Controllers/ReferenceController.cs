using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Melin.Server.Filter;
using Microsoft.AspNetCore.Authorization;
using Melin.Server.Models;
using Melin.Server.Models.Context;
using Melin.Server.Services;
using Melin.Server.Wrappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Melin.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class ReferenceController : ControllerBase
{
    private readonly ApiService _apiService;
    private readonly ReferenceContext _referenceContext;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly TagService _tagService;

    public ReferenceController(ApiService apiService, ReferenceContext database, UserManager<IdentityUser> userManager, TagService tagService)
    {
        _apiService = apiService;
        _referenceContext = database;
        _userManager = userManager;
        _tagService = tagService;
    }
    
    
    // GET: Get All User Owned References
    [HttpGet("references")]
    [Authorize]
    public async Task<IActionResult> GetReferences([FromQuery] PaginationFilter filter)
    {
        
        var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
        var pagedReferences = await _referenceContext.Reference
            .Include(t => t.Tags)
            .Include(r => r.Creators)
            .Where(a => a.OwnerEmail == User.Identity.Name)
            .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
            .Take(validFilter.PageSize)
            .ToListAsync();

        var totalRefCount = await _referenceContext.Reference
            .Where(a => a.OwnerEmail == User.Identity.Name)
            .CountAsync();
        
        return Ok(new PagedResponse<List<Reference>>(pagedReferences, validFilter.PageNumber, validFilter.PageSize, totalRefCount));
    }

    // POST: Create new reference
    [HttpPost("create-reference")]
    [Authorize]
    public async Task<ActionResult<Reference>> PostReference(Reference reference) {
        if (reference == null) {
            return BadRequest("Reference cannot be null.");
        }
        
        try {
            _referenceContext.Reference.Add(reference);
            await _referenceContext.SaveChangesAsync();

            return CreatedAtAction(nameof(PostReference), new { id = reference.Id }, reference);
        } catch (Exception ex) {
            return StatusCode(500, "An error occurred while creating the reference.");
        }
    }

    [HttpPost("create-book")]
    [Authorize]
    public async Task<ActionResult<Book>> PostReferenceBook([FromBody] Book book) {
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized("User is not authenticated.");
        }

        // check for tags
        if (book.Tags != null)
        {
            bool res = await HandleTagsWithReferencePost(book.Tags);
            if (!res)
            {
                return NoContent();
            }
        }

        book.OwnerEmail = User.Identity.Name;
        book.Language = Language.English;
        book.Type = ReferenceType.Book;
        _referenceContext.Books.Add(book);
        
        await _referenceContext.SaveChangesAsync();

        return Ok();
    }
    
    [HttpPost("create-artwork")]
    [Authorize]
    public async Task<ActionResult<Artwork>> PostReferenceArtwork([FromBody] Artwork artwork)
    {
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized("User is not authenticated.");
        }

        // check for tags
        if (artwork.Tags != null)
        {
            bool res = await HandleTagsWithReferencePost(artwork.Tags);
            if (!res)
            {
                return NoContent();
            }
        }

        artwork.OwnerEmail = User.Identity.Name;
        artwork.Language = Language.English;
        artwork.Type = ReferenceType.Artwork;
        _referenceContext.Artworks.Add(artwork);
        
        await _referenceContext.SaveChangesAsync();

        return Ok();
    }

    private async Task<bool> HandleTagsWithReferencePost(ICollection<Tag> tags)
    {
        try
        {
            if (tags.Count > 1)
            {
                string createdBy = User.Identity.Name;
                await _tagService.CreateTagsAsync(tags, createdBy);
            }
            else
            {
                foreach (var tag in tags)
                {
                    tag.CreatedBy = User.Identity.Name;
                    var existingTag = await _tagService.GetTagAsync(tag.Id);
                    if (existingTag == null)
                    {
                        await _tagService.CreateTagAsync(tag);
                    }
                }
            }

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }

    }

    
    // UPDATE: update reference
    [HttpPut("update-artwork")]
    [Authorize]
    public async Task<ActionResult<Reference>> UpdateArtwork(int refId, [FromBody] Artwork artwork)
    {
        try
        {
            var prevArtwork = await _referenceContext.Artworks.FindAsync(refId);

            if (prevArtwork == null)
            {
                return NotFound("Reference Not Found. Cannot Update");
            }

            if (!prevArtwork.Title.Equals(artwork.Title))
            {
                prevArtwork.Title = artwork.Title;
            }
            
            if (!prevArtwork.Language.Equals(artwork.Language))
            {
                prevArtwork.Language = artwork.Language;
            }
            
            if (!prevArtwork.Rights.Equals(artwork.Rights))
            {
                prevArtwork.Rights = artwork.Rights;
            }
            
            if (!prevArtwork.DatePublished.Equals(artwork.DatePublished))
            {
                prevArtwork.DatePublished = artwork.DatePublished;
            }
            
            if (!prevArtwork.Medium.Equals(artwork.Medium))
            {
                prevArtwork.Medium = artwork.Medium;
            }
            
            if (!prevArtwork.MapType.Equals(artwork.MapType))
            {
                prevArtwork.MapType = artwork.MapType;
            }
            
            if (!prevArtwork.Dimensions.Equals(artwork.Dimensions))
            {
                prevArtwork.DatePublished = artwork.DatePublished;
            }
            
            if (!prevArtwork.Scale.Equals(artwork.Scale))
            {
                prevArtwork.Scale = artwork.Scale;
            }
            
            prevArtwork.UpdatedAt = DateTime.UtcNow;

            await _referenceContext.SaveChangesAsync();

            return Ok("Artwork updated successfully");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    // DELETE: Delete single reference
    [HttpDelete("delete-reference")]
    [Authorize]
    public async Task<ActionResult<bool>> DeleteSpecificReference(int refId)
    {
        try
        {
            await _referenceContext.Reference
                .Where(r => r.OwnerEmail == User.Identity.Name)
                .Where(r => r.Id == refId)
                .ExecuteDeleteAsync();

            return Ok("Reference with the ID: " + refId + " has been deleted");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    // DELETE: Delete list of references
    [HttpDelete("delete-multiple-references")]
    [Authorize]
    public async Task<ActionResult<bool>> DeleteListOfReferences(int[] refIdList)
    {
        try
        {
            if (refIdList.Length < 1)
            {
                return Problem("ERROR: Ref ID List has < 1 ids");
            }
            
            List<Reference> references = new List<Reference>();
            
            var ownedRefs = await GetCurrentUserReferenceList();
            foreach (var refId in refIdList)
            {

                var r = ownedRefs
                    .Find(r => r.Id == refId);
                
                if (r == null)
                {
                    return NotFound("Tag not found with ID: " + refId);
                }
                
                references.Add(r);
            }

            _referenceContext.Reference.RemoveRange(references);
            
            
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
    private async Task<List<Reference>> GetCurrentUserReferenceList()
    {
        try
        {
            var ownedRefs = await _referenceContext.Reference
                .Where(r => r.OwnerEmail == User.Identity.Name)
                .OrderBy(t => t.CreatedAt)
                .ToListAsync();

            if (ownedRefs != null)
            {
                return ownedRefs;
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