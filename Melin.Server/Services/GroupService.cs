using System.Linq.Expressions;
using System.Security.Claims;
using Melin.Server.Filter;
using Melin.Server.Models;
using Melin.Server.Models.Repository;
using Melin.Server.Wrappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Serilog;
using Task = System.Threading.Tasks.Task;

namespace Melin.Server.Services;

/// <summary>
/// Handles Group business logic.
/// Requires UserEmail
/// </summary>
public interface IGroupService
{
    /// <summary>
    /// Retrieves a single group that is owned by a user.
    /// DOES NOT INCLUDE REFERENCES OR OTHER GROUPS
    /// </summary>
    /// <param name="id">Integer value for the 'id' of the Group</param>
    /// <param name="userEmail">String value for a Users Email</param>
    /// <returns>Result Wrapper object with Group as its data</returns>
    Result<Group> GetGroupById(string userEmail, int id);
    
    /// <summary>
    /// Similar to GetGroupById but includes all associated Groups and References.
    ///
    /// </summary>
    /// <param name="id">Integer value for the 'id' of the Group</param>
    /// <param name="userEmail">String value for a Users Email</param>
    /// <returns>Result Wrapper object with Group as its data</returns>
    Result<Group> GetGroupAllDetailsById(string userEmail, int id);
    
    Task<Result<IEnumerable<Group>>> GetGroupsAsync(string userEmail, PaginationFilter filter);

    Task<bool> AddGroupAsync(string userEmail, Group group);
    Task<bool> UpdateGroupAsync(string userEmail, int groupId, Group newGroup);
    bool DeleteGroup(string userEmail, int groupId);
    Task<bool> DeleteGroupRangeAsync(string userEmail, List<int> groupIds);
    Task<Result<int>> GetGroupCountAsync(string userEmail);
    Task<Result<int>> GetNestedReferencesInGroupCountAsync(string userEmail);
    
}

public class GroupService : IGroupService
{
    private readonly IGenericRepository<Group> _groupRepository;
    public GroupService(IGenericRepository<Group> repository)
    {
        _groupRepository = repository;
    }

    public Result<Group> GetGroupById(string userEmail, int id)
    {
        try
        {
            // TODO CHANGE THIS TO INCLUDE USER VERIFICATION
            var r = _groupRepository.GetById(id);
            
            if (r == null)
            {
                Log.Information("Unable to find group with ID: {GroupID}", id);
                return new Result<Group>
                {
                    Data = null,
                    ErrorMessage = "Unable to find Group",
                    Success = false
                };
            }

            return new Result<Group>
            {
                Data = r,
                ErrorMessage = "",
                Success = true
            };
        }
        catch (Exception e)
        {
            Log.Warning("Unable to find group with ID: {GroupID}", id);
            return new Result<Group>
            {
                Data = null,
                ErrorMessage = "Unable to find Group",
                Success = false
            };
        }
    }

    // TODO DETERMINE IF THE METHOD ABOVE AND BELOW DO THE SAME THING
    public Result<Group> GetGroupAllDetailsById(string userEmail, int id)
    {
        try
        {
            // var r = _groupRepository.GetById(id);
            // TODO check to see if the GenericRepository 'Find' works as expected
            var res = _groupRepository.Find(g => g.CreatedBy == userEmail && g.Id == id);
            var r = res.FirstOrDefault();

            if (r == null)
            {
                Log.Information("Unable to find group with ID: {GroupID}", id);
                return new Result<Group>
                {
                    Data = null,
                    ErrorMessage = "Unable to find Group",
                    Success = false
                };
            }

            return new Result<Group>
            {
                Data = r,
                ErrorMessage = "",
                Success = true
            };
        }
        catch (Exception e)
        {
            Log.Warning("Unable to find group with ID: {GroupID}", id);
            return new Result<Group>
            {
                Data = null,
                ErrorMessage = "Unable to find Group",
                Success = false
            };
        }
    }

