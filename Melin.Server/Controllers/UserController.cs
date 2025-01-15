using Melin.Server.Models;
using Melin.Server.Models.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Melin.Server.Controllers;

[ApiController]
[Route("/api/auth/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;

    public UserController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }
    
    /// <summary>
    /// Retrieves a User's details
    /// </summary>
    /// <returns>A DTO Object containing minimized user details</returns>
    [HttpGet("UserDetails")]
    [ProducesResponseType(typeof(ActionResult<UserDetailDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<UserDetailDto>> GetUser()
    {
        try
        {
            if (User.Identity?.Name == null)
            {
                Log.Information("Unauthorized attempt to get user details");
                return Unauthorized("User is not logged in.");
            }

            var currentUserEmail = User.Identity.Name;
            var user = await _userManager.FindByEmailAsync(currentUserEmail);

            if (user?.Email == null)
            {
                Log.Information("Unable to find user when user is authenticated: Email: {email}", User.Identity.Name);
                return NotFound("Current User Not Found");
            }
            
            // TODO: When Custom user model is merged. Add UserDetailDTO transformation for return value
            var userDetails = new UserDetailDto
            {
                Email = user.Email,
            };
            return Ok(userDetails);
        }
        catch (Exception e)
        {
            Log.Warning("Exception Caught while attempting to retrieve User Details. Exception Thrown: {ExceptionDetails}", e.Message);
            return BadRequest();
        }

    }
}