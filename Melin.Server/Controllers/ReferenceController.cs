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
using Melin.Server.Models.References;
using Melin.Server.Services;
using Melin.Server.Wrappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Presentation = Melin.Server.Models.Presentation;
using Report = Melin.Server.Models.Report;
using Software = Melin.Server.Models.Software;
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

    [HttpGet("get-single-reference")]
    [Authorize]
    public async Task<IActionResult> GetSingleReference(int refId)
    {
        try
        {
            var reference = await _referenceService.GetReferenceWithAllDetailsById(User.Identity.Name, refId);

            if (reference.Success)
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
        validFilter.PageSize = 1000;
        var pagedReferences = await _referenceService.GetOwnedReferencesAsync(filter, userEmail);

        var totalRefCount = await _referenceService.GetOwnedReferenceCountAsync(userEmail);
        
        return Ok(new PagedResponse<ICollection<Reference>>(pagedReferences, validFilter.PageNumber, validFilter.PageSize, totalRefCount));
    }

    // POST: Create new reference
    [HttpPost("create-reference")]
    [Authorize]
    public async Task<ActionResult> PostReference([FromBody] Reference reference) {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        try {
            if (reference.Type == ReferenceType.Artwork)
            {
                var artwork = reference as Artwork;
                if (artwork == null)
                {
                    return BadRequest("Invalid Artwork.");
                }
                
                await _referenceService.AddReferenceAsync(reference);
                return Ok("Artwork created successfully");
            }
            else
            {
                return BadRequest("Unsupported reference type");
            }

        } catch (Exception ex) {
            return StatusCode(500, "An error occurred while creating the reference.");
        }
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
    
    // UPDATE
    [HttpPut("update/{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateItem(int id, [FromBody] Reference updatedItem)
    {
        // Validate model
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Find the item to update
        var existingItemResult = await _referenceService.GetReferenceWithAllDetailsById(User.Identity.Name, id);
        if (!existingItemResult.Success)
        {
            return NotFound();
        }

        var existingItem = existingItemResult.Data;

        // Replace the existing item with the updated data
        existingItem.Title = updatedItem.Title;
        existingItem.Type = updatedItem.Type;
        existingItem.Creators = updatedItem.Creators;
        existingItem.DatePublished = updatedItem.DatePublished;
    
        // Handle specific fields for types like journalArticle or book
        if (updatedItem.Type == ReferenceType.JournalArticle)
        {
            var journalArticle = existingItem as JournalArticle;
            var updatedJournalArticle = updatedItem as JournalArticle;
            journalArticle.PublicationTitle = updatedJournalArticle?.PublicationTitle;
            journalArticle.Volume = updatedJournalArticle?.Volume;
            journalArticle.Issue = updatedJournalArticle?.Issue;
            journalArticle.Pages = updatedJournalArticle?.Pages;
        }
        else if (updatedItem.Type == ReferenceType.Artwork)
        {
            var artwork = existingItem as Artwork;
            var updatedArtwork = updatedItem as Artwork;
            artwork.Dimensions = updatedArtwork?.Dimensions;
            artwork.Medium = updatedArtwork?.Medium;
            artwork.MapType = updatedArtwork?.MapType;
            artwork.Scale = updatedArtwork?.Scale;
        }
        else if (updatedItem.Type == ReferenceType.Book)
        {
            var book = existingItem as Book;
            var updatedBook = updatedItem as Book;
            book.Publisher = updatedBook?.Publisher;
            book.Edition = updatedBook?.Edition;
        }

        return Ok(existingItem); // Return the updated item
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