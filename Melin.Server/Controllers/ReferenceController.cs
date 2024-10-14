using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Melin.Server.Models;

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

    // [HttpGet, Authorize]
    // public async Task<IActionResult> Get()
    // {
        
    // }

    [HttpPost("create-reference")]
    public async Task<ActionResult<Reference>> PostReference(Reference reference) {
        // find out reference type
        _referenceContext.Reference.Add(reference);
        await _referenceContext.SaveChangesAsync();

        return Ok();
    }

    [HttpPost("create-book")]
    public async Task<ActionResult<Book>> PostReferenceBook(Book book) {
        // find out reference type
        _referenceContext.Reference.Add(book);
        await _referenceContext.SaveChangesAsync();

        return Ok();
    }
    
}