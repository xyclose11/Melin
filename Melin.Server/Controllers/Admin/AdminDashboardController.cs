using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Melin.Server.Controllers.Admin;

[ApiController]
[Route("/api/[controller]")]
public class AdminDashboardController : ControllerBase
{
    [HttpGet("all-users")]
    [Authorize(Policy = "Admin")]
    public async Task<ActionResult<List<IdentityUser>>> GetUsers()
    {
        try
        {
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}