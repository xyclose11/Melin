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
using Microsoft.AspNetCore.JsonPatch;
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
            await _referenceService.AddReferenceAsync(reference);
            return Ok("Artwork created successfully");
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
    [HttpPatch("update/{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateItem(int id, [FromBody] JsonPatchDocument<Reference> updatedItem)
    {
        // Validate model
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (updatedItem == null)
        {
            return BadRequest(ModelState);
        }

        if (User.Identity == null)
        {
            return Unauthorized("User Currently Not Authorized to Update Item");
        }

        if (User.Identity.Name == null)
        {
            return Unauthorized("User Has Incorrect Auth Details. Attempt to Login Again: Please Advise.");
        }

        // Find the item to update
        var existingItemResult = await _referenceService.GetReferenceWithAllDetailsById(User.Identity.Name, id);
        if (!existingItemResult.Success)
        {
            return NotFound();
        }

        var existingItem = existingItemResult.Data;
        
        updatedItem.ApplyTo(existingItem, ModelState);

        if (!ModelState.IsValid)
        {
            return BadRequest("ERROR");
        }
        
        // apply patch to DB
        _referenceService.ApplyPatch(existingItem);
        return new ObjectResult(existingItem);
        
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