using Melin.Server.Models.DTO;
using Melin.Server.Services;
using Melin.Server.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Melin.Server.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class FileController : ControllerBase
{
    private readonly IFileUploadService _fileUploadService;
    private readonly UserManager<IdentityUser> _userManager;

    public FileController(IFileUploadService fileUploadService, UserManager<IdentityUser> userManager, TagService tagService)
    {
        _fileUploadService = fileUploadService;
        _userManager = userManager;
    }

    [HttpPost("upload-files")]
    [Authorize]
    public async Task<ActionResult<Result<bool>>> UploadFile([FromForm] List<IFormFile> files)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _fileUploadService.ValidateJsonFile();
        var size = files.Sum(f => f.Length);

        return Ok(new { count = files.Count, size});
    }
}