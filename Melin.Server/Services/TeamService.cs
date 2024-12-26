using Melin.Server.Data;
using Melin.Server.Models;
using Melin.Server.Models.User;
using Microsoft.AspNetCore.Identity;
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

    public async Task<bool> AddUser(UserManager<ApplicationUser> userManager, string teamName, string ownerEmail, string newUserEmail)
    {
        if (string.IsNullOrWhiteSpace(teamName) || string.IsNullOrWhiteSpace(ownerEmail) || string.IsNullOrWhiteSpace(newUserEmail))
        {
            return false;
        }

        try
        {
            var team = await _dataContext.Teams
                .Where(t => t.OwnerId == ownerEmail)
                .Where(t => t.Name == teamName)
                .FirstOrDefaultAsync();
            
            if (team == null)
            {
                return false;
            }

            var user = await userManager.FindByEmailAsync(newUserEmail);
            
            if (user == null)
            {
                return false;
            }
            
            team.Members.Add(user);
            user.Teams?.Add(team);
            await _dataContext.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public async Task<bool> RemoveUser(UserManager<ApplicationUser> userManager, string teamName, string ownerEmail, string existingUserEmail)
    {
        if (string.IsNullOrWhiteSpace(teamName) || string.IsNullOrWhiteSpace(ownerEmail) || string.IsNullOrWhiteSpace(existingUserEmail))
        {
            return false;
        }

        try
        {
            var team = await _dataContext.Teams
                .Where(t => t.OwnerId == ownerEmail)
                .Where(t => t.Name == teamName)
                .FirstOrDefaultAsync();
            
            if (team == null)
            {
                return false;
            }

            var user = await userManager.FindByEmailAsync(existingUserEmail);
            
            if (user == null)
            {
                return false;
            }
            
            team.Members.Remove(user);
            user.Teams?.Remove(team);
            await _dataContext.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
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
        // TODO: Add it so that if a Team is deleted it will cascade delete the reference in the
        // users Teams list
        await _dataContext.SaveChangesAsync();
        
        return true;
    } }