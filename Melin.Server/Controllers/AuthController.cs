using System.Security.Claims;
using Melin.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Serilog;
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
            Log.Information("Attempted Login with Email: {userEmail}", model.Email);
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, lockoutOnFailure: false);
            
            var user = await userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                Log.Information("Failed Login Attempt. User is null. {userEmail}", model.Email);
                return BadRequest();
            }
            
            var roles = await userManager.GetRolesAsync(user);
            
            ClaimsIdentity claims = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role, roles.First())
            });
            
            if (result.Succeeded)
            {
                // TODO test claims.RoleClaimType.First()
                Log.Information("Successful Login: {userEmail}. Authorization Level: {AuthLevel}", user.Email, claims.RoleClaimType.First());
                return Ok(claims);
            }
        } catch(Exception e) {
            Log.Warning("Exception Hit during Login attempt. {UserEmail}", model.Email);
            return BadRequest(e);
        }
        Log.Information("Failed Login Attempt. {userEmail}", model.Email);
        return Unauthorized();
    }

    [AllowAnonymous]
    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp(UserManager<IdentityUser> userManager, [FromBody] UserCreationModel userCreationModel)
    {
        Log.Information("Sign Up Started... With Email: {NewUserEmail}", userCreationModel.Email);
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        IdentityUser user = new IdentityUser()
        {
            UserName = userCreationModel.Email,
            Email = userCreationModel.Email
        };

        var result = await userManager.CreateAsync(user, userCreationModel.Password);

        if (!result.Succeeded)
        {
            Log.Information("Unable to Create New User");
            return BadRequest(result);
        }
        
        // Default Role is "User" for new users
        await userManager.AddToRoleAsync(user, "User");

        Log.Information("New User Created: {UserEmail}", user.Email);
        return Ok(result);
    }

    
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        if (User.Identity != null)
        {
            Log.Information("{UserEmail} initiated logout", User.Identity.Name);
        }
        else
        {
            Log.Warning("Unknown Party initiated logout");
        }
        
        await _signInManager.SignOutAsync();
        Log.Information("Logout Successful");
        return Ok();
    }

    [HttpGet("check")]
    [Authorize]
    public IActionResult Check()
    {
        if (User.Identity == null)
        {
            Log.Information("Unauthorized Party Attempted to check authorization");
            return StatusCode(403);
        }
        
        Log.Information("{UserEmail} checked the User.Identity Session: Success", User.Identity.Name);
        return Ok(User.Identity.IsAuthenticated);
    }

    [HttpGet("user-role")]
    [Authorize]
    public async Task<IActionResult> GetUserRole(UserManager<IdentityUser> userManager, string userEmail)
    {
        Log.Information("Initiated Check-User-Role... for {UserEmail}", userEmail);
        if (!ModelState.IsValid)
        {
            return BadRequest("UNABLE TO RETRIEVE USER ROLE");
        }

        var user = await userManager.FindByEmailAsync(userEmail);

        if (user == null)
        {
            Log.Information("Unable to find user with {UserEmail}", userEmail);
            return BadRequest("UNABLE TO RETRIEVE USER OBJECT");
        }
        
        var role = await userManager.GetRolesAsync(user);
        Log.Information("Successfully retrieved {UserEmail} roles:: {Roles}", userEmail, role);
        return Ok(role);
    }

}