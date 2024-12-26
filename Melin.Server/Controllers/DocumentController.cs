using System.Text;
using Melin.Server.Models;
using Melin.Server.Models.DTO;
using Melin.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
namespace Melin.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class DocumentController : ControllerBase
{
    private readonly IMelinDocumentService _documentService;

    public DocumentController(IMelinDocumentService service)
    {
        _documentService = service;
    }
    
    [HttpGet("retrieve")]
    [Authorize]
    public async Task<ActionResult<MelinDocument>> GetDocument([FromQuery] string fileName)
    {
        if (User.Identity?.Name == null)
        {
            return Unauthorized();
        }

        var document = await _documentService.GetDocumentAsync(User.Identity.Name, fileName);

        if (document != null)
        {
            return Ok(document);
        }

        return NotFound();
    }
    
    [HttpGet("retrieve-all")]
    [Authorize]
    public async Task<ActionResult<List<MelinDocument>>> GetAllDocuments()
    {
        if (User.Identity?.Name == null)
        {
            return Unauthorized();
        }

        var document = await _documentService.GetDocumentsAsync(User.Identity.Name);

        if (document != null)
        {
            return Ok(document);
        }

        return NotFound();
    }
    
    
    
    [HttpPost("create")]
    [Authorize]
    public async Task<ActionResult<MelinDocument>> CreateDocument([FromBody] CreateTextFileRequest fileRequest)
    {
        if (User.Identity?.Name == null)
        {
            return Unauthorized();
        }

        var res = await _documentService
            .CreateDocumentAsync(User.Identity.Name, fileRequest.FileName, fileRequest.Content);
        return res;
    }
}