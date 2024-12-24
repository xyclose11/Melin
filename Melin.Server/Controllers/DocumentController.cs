using System.Text;
using Melin.Server.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
namespace Melin.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class DocumentController : ControllerBase
{
    [HttpGet("retrieve")]
    public IActionResult GetDocument([FromQuery] string fileName)
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "UserDocuments", fileName);

        if (!System.IO.File.Exists(filePath))
        {
            return NotFound("Filed not found");
        }

        var fileContent = System.IO.File.ReadAllText(filePath);

        return Ok(fileContent);
    }
    
    [HttpPost("create")]
    public IActionResult CreateDocument([FromBody] CreateTextFileRequest fileRequest)
    {
        if (string.IsNullOrWhiteSpace(fileRequest.FileName))
        {
            return BadRequest("File name is required.");
        }

        try
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "UserDocuments", fileRequest.FileName);
            System.IO.File.WriteAllText(filePath, fileRequest.Content);
            return Ok(new { Message = "File created successfully" });
        }
        catch (Exception e)
        {
            return StatusCode(500, "Internal server error occured when Creating Document");
        }
    }
}