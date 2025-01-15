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

    [HttpPost("Update-User-Details")]
    public async Task<ActionResult<bool>> UpdateUser([FromBody] UpdateUserDto updateUserDto)
    {
        // Ideally this would utilize some kind of verification (email), but for the scope
        // of this project I kept this relatively simple
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            if (User.Identity?.Name == null)
            {
                return Unauthorized("User currently not logged in");
            }


            var userEmail = User.Identity.Name;
            var currentUser = await _userManager.FindByEmailAsync(userEmail);
            
            if (currentUser == null)
            {
                return Unauthorized("Could Not Find Current User Details");
            }

            var passwordCheck = await _userManager.CheckPasswordAsync(currentUser, updateUserDto.CurrentPassword);
            if (!passwordCheck)
            {
                ModelState.AddModelError("CurrentPassword", "The Current Password Provided does not match this user.");
                return BadRequest(ModelState);
            }

            // Update logic
            if (updateUserDto.Email != null)
            {
                var response = await UpdateUserEmail(currentUser, updateUserDto.Email);
            }

            if (updateUserDto.Password != null)
            {
                var response = await UpdateUserPasswordAsync(currentUser, updateUserDto.Password, updateUserDto.CurrentPassword);
            }

            var result = await _userManager.UpdateAsync(currentUser);

            // Handle Errors
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return BadRequest(ModelState);
            }

            return Ok(true);

        }
        catch (Exception e)
        {
            Log.Warning("Exception caught when attempting to update user details");
            return BadRequest(e);
        }
    }

    private async Task<bool> UpdateUserEmail(IdentityUser user, UpdateUserEmail updateUserEmail)
    {
        if (updateUserEmail.Email != updateUserEmail.ConfirmEmail)
        {
            return false;
        }

        if (string.IsNullOrWhiteSpace(updateUserEmail.Email) || string.IsNullOrWhiteSpace(updateUserEmail.ConfirmEmail))
        {
            return false;
        }

        // Not checking for unique email since it is defined in IdentityOptions -> Program.cs
        await _userManager.SetEmailAsync(user, updateUserEmail.Email);
        await _userManager.UpdateNormalizedEmailAsync(user);

        return true;
    }

    private async Task<bool> UpdateUserPasswordAsync(IdentityUser currentUser, UpdateUserPassword updateUserDto, string currentPassword)
    {
        if (updateUserDto.Password != updateUserDto.ConfirmPassword)
        {
            return false;
        }

        if (string.IsNullOrWhiteSpace(updateUserDto.Password) || string.IsNullOrWhiteSpace(updateUserDto.ConfirmPassword))
        {
            return false;
        }
        
        await _userManager.ChangePasswordAsync(currentUser, currentPassword, updateUserDto.Password);

        return true;
    }
}