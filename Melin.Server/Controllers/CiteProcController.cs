using Microsoft.AspNetCore.Mvc;

namespace Melin.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class CiteProcController : ControllerBase
{
    private readonly ApiService _apiService;

    public CiteProcController(ApiService apiService)
    {
        _apiService = apiService;
    }

    [HttpGet(Name = "GetCiteProc")]
    public async Task<IActionResult> Get()
    {
        string apiUrl = "http://localhost:3001/cite";
        string data = await _apiService.GetDataFromApiAsync(apiUrl);
        return Ok(data);
    }
    
}