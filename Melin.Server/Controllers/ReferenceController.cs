using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Melin.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Melin.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class ReferenceController : ControllerBase
{
    private readonly ApiService _apiService;
    private readonly ReferenceContext _referenceContext;

    public ReferenceController(ApiService apiService, ReferenceContext database)
    {
        _apiService = apiService;
        _referenceContext = database;
    }

    [HttpGet("references")] // GET: all references for a user
    public List<Reference> GetReferences() {

        var references = _referenceContext.Reference.ToList();
        return references;
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
            // Log the exception (ex) here as needed
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
    public async Task<ActionResult<Artwork>> PostReferenceArtwork(Artwork artwork)
    {
        artwork.Language = Language.English;
        artwork.Type = ReferenceType.Artwork;
        _referenceContext.Artworks.Add(artwork);
        await _referenceContext.SaveChangesAsync();

        return Ok();
    }

    


}