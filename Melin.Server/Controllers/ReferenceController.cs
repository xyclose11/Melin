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

    [HttpGet("references")]
    public async Task<IActionResult> GetReferences([FromQuery] PaginationFilter filter)
    {
        
        var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
        var pagedReferences = await _referenceContext.Reference
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

    [HttpPost("create-reference")]
    public async Task<ActionResult<Reference>> PostReference(Reference reference) {
        if (reference == null) {
            return BadRequest("Reference cannot be null.");
        }
        
        reference.Type = ReferenceType.Book;
        try {
            _referenceContext.Reference.Add(reference);
            await _referenceContext.SaveChangesAsync();

            return CreatedAtAction(nameof(PostReference), new { id = reference.Id }, reference);
        } catch (Exception ex) {
            return StatusCode(500, "An error occurred while creating the reference.");
        }
    }

    [HttpPost("create-book")]
    public async Task<ActionResult<Book>> PostReferenceBook(Book book) {
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
            if (artwork.Tags.Count > 1)
            {
                _tagService.CreateTagsAsync(artwork.Tags);
            }
            else
            {
                foreach (var tag in artwork.Tags)
                {
                    tag.CreatedBy = User.Identity.Name;
                    var existingTag = await _tagService.GetTagAsync(tag.Id);
                    if (existingTag == null)
                    {
                        await _tagService.CreateTagAsync(tag);
                    }
                }
            }
        }

        artwork.OwnerEmail = User.Identity.Name;
        artwork.Language = Language.English;
        artwork.Type = ReferenceType.Artwork;
        _referenceContext.Artworks.Add(artwork);
        
        await _referenceContext.SaveChangesAsync();

        return Ok();
    }



    private IActionResult CheckUserAuth()
    {
        if (User.Identity.IsAuthenticated)
        {
            return Ok();
        }

        return Unauthorized("User is not authenticated.");
    }

    


}