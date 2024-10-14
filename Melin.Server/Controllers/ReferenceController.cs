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
    private readonly Database _database;

    public ReferenceController(ApiService apiService, Database database)
    {
        _apiService = apiService;
        _database = database;
    }

    // [HttpGet, Authorize]
    // public async Task<IActionResult> Get()
    // {
        
    // }

    [HttpPost("create-reference")]
    public async Task<ActionResult<Reference>> PostReference(Reference reference) {
        // find out reference type
        _database.Reference.Add(reference);
        await _database.SaveChangesAsync();

        return Ok();
    }

    [HttpPost("create-book")]
    public async Task<ActionResult<Book>> PostReferenceBook(Book book) {
        // find out reference type
        _database.Reference.Add(book);
        await _database.SaveChangesAsync();

        return Ok();
    }
    
}