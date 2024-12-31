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
    private const int MaxFileCount = 10;
    private const long MaxFileSize = 1024 * 1024 * 3;
    private const long MaxTotalSize = 1024 * 1024 * 12;
    private static readonly string[] AllowedExtensions = new[] { ".txt", ".csv", ".bibtex", ".json"};
    private static readonly string[] AllowedMimeTypes = new[] { "text/plain", "text/csv", "application/x-bibtex", "application/json" };
    private static readonly string TemporaryUploadPath = Path.Combine(Directory.GetCurrentDirectory(),"TempUploads");
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

        switch (files.Count)
        {
            case <= 0:
                return BadRequest("No files were uploaded.");
            case > MaxFileCount:
                return BadRequest($"You can upload a maximum of {MaxFileCount} files.");
        }

        if (files.Any(f => f.Length == 0))
        {
            return BadRequest("One or more files are empty.");
        }

        if (files.Any(f => f.Length > MaxFileSize))
        {
            return BadRequest("One or more files exceed the maximum allowed size of 3MB");
        }

        var maxTotalSize = files.Sum(f => f.Length);
        if (MaxTotalSize > maxTotalSize)
        {
            return BadRequest("The total size of uploaded files exceeds the 12 MB limit.");
        }

        // TODO create a custom validation object that will return which files are problematic
        if (files.Any(f => !AllowedExtensions.Contains(Path.GetExtension(f.FileName).ToLower())))
        {
            return BadRequest("One or more files have an unsupported file extension");
        }

        if (files.Any(f => !AllowedMimeTypes.Contains(f.ContentType)))
        {
            return BadRequest("One or more files have an unsupported content type.");
        }

        foreach (var fileName in files.Select(file => Path.GetFileName(file.FileName)).Where(fileName => string.IsNullOrWhiteSpace(fileName) || fileName.IndexOfAny(Path.GetInvalidPathChars()) >= 0))
        {
            return BadRequest($"Invalid file name: {fileName}");
        }

        if (!Directory.Exists(TemporaryUploadPath))
        {
            Directory.CreateDirectory(TemporaryUploadPath);
        }

        foreach (var file in files)
        {
            var filePath = Path.Combine(TemporaryUploadPath, Path.GetFileName(file.FileName));
            await using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);
        }

        await _fileUploadService.ValidateFile();
        var size = files.Sum(f => f.Length);

        return Ok(new { count = files.Count, size});
    }
}