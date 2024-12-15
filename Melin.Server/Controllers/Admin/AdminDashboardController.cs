using Melin.Server.Filter;
using Melin.Server.Models;
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
    /// <param name="paginationFilter">A PaginationFilter Object <see cref="PaginationFilter"/></param>
    /// <returns>A List of IdentityUser <see cref="IdentityUser{TKey}"/></returns>
    [HttpGet("all-users")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(ActionResult<List<IdentityUser>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<IdentityUser>>> GetUsers([FromBody] UserPaginationFilter paginationFilter)
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
            
            // Validate filter to ensure non-negative values
            if (paginationFilter.PageNumber < 0)
            {
                return BadRequest("Page Number cannot be negative");
            }

            if (paginationFilter.PageSize < 0)
            {
                return BadRequest("Page Size cannot be negative");
            }

            var users = await _userManager.Users
                .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
                .Take(paginationFilter.PageSize)
                .ToListAsync();
            
            Log.Information("Admin User: {AdminUser} Retrieved {UserCount} Users", User.Identity.Name, users.Count);
            return Ok(users);
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
}