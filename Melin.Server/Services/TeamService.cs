using Melin.Server.Data;
using Melin.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Melin.Server.Services;

public class TeamService : ITeamService
{
    private readonly DataContext _dataContext;
    
    public TeamService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    
    // TODO rename OwnerId -> ownerEmail
    public async Task<Team?> GetTeamByNameAsync(string userEmail, string teamName)
    {
        var res = await _dataContext.Teams
            .Where(t => t.OwnerId == userEmail)
            .Where(t => t.Name == teamName)
            .FirstOrDefaultAsync();

        return res;
    }

    public async Task<bool> CreateTeam(string userEmail, Team team)
    {
        _dataContext.Teams.Add(team);

        team.OwnerId = userEmail;

        await _dataContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateTeam(string userEmail, Team existingTeam, Team updatedTeam)
    {
        if (existingTeam.OwnerId != userEmail || updatedTeam.OwnerId != userEmail)
        {
            return false;
        }
        existingTeam.Name = updatedTeam.Name;
        existingTeam.Members = updatedTeam.Members;
        existingTeam.Description = updatedTeam.Description;
        await _dataContext.SaveChangesAsync();
        
        return true;
    }

    public async Task<bool> DeleteTeam(string userEmail, string teamName)
    {
        var res = await _dataContext.Teams
            .Where(t => t.OwnerId == userEmail)
            .Where(t => t.Name == teamName)
            .FirstOrDefaultAsync();

        if (res == null)
        {
            return false;
        }

        _dataContext.Teams.Remove(res);
        await _dataContext.SaveChangesAsync();
        
        return true;
    }
}