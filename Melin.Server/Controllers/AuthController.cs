using Melin.Server.Services;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Melin.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request) {
        
        // Validate user creds
        Console.WriteLine(request);
        
        var token = _authService.GenerateToken(request.Email);
        return Ok(new { Token = token });

    }
}