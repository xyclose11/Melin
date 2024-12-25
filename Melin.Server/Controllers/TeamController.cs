using Melin.Server.Models;
using Melin.Server.Models.DTO;
using Melin.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Melin.Server.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class TeamController : ControllerBase
{
    private readonly ITeamService _teamService;
    public TeamController(ITeamService teamService)
    {
        _teamService = teamService;
    }
    
    [HttpGet("retrieve-by-name")]
    [Authorize]
    public async Task<ActionResult<Team>> RetrieveTeam([FromQuery] string teamName)
    {
        if (User.Identity?.Name == null)
        {
            return Unauthorized();
        }

        var res = await _teamService.GetTeamByNameAsync(User.Identity.Name, teamName);

        if (res == null)
        {
            return NotFound($"Cannot find Team with name: {teamName}");
        }

        return Ok(res);
    }
    
    [HttpPost]
    [Authorize]
    public async Task
}