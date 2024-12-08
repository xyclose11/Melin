using Melin.Server.Filter;
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

    public AdminDashboardController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
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

    // [HttpPut("change-user-role")]
    // [Authorize(Roles = "Admin")]
    // public async Task<IActionResult> ChangeUserRole()
    // {
    //     RoleManager<IdentityRole>
    // }
}