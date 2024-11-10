using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Melin.Server.Filter;
using Melin.Server.Interfaces;
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
    private readonly IReferenceService _referenceService;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly TagService _tagService;

    public ReferenceController(IReferenceService referenceService, UserManager<IdentityUser> userManager, TagService tagService)
    {
        _referenceService = referenceService;
        _userManager = userManager;
        _tagService = tagService;
    }
    
    
    // GET: Get All User Owned References
    // [HttpGet("references")]
    // [Authorize]
    // public async Task<IActionResult> GetReferences([FromQuery] PaginationFilter filter)
    // {
    //     
    //     var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
    //     var pagedReferences = await _referenceContext.Reference
    //         .Include(t => t.Tags)
    //         .Include(r => r.Creators)
    //         .Where(a => a.OwnerEmail == User.Identity.Name)
    //         .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
    //         .Take(validFilter.PageSize)
    //         .ToListAsync();
    //
    //     var totalRefCount = await _referenceContext.Reference
    //         .Where(a => a.OwnerEmail == User.Identity.Name)
    //         .CountAsync();
    //     
    //     return Ok(new PagedResponse<List<Reference>>(pagedReferences, validFilter.PageNumber, validFilter.PageSize, totalRefCount));
    // }

    [HttpGet("get-single-reference")]
    [Authorize]
    public async Task<IActionResult> GetSingleReference(int refId)
    {
        try
        {
            var reference = await _referenceService.GetReferenceByIdAsync(User.Identity.Name, refId);

            if (reference != null)
            {
                return Ok(reference.Data);
            }
            else
            {
                return NotFound("REFERENCE NOT FOUND");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest();
        }
    }
    
    [HttpGet("references")]
    [Authorize]
    public async Task<IActionResult> GetReferences([FromQuery] PaginationFilter filter)
    {
        var userEmail = User.Identity.Name;
        var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

        var pagedReferences = await _referenceService.GetOwnedReferencesAsync(filter, userEmail);

        var totalRefCount = await _referenceService.GetOwnedReferenceCountAsync(userEmail);
        
        return Ok(new PagedResponse<ICollection<Reference>>(pagedReferences, validFilter.PageNumber, validFilter.PageSize, totalRefCount));
    }

    // POST: Create new reference
    [HttpPost("create-reference")]
    [Authorize]
    public async Task<ActionResult<Reference>> PostReference(Reference reference) {
        if (reference == null) {
            return BadRequest("Reference cannot be null.");
        }
        
        try {
            await _referenceService.AddReferenceAsync(reference);

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


        await _referenceService.AddBookAsync(book);
        
        return Ok();
    }
    
    [HttpPost("create-artwork")]
    [Authorize]
    public async Task<ActionResult<Artwork>> PostReferenceArtwork([FromBody] Artwork artwork)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
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

        await _referenceService.AddArtworkAsync(artwork);

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
    public async Task<ActionResult<Reference>> UpdateArtwork(int oldRefId, [FromBody] Artwork artwork)
    {
        try
        {
            var prevArtwork = await _referenceService.UpdateArtworkAsync(User.Identity.Name, oldRefId, artwork);
            if (prevArtwork == false)
            {
                return NotFound("Reference Not Found. Cannot Update");
            }
            
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

            var r = await _referenceService.DeleteReferenceAsync(User.Identity.Name, refId);
            
            if (r == null)
            {
                return NotFound("Reference with ID: " + refId + " not found.");
            }
            
            
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
    public async Task<ActionResult<bool>> DeleteListOfReferences(List<int> refIdList)
    {
        try
        {
            if (refIdList.Count < 1)
            {
                return Problem("ERROR: Ref ID List has < 1 ids");
            }

            await _referenceService.DeleteReferenceRangeAsync(User.Identity.Name, refIdList);
            
            
            return Ok(true);
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}