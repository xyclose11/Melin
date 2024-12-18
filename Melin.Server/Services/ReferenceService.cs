using Melin.Server.Filter;
using Melin.Server.Models;
using Melin.Server.Models.References;
using Melin.Server.Models.Repository;
using Melin.Server.Wrappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Melin.Server.Services;

public class ReferenceService : IReferenceService
{
    private readonly IReferenceRepository _referenceRepository;
    private readonly IMemoryCache _cache;

    public ReferenceService(IReferenceRepository referenceRepository, IMemoryCache cache)
    {
        _referenceRepository = referenceRepository;
        _cache = cache;
    }
    
    public async Task<Result<Reference>> GetReferenceByIdAsync(string userEmail, int id)
    {
        try
        {
            var res = await _referenceRepository.GetReferenceByIdAsync(userEmail, id);
            return res;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result<Reference>.FailureResult("Unable to get reference by ID");
        }
    }

    public async Task<Result<Reference>> GetReferenceWithTagsById(string userEmail, int id)
    {
        try
        {
            var res = await _referenceRepository.GetReferenceByIdAsync(userEmail, id);
            return res;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result<Reference>.FailureResult("Unable to get reference by ID");
        }
        
    }

    public async Task<Result<Reference>> GetReferenceWithGroupsById(string userEmail, int id)
    {
        try
        {
            var res = await _referenceRepository.GetReferenceByIdAsync(userEmail, id);
            return res;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result<Reference>.FailureResult("Unable to get reference by ID");
        }
    }

    public async Task<Result<Reference>> GetReferenceWithAllDetailsById(string userEmail, int id)
    {
        try
        {
            var res = await _referenceRepository.GetReferenceAllDetailsByIdAsync(userEmail, id);
            return res;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result<Reference>.FailureResult("Unable to get reference by ID");
        }
        
    }



    public Result<bool> ApplyPatch(Reference v)
    {
        try
        {
            _referenceRepository.Update(v);
            _referenceRepository.SaveChanges();
            var r = new Result<bool>
            {
                Success = true
            };
            return r;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            var r = new Result<bool>
            {
                Success = false
            };
            return r;
        }
    }

    public async Task<ICollection<Reference>> GetOwnedReferencesAsync(PaginationFilter paginationFilter, string userEmail)
    {
        var res = await _referenceRepository.GetOwnedPaginatedReferencesAsync(paginationFilter, userEmail);
        return res.Data;
    }
    
    public async Task<ICollection<Reference>> GetReferencesFromGroupAsync(PaginationFilter paginationFilter, string userEmail, string groupName)
    {
        var res = await _referenceRepository.GetOwnedPaginatedReferencesAsync(paginationFilter, userEmail);
        return res.Data;
    }
    
    public async Task<bool> AddReferenceAsync(Reference newReference)
    {
        try
        {
            return await _referenceRepository.AddAsync(newReference);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    

    async Task<Result<bool>> IReferenceService.UpdateReferenceAsync(string userEmail, int referenceId, Reference updatedReference)
    {
        try
        {
            await _referenceRepository.UpdateReferenceAsync(updatedReference);
            return new Result<bool>
            {
                Success = true,
                Data = true,
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    

    public async Task<bool> UpdateReferenceAsync(string userEmail, int referenceId, Reference updatedReference)
    {
        throw new NotImplementedException();
    }
    
    private async Task<bool> UpdateGeneralFields(Reference prevReference, Reference newReference)
    {
        try
        {
            if (!prevReference.Title.Equals(newReference.Title))
            {
                prevReference.Title = newReference.Title;
            }

            if (prevReference.ShortTitle != null)
            {
                if (!prevReference.ShortTitle.Equals(newReference.ShortTitle))
                {
                    prevReference.ShortTitle = newReference.ShortTitle;
                }
            }
            
            if (!prevReference.Language.Equals(newReference.Language))
            {
                prevReference.Language = newReference.Language;
            }

            if (prevReference.Rights != null)
            {
                if (!prevReference.Rights.Equals(newReference.Rights))
                {
                    prevReference.Rights = newReference.Rights;
                }
            }
            
            if (!prevReference.DatePublished.Equals(newReference.DatePublished))
            {
                prevReference.DatePublished = newReference.DatePublished;
            }

            if (prevReference.Creators != null)
            {
                if (!prevReference.Creators.Equals(newReference.Creators))
                {
                    prevReference.Creators = newReference.Creators;
                }
            }

            if (prevReference.Tags != null)
            {
                if (!prevReference.Tags.Equals(newReference.Tags))
                {
                    prevReference.Tags = newReference.Tags;
                }
            }


            await _referenceRepository.UpdateReferenceAsync(prevReference);

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public async Task<bool> DeleteReferenceAsync(string userEmail, int referenceId)
    {
        try
        {
            await _referenceRepository.DeleteAsync(userEmail, referenceId);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> DeleteReferenceRangeAsync(string userEmail, List<int> referenceIds)
    {
        try
        {
            await _referenceRepository.DeleteRangeAsync(userEmail, referenceIds);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }


    public async Task<Reference> GetReferenceByIdAsync(int referenceId)
    {
        throw new NotImplementedException();
    }


    public async Task<int> GetReferencesCountAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ReferenceExistsAsync(int referenceId)
    {
        try
        {
            return await _referenceRepository.DoesReferenceExist(referenceId);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    

    public async Task<int> GetOwnedReferenceCountAsync(string userEmail)
    {
        var cacheKey = $"ReferencesCount_{userEmail}";

        if (!_cache.TryGetValue(cacheKey, out int count))
        {
            count = await _referenceRepository.GetOwnedReferenceCount(userEmail);

            _cache.Set(cacheKey, count, TimeSpan.FromMinutes(5));
        }

        return count;
    }
}