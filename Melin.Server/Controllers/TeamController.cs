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

    [HttpPost("create")]
    [Authorize]
    public async Task<IActionResult> CreateTeam([FromBody] TeamCreateDto teamCreateDto)
    {
        if (User.Identity?.Name == null)
        {
            return Unauthorized();
        }

        var team = new Team
        {
            Name = teamCreateDto.Name,
            Description = teamCreateDto.Description,
            Members = teamCreateDto.Members,
            OwnerId = User.Identity.Name
        };


        var res = await _teamService.CreateTeam(User.Identity.Name, team);

        if (res)
        {
            return Ok();
        }
        
        return BadRequest("Unable to create new Team");
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateTeam([FromBody] Team updatedTeam, string existingTeamName)
    {
        if (User.Identity?.Name == null)
        {
            return Unauthorized();
        }

        var existingTeam = await _teamService.GetTeamByNameAsync(User.Identity.Name, existingTeamName);
        
        if (existingTeam == null)
        {
            return NotFound("Existing Team Not Found");
        }
        
        var res = await _teamService.UpdateTeam(User.Identity.Name, existingTeam, updatedTeam);

        if (res)
        {
            return Ok();
        }

        return BadRequest();
    }

    [HttpDelete("delete")]
    [Authorize]
    public async Task<IActionResult> DeleteTeam([FromQuery] string teamName)
    {
        if (User.Identity?.Name == null)
        {
            return Unauthorized();
        }

        var res = await _teamService.DeleteTeam(User.Identity.Name, teamName);

        if (res)
        {
            return Ok("Team Deleted Successfully");
        }

        return BadRequest("Team Unable to be Deleted");
    }
}