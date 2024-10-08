using Melin.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Melin.Server.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly SignInManager<IdentityUser> _signInManager;

    public AuthController(SignInManager<IdentityUser> signInManager)
    {
        _signInManager = signInManager;
    }

    // [HttpPost("login")]
    // public async Task<IActionResult> Login() {
    //     var user = _signInManager.SignInAsync(User.Identity)
    //     return Ok();
    // }
    
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok();
    }

    [HttpGet("check")]
    public IActionResult Check()
    {
        return Ok(User.Identity.IsAuthenticated);
    }
}