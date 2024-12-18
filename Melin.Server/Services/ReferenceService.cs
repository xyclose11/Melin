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
            var existingReferenceResult = await _referenceRepository.GetReferenceAllDetailsByIdAsync(userEmail, referenceId);

            if (existingReferenceResult is { Success: false, Data: not null })
            {
                return new Result<bool>
                {
                    Success = false,
                    Data = false,
                };
            }

            if (existingReferenceResult.Data == null)
            {
                return new Result<bool>
                {
                    Success = false,
                    Data = false,
                };
            }

            var existingReference = existingReferenceResult.Data;
            var generalFieldResult = UpdateGeneralFields(existingReference, updatedReference);
            
            
            var res = await _referenceRepository.UpdateReferenceAsync(existingReference);
            if (res.Success)
            {
                return new Result<bool>
                {
                    Success = true,
                    Data = true,
                };
            }
            return new Result<bool>
            {
                Success = false,
                Data = false,
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
    
    private bool UpdateGeneralFields(Reference existingReference, Reference updatedReference)
    {
        try
        {
            if (!existingReference.Title.Equals(updatedReference.Title))
            {
                existingReference.Title = updatedReference.Title;
            }

            if (existingReference.ShortTitle != null)
            {
                if (!existingReference.ShortTitle.Equals(updatedReference.ShortTitle))
                {
                    existingReference.ShortTitle = updatedReference.ShortTitle;
                }
            }
            
            if (!existingReference.Language.Equals(updatedReference.Language))
            {
                existingReference.Language = updatedReference.Language;
            }

            if (existingReference.Rights != null)
            {
                if (!existingReference.Rights.Equals(updatedReference.Rights))
                {
                    existingReference.Rights = updatedReference.Rights;
                }
            }
            
            if (!existingReference.DatePublished.Equals(updatedReference.DatePublished))
            {
                existingReference.DatePublished = updatedReference.DatePublished;
            }

            if (existingReference.Creators != null)
            {
                if (!existingReference.Creators.Equals(updatedReference.Creators))
                {
                    existingReference.Creators = updatedReference.Creators;
                }
            }

            if (existingReference.Tags != null)
            {
                if (!existingReference.Tags.Equals(updatedReference.Tags))
                {
                    existingReference.Tags = updatedReference.Tags;
                }
            }


            // await _referenceRepository.UpdateReferenceAsync(existingReference);

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