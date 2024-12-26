using System.Security.Claims;
using Melin.Server.Models;
using Melin.Server.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Melin.Server.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AuthController(SignInManager<ApplicationUser> signInManager)
    {
        _signInManager = signInManager;
    }
    
    /// <summary>
    /// Handles custom Login logic for ASP.NET Core Identity
    /// </summary>
    /// <param name="userManager">A <see cref="UserManager{TUser}"/></param>
    /// <param name="model"><see cref="LoginModel"/></param>
    /// <returns><see cref="IActionResult"/></returns>
    [HttpPost("login")]
    [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login(UserManager<ApplicationUser> userManager, [FromBody] LoginModel model)
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

            user.LastLoginDate = DateTime.UtcNow;
            
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

    /// <summary>
    /// Handles Custom Implementation of ASP.NET Core Account Creation
    /// </summary>
    /// <param name="userManager"><see cref="UserManager{TUser}"/></param>
    /// <param name="userCreationModel"><see cref="UserCreationModel"/></param>
    /// <returns><see cref="IActionResult"/></returns>
    [AllowAnonymous]
    [HttpPost("sign-up")]
    [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SignUp(UserManager<ApplicationUser> userManager, [FromBody] UserCreationModel userCreationModel)
    {
        Log.Information("Sign Up Started... With Email: {NewUserEmail}", userCreationModel.Email);
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        ApplicationUser user = new ApplicationUser()
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

    /// <summary>
    /// Handles the logout logic
    /// </summary>
    /// <returns><see cref="IActionResult"/></returns>
    [HttpPost("logout")]
    [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
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

    /// <summary>
    /// Used to check the status of the Users Authentication status
    /// </summary>
    /// <returns><see cref="IActionResult"/></returns>
    [HttpGet("check")]
    [Authorize]
    [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public IActionResult Check()
    {
        if (User.Identity == null)
        {
            Log.Information("Unauthorized Party Attempted to check authorization");
            return Forbid();
        }
        
        Log.Information("{UserEmail} checked the User.Identity Session: Success", User.Identity.Name);
        return Ok(User.Identity.IsAuthenticated);
    }

    /// <summary>
    /// Retrieves the current Users role(s)
    /// </summary>
    /// <param name="userManager"><see cref="UserManager{TUser}"/></param>
    /// <returns><see cref="IActionResult"/></returns>
    [HttpGet("user-role")]
    [Authorize]
    [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetUserRole(UserManager<ApplicationUser> userManager)
    {
        if (User.Identity?.Name == null)
        {
            return Unauthorized();
        }

        var userEmail = User.Identity.Name;
        
        Log.Information("Initiated Check-User-Role... for {UserEmail}", userEmail);
        if (!ModelState.IsValid)
        {
            return BadRequest("UNABLE TO RETRIEVE USER ROLE");
        }
        
        var user = await userManager.FindByEmailAsync(userEmail);

        if (user == null)
        {
            Log.Information("Unable to find user with {UserEmail}", userEmail);
            return Unauthorized("UNABLE TO RETRIEVE USER OBJECT");
        }
        
        var role = await userManager.GetRolesAsync(user);
        Log.Information("Successfully retrieved {UserEmail} roles:: {Roles}", userEmail, role);
        return Ok(role);
    }


    [HttpPost("enable-disable-account")]
    [Authorize]
    // TODO Log and Documentation
    public async Task<IActionResult> EnableDisableAccount([FromQuery] string userEmail, [FromQuery] bool decision, UserManager<ApplicationUser> userManager)
    {
        if (User.Identity?.Name == null)
        {
            return Unauthorized();
        }

        var user = await userManager.FindByEmailAsync(userEmail);

        if (user == null)
        {
            return StatusCode(500, "Unable to find user in user manager");
        }

        user.IsActive = decision;

        return Ok($"User has been: {decision}");
    }

}