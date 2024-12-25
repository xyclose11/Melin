using Melin.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Melin.Server.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class TeamController : ControllerBase
{
    public TeamController()
    {
        
    }
    
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<Team>> RetrieveTeam([FromQuery] string teamName)
    {
        if (User.Identity?.Name == null)
        {
            return Unauthorized();
        }
        
        
    }
}