    // TODO SEE IF THE <see /> XML BELOW ACTUALLY WORKS
    /// <summary>
    /// Retrieves a paginated list of a users groups
    /// </summary>
    /// <param name="filter">PaginationFilter Object <see cref="PaginationFilter"/></param>
    /// <param name="userEmail">String depicting userEmail</param>
    /// <returns>Result</returns>
    public async Task<Result<IEnumerable<Group>>> GetGroupsAsync(string userEmail, PaginationFilter filter)
    {
        try
        {

            Expression<Func<Group, bool>> expression = group =>
                group.CreatedBy == userEmail;
            
            var groupQuery = _groupRepository.FindQueryable(expression);

            if (groupQuery == null)
            {
                Log.Warning("Group IQueryable is null");
                return new Result<IEnumerable<Group>>
                {
                    Data = null,
                    ErrorMessage = "IQueryable is null. Check IGenericRepository, and Appropriate Log Files.",
                    Success = false
                };
            }
            
            // Apply Pagination
            var groups = await groupQuery
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            Result<IEnumerable<Group>> result = new Result<IEnumerable<Group>>
            {
                Data = groups,
                ErrorMessage = "",
                Success = true
            };
            
            Log.Information("Successfully Retrieved {GroupCount}", groups.Count);
            return result;
        }
        catch (Exception e)
        {
            Log.Warning("Exception Caught Attempting to get user groups. PageSize: {PageSize} PageNumber: {PageNumber} User: {UserEmail}", filter.PageSize, filter.PageNumber, userEmail);
            return new Result<IEnumerable<Group>>
            {
                Data = null,
                ErrorMessage = "Unable to find Group",
                Success = false
            };
        }
    }
    

    public async Task<bool> AddGroupAsync(string userEmail, Group group)
    {
        try
        {
            return await _groupRepository.AddAsync(group);
        }
        catch (Exception e)
        {
            // TODO TEST IF LOGGING CONFIG ADDS THE USER, ENDPOINT, METHOD, AND HOST TO EACH LOG
            Log.Warning("Exception Caught Attempting to Add Group Async");
            return false;
        }
    }

    public async Task<bool> UpdateGroupAsync(string userEmail, int groupId, Group newGroup)
    {
        try
        {
            // TODO IMPLEMENT THIS FUNCTION
            await Task.CompletedTask;
            return false;
        }
        catch (Exception e)
        {
            Log.Warning("Exception Caught Attempting to Update Group Async");
            return false;
        }
    }

    public bool DeleteGroup(string userEmail, int groupId)
    {
        try
        {
            var group = _groupRepository.GetById(groupId);

            if (group == null)
            {
                Log.Information("Cannot Find group to delete");
                return false;
            }
            
            _groupRepository.Remove(group);
            return true;
        }
        catch (Exception e)
        {
            Log.Warning("Exception Caught Attempting to Delete Group");
            return false;
        }
        
    }

    public async Task<bool> DeleteGroupRangeAsync(string userEmail, List<int> groupIds)
    {
        try
        {
            var ownedGroups = _groupRepository.FindQueryable(g => g.CreatedBy == userEmail);

            if (ownedGroups == null)
            {
                Log.Information("Owned Groups Queryable is null");
                return false;
            }
            
            // TODO TEST THIS FOR FUNCTIONALITY. NOT SURE IF IT WILL GET ALL GROUPS
            var groupsToDelete = await ownedGroups
                .Where(g => groupIds.Contains(g.Id))
                .ToListAsync();

            _groupRepository.RemoveRange(groupsToDelete);

            Log.Information("Successfully Removed {GroupCount} Groups", ownedGroups.Count());
            return true;
        }
        catch (Exception e)
        {
            Log.Warning("Exception Caught Attempting to Add Group Async");
            return false;
        }
    }

    public async Task<Result<int>> GetGroupCountAsync(string userEmail)
    {
        try
        {
            // TODO FIND OUT IF FINDQUERYABLE WILL RETURN 0 IF THERE ARE 0 GROUPS
            var r = _groupRepository.FindQueryable(g => g.CreatedBy == userEmail);

            if (r == null)
            {
                Log.Information("Owned Groups Queryable is null");
                return new Result<int>
                {
                    Data = 0,
                    ErrorMessage = "",
                    Success = true
                };
            }

            var countAsync = await r.CountAsync();
            return new Result<int>
            {
                Data = countAsync,
                ErrorMessage = "",
                Success = true
            };
        }
        catch (Exception e)
        {
            Log.Warning("Exception Caught Attempting to Add Group Async");
            return new Result<int>
            {
                Data = -1,
                ErrorMessage = "Exception Caught when attempting to get Group Count",
                Success = false
            };
        }
    }

    public async Task<Result<int>> GetNestedReferencesInGroupCountAsync(string userEmail)
    {
        try
        {
            // TODO IMPLEMENT METHOD
            await Task.CompletedTask;
            return new Result<int>
            {
                Data = -1,
                ErrorMessage = "Exception Caught when attempting to get Group Count",
                Success = false
            };
        }
        catch (Exception e)
        {
            Log.Warning("Exception Caught Attempting to Add Group Async");
            return new Result<int>
            {
                Data = -1,
                ErrorMessage = "Exception Caught when attempting to get Group Count",
                Success = false
            };
        }
    }
}