using Melin.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace Melin.Server.Services;

public interface ITeamService
{
    public Task<Team?> GetTeamByNameAsync(string userEmail, string teamName);

    public Task<bool> CreateTeam(string userEmail, Team team);

    public Task<bool> UpdateTeam(string userEmail, Team existingTeam, Team updatedTeam);

    public Task<bool> DeleteTeam(string userEmail, string teamName);
}