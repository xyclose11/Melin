using Castle.Components.DictionaryAdapter;
using Melin.Server.Filter;
using Melin.Server.Models;
using Melin.Server.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Melin.Server.Controllers.Admin;

[ApiController]
[Route("/api/[controller]")]
public class AdminDashboardController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    
    public AdminDashboardController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }
    
    /// <summary>
    /// Retrieves a Paginated List of all users in the current instance of the application
    /// </summary>
    /// <returns>A List of IdentityUser <see cref="IdentityUser{TKey}"/></returns>
    [HttpGet("all-users")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(ActionResult<List<IdentityUser>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<UserTableViewDto>>> GetUsers()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        try
        {
            if (User.Identity?.Name == null)
            {
               Log.Information("Unauthenticated Attempt to GetUsers for Admin Dashboard");
               return Unauthorized();
            }

            var users = await _userManager.Users
                .ToListAsync();

            List<UserTableViewDto> userTableViewList = new EditableList<UserTableViewDto>();
            foreach (var user in users)
            {
                var mappedUser = new UserTableViewDto
                {
                    Id = user.Id,
                    FullName = "", // Not currently implemented in the user model
                    Email = user.Email ?? "N/A",
                    LastLoginDate = DateTime.Now, // Not currently implemented
                    AccessFailedCount = user.AccessFailedCount,
                    PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                    EmailConfirmed = user.EmailConfirmed,
                    LockoutEnabled = user.LockoutEnabled,
                    Roles = await _userManager.GetRolesAsync(user),
                    UserName = user.UserName ?? "N/A",
                };
                
                userTableViewList.Add(mappedUser);
            }
            
            Log.Information("Admin User: {AdminUser} Retrieved {UserCount} Users", User.Identity.Name, users.Count);
            return Ok(userTableViewList);
        }
        catch (Exception e)
        {
            Log.Information("Exception Caught when retrieving Users for Admin Dashboard");
            Console.WriteLine(e);
            return BadRequest();
        }
    }

    /// <summary>
    /// Updates a Users Role via HTTP PUT
    /// NOTE: Calling User must have the Role of "Admin"
    /// </summary>
    /// <param name="userEmail">A string value for the desired user to be updated</param>
    /// <param name="newRole">A string value for the new role</param>
    /// <returns>A Status Code</returns>
    [HttpPut("update-user-role")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateUserRole([FromQuery] string userEmail, [FromBody] string newRole)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            // Validate new Role
            var roleResult = Enum.IsDefined(typeof(Roles), newRole);
            if (!roleResult)
            {
                return BadRequest("Invalid Role");
            }
            
            // Retrieve user
            var user = await _userManager.FindByEmailAsync(userEmail);

            if (user == null)
            {
                return BadRequest($"USER WITH EMAIL: {userEmail} DOES NOT EXIST");
            }

            var res = await _userManager.AddToRoleAsync(user, newRole);

            if (res.Succeeded)
            {
                return Ok(user);
            }

            return BadRequest(res.Errors);
            
        }
        catch (Exception e)
        {
            Log.Information("Exception Caught when updating a users role");
            Console.WriteLine(e);
            return BadRequest(e);
        }
    }

    /// <summary>
    /// Deletes a user from the database
    /// Caller must contain the "Admin" role
    /// </summary>
    /// <param name="userEmail">String query parameter for the desired user to delete</param>
    /// <returns>A Status Code for the result of the action</returns>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DeleteUser([FromQuery] string userEmail)
    {
        try
        {
            if (User.Identity?.Name == null)
            {
                Log.Information("Unauthorized attempt to delete user: {UserEmail}", userEmail);
                return Unauthorized();
            }

            var user = await _userManager.FindByEmailAsync(userEmail);

            if (user == null)
            {
                Log.Information("User not found when attempting to delete User: {UserEmail}", userEmail);
                return NotFound();
            }
            
            var response = await _userManager.DeleteAsync(user);

            if (!response.Succeeded)
            {
                Log.Information("User: {UserEmail} Unsuccessfully deleted", userEmail);
                return BadRequest();
            }
            
            Log.Information("User: {UserEmail} deleted, by Admin: {AdminUser}", userEmail, User.Identity.Name);
            return Ok($"User {userEmail} Successfully Deleted");
        }
        catch (Exception e)
        {
            Log.Warning("Exception Caught when attempting to delete user: {UserEmail}", userEmail);
            Console.WriteLine(e);
            return BadRequest();
        }
    }
}