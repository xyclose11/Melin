using System.Security.Claims;
using Melin.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Task = Melin.Server.Models.Task;

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
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(UserManager<IdentityUser> userManager, [FromBody] LoginModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, lockoutOnFailure: false);

            var user = await userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return BadRequest();
            }
            
            var roles = await userManager.GetRolesAsync(user);
            
            
            ClaimsIdentity claims = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role, roles.First())
            });
            
            if (result.Succeeded)
            {
                return Ok();
            }
        } catch(Exception e) {
            return BadRequest(e);
        }
        
        return Unauthorized();
    }

    [AllowAnonymous]
    [HttpPost("sign-up")]
    public async Task<IResult> SignUp(UserManager<IdentityUser> userManager, [FromBody] UserCreationModel userCreationModel)
    {

        IdentityUser user = new IdentityUser()
        {
            UserName = userCreationModel.Email,
            Email = userCreationModel.Email
        };
        
        var result = await userManager.CreateAsync(user, userCreationModel.Password);
        await userManager.AddToRoleAsync(user, userCreationModel.Role);

        if (!result.Succeeded)
        {
            return Results.BadRequest(result);
        }

        return Results.Ok(result);
    }

    
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok();
    }

    [HttpGet("check")]
    [Authorize]
    public IActionResult Check()
    {
        return Ok(User.Identity.IsAuthenticated);
    }

    [HttpGet("user-role")]
    [Authorize]
    private string GetUserRole(ClaimsPrincipal user)
    {
        return "";
    }

}