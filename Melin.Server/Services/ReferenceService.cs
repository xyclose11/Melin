using Melin.Server.Filter;
using Melin.Server.Models;
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
    
    public async Task<Result<Reference>> GetOwnedReference(string email, int id)
    {
        return await _referenceRepository.GetReferenceByIdAsync(email, id);
    }

    public async Task<Artwork> GetArtworkAsync(string email, int id)
    {
        return await _referenceRepository.GetArtworkByIdAsync(email, id);
    }

    public async Task<Book> GetBookAsync(string email,int id)
    {
        return await _referenceRepository.GetBookByIdAsync(email, id);
    }

    public async Task<ICollection<Reference>> GetOwnedReferencesAsync(PaginationFilter paginationFilter, string userEmail)
    {
        return await _referenceRepository.GetOwnedPaginatedReferencesAsync(paginationFilter, userEmail);
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

    public async Task<bool> AddArtworkAsync(Artwork newArtwork)
    {
        try
        {
            newArtwork.Language = Language.English;
            newArtwork.Type = ReferenceType.Artwork;
            return await _referenceRepository.AddAsync(newArtwork);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }    
    }

    public async Task<bool> AddBookAsync(Book newBook)
    {
        try
        {
            newBook.Language = Language.English;
            newBook.Type = ReferenceType.Book;
            return await _referenceRepository.AddAsync(newBook);
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
            
            if (!prevReference.Language.Equals(newReference.Language))
            {
                prevReference.Language = newReference.Language;
            }
            
            if (!prevReference.Rights.Equals(newReference.Rights))
            {
                prevReference.Rights = newReference.Rights;
            }
            
            if (!prevReference.DatePublished.Equals(newReference.DatePublished))
            {
                prevReference.DatePublished = newReference.DatePublished;
            }

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> UpdateArtworkAsync(string userEmail, int artworkId, Artwork updatedArtwork)
    {
        try
        {
            var artwork = await GetArtworkAsync(userEmail, artworkId);
            
            await UpdateGeneralFields( artwork,updatedArtwork);
            
            if (!updatedArtwork.Medium.Equals(artwork.Medium))
            {
                updatedArtwork.Medium = artwork.Medium;
            }
            
            if (!updatedArtwork.MapType.Equals(artwork.MapType))
            {
                updatedArtwork.MapType = artwork.MapType;
            }
            
            if (!updatedArtwork.Dimensions.Equals(artwork.Dimensions))
            {
                updatedArtwork.DatePublished = artwork.DatePublished;
            }
            
            if (!updatedArtwork.Scale.Equals(artwork.Scale))
            {
                updatedArtwork.Scale = artwork.Scale;
            }
            
            updatedArtwork.UpdatedAt = DateTime.UtcNow;

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> UpdateBookAsync(string userEmail, int referenceId, Book updatedBook)
    {
        throw new NotImplementedException();
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