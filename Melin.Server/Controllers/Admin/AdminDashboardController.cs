using Melin.Server.Filter;
using Melin.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    
    [HttpGet("all-users")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<List<IdentityUser>>> GetUsers([FromBody] UserPaginationFilter paginationFilter)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        try
        {
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
            
            return Ok(users);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPut("update-user-role")]
    [Authorize(Roles = "Admin")]
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
            Console.WriteLine(e);
            return BadRequest(e);
        }
    }
}