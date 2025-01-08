using Melin.Server.Filter;
using Melin.Server.Models;
using Melin.Server.Models.References;
using Melin.Server.Models.Repository;
using Melin.Server.Wrappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Serilog;

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
    

    public async Task<Result<bool>> UpdateReferenceAsync(string userEmail, int referenceId, Reference updatedReference)
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

            UpdateGeneralFields(existingReference, updatedReference);
            
            // ensure that both references are of the same type
            if (existingReference.Type != updatedReference.Type)
            {
                return new Result<bool>
                {
                    Success = false
                };
            }


            // determine Reference type and update fields
            switch (existingReference.Type)
            {
                case ReferenceType.Artwork:
                    if (existingReference is not Artwork existingArtwork|| updatedReference is not Artwork updatedArtwork)
                    {
                        return new Result<bool>
                        {
                            Success = false
                        };
                    }
                    UpdateArtworkFields(existingArtwork, updatedArtwork);
                    break;
                case ReferenceType.AudioRecording:
                    if (existingReference is not AudioRecording existingAudioRecording|| updatedReference is not AudioRecording updatedAudioRecording)
                    {
                        return new Result<bool>
                        {
                            Success = false
                        };
                    }
                    UpdateAudioRecordingFields(existingAudioRecording, updatedAudioRecording);
                    break;
                case ReferenceType.Book:
                    if (existingReference is not Book existingBook|| updatedReference is not Book updatedBook)
                    {
                        return new Result<bool>
                        {
                            Success = false
                        };
                    }
                    UpdateBookFields(existingBook, updatedBook);
                    break;
                case ReferenceType.BookSection:
                    if (existingReference is not BookSection existingBookSection|| updatedReference is not BookSection updatedBookSection)
                    {
                        return new Result<bool>
                        {
                            Success = false
                        };
                    }
                    UpdateBookSectionFields(existingBookSection, updatedBookSection);
                    break;
                case ReferenceType.Document:
                    if (existingReference is not Document existingDocument || updatedReference is not Document updatedDocument)
                    {
                        return new Result<bool>
                        {
                            Success = false
                        };
                    }
                    UpdateDocumentFields(existingDocument, updatedDocument);
                    break;
                case ReferenceType.JournalArticle:
                    if (existingReference is not JournalArticle existingJournalArticle || updatedReference is not JournalArticle updatedJournalArticle)
                    {
                        return new Result<bool>
                        {
                            Success = false
                        };
                    }
                    UpdateJournalArticleFields(existingJournalArticle, updatedJournalArticle);
                    break;
                case ReferenceType.EncyclopediaArticle:
                    if (existingReference is not EncyclopediaArticle existingEncycArticle || updatedReference is not EncyclopediaArticle updatedEncycArticle)
                    {
                        return new Result<bool>
                        {
                            Success = false
                        };
                    }
                    UpdateEncyclopediaArticleFields(existingEncycArticle, updatedEncycArticle);
                    break;
            }
            
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
    
    
    private void UpdateGeneralFields(Reference existingReference, Reference updatedReference)
    {
        try
        {
            // Update Title
            if (!string.Equals(existingReference.Title, updatedReference.Title, StringComparison.Ordinal))
            {
                existingReference.Title = updatedReference.Title;
            }

            // Update ShortTitle
            if (!string.Equals(existingReference.ShortTitle, updatedReference.ShortTitle, StringComparison.Ordinal))
            {
                existingReference.ShortTitle = updatedReference.ShortTitle;
            }
            
            if (!existingReference.Language.Equals(updatedReference.Language))
            {
                existingReference.Language = updatedReference.Language;
            }

            // Update Rights
            if (updatedReference.Rights != null && 
                (existingReference.Rights == null || !existingReference.Rights.SequenceEqual(updatedReference.Rights)))
            {
                existingReference.Rights = updatedReference.Rights;
            }
            
            // Update DatePublished
            if (existingReference.DatePublished != updatedReference.DatePublished)
            {
                existingReference.DatePublished = updatedReference.DatePublished;
            }
            
            // Update Tags
            if (updatedReference.Tags != null &&
                (existingReference.Tags == null || !existingReference.Tags.SequenceEqual(updatedReference.Tags)))
            {
                existingReference.Tags = updatedReference.Tags.ToList();
            }

            
            UpdateCreators(existingReference, updatedReference);
            
        }
        catch (Exception e)
        {
            Log.Warning("Unable to Update Reference: {Existing}, with Updated: {Updated}", existingReference, updatedReference);
            Console.WriteLine(e);
            throw;
        }
    }

    private void UpdateCreators(Reference existingReference, Reference updatedReference)
    {
        try
        {
            var creatorsToRemove = existingReference.Creators
                .Where(c => updatedReference.Creators.All(uc => uc.Id != c.Id))
                .ToList();
            
            foreach (var creator in creatorsToRemove)
            {
                existingReference.Creators.Remove(creator);
                _referenceRepository.DeleteCreator(creator);
            }
            
            existingReference.Creators
                .Where(existing => updatedReference.Creators.Any(u => u.Id == existing.Id))
                .ToList()
                .ForEach(existing =>
                {
                    var updated = updatedReference.Creators.First(u => u.Id == existing.Id);
                    existing.FirstName = updated.FirstName;
                    existing.LastName = updated.LastName;
                    existing.Types = updated.Types;
                });

            var creatorsToAdd = updatedReference.Creators
                .Where(updated => !existingReference.Creators.Any(existing => existing.Id == updated.Id))
                .ToList();

            foreach (var creator in creatorsToAdd)
            {
                existingReference.Creators.Add(creator);
            }

            _referenceRepository.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            Log.Error("Exception thrown when attempting to update creators");
            throw;
        }
    }


    private static void UpdateArtworkFields(Artwork existingReference, Artwork updatedReference)
    {
        if (updatedReference == null)
        {
            throw new ArgumentNullException(nameof(updatedReference), "Updated reference cannot be null.");
        }

        if (!string.Equals(existingReference.Medium, updatedReference.Medium, StringComparison.Ordinal))
        {
            existingReference.Medium = updatedReference.Medium;
        }

        if (!string.Equals(existingReference.Dimensions, updatedReference.Dimensions, StringComparison.Ordinal))
        {
            existingReference.Dimensions = updatedReference.Dimensions;
        }

        if (existingReference.Scale != updatedReference.Scale)
        {
            existingReference.Scale = updatedReference.Scale;
        }

        if (existingReference.MapType != updatedReference.MapType)
        {
            existingReference.MapType = updatedReference.MapType;
        }
    }

    
    private static void UpdateAudioRecordingFields(AudioRecording existing, AudioRecording updated)
    {
        if (updated == null)
        {
            throw new ArgumentNullException(nameof(updated), "Updated audio recording cannot be null.");
        }

        // Update textual fields
        if (!string.Equals(existing.AudioRecordingFormat, updated.AudioRecordingFormat, StringComparison.Ordinal))
        {
            existing.AudioRecordingFormat = updated.AudioRecordingFormat;
        }

        if (!string.Equals(existing.SeriesTitle, updated.SeriesTitle, StringComparison.Ordinal))
        {
            existing.SeriesTitle = updated.SeriesTitle;
        }

        // Update numeric fields
        if (existing.Volume != updated.Volume)
        {
            existing.Volume = updated.Volume;
        }

        if (existing.NumberOfVolumes != updated.NumberOfVolumes)
        {
            existing.NumberOfVolumes = updated.NumberOfVolumes;
        }

        // Update other fields
        if (!string.Equals(existing.Place, updated.Place, StringComparison.Ordinal))
        {
            existing.Place = updated.Place;
        }

        if (!string.Equals(existing.Label, updated.Label, StringComparison.Ordinal))
        {
            existing.Label = updated.Label;
        }

        if (existing.RunningTime != updated.RunningTime)
        {
            existing.RunningTime = updated.RunningTime;
        }
    }


    private static void UpdateBookFields(Book existing, Book updated)
    {
        if (updated == null)
        {
            throw new ArgumentNullException(nameof(updated), "Update Book cannot be null.");
        }

        existing.Publication = updated.Publication;
        existing.BookTitle = updated.BookTitle;
        existing.Volume = updated.Volume;
        existing.Issue = updated.Issue;
        existing.Pages = updated.Pages;
        existing.Edition = updated.Edition;
        existing.Series = updated.Series;
        existing.SeriesNumber = updated.SeriesNumber;
        existing.SeriesTitle = updated.SeriesTitle;
        existing.VolumeAmount = updated.VolumeAmount;
        existing.PageAmount = updated.PageAmount;
        existing.Section = updated.Section;
        existing.Place = updated.Place;
        existing.Publisher = updated.Publisher;
        existing.JournalAbbr = updated.JournalAbbr;
        existing.ISBN = updated.ISBN;
        existing.ISSN = updated.ISSN;
    }

    private static void UpdateBookSectionFields(BookSection existing, BookSection updated)
    {
        if (updated == null)
        {
            throw new ArgumentNullException(nameof(updated), "Update Book Section cannot be null.");
        }
        
        existing.BookTitle = updated.BookTitle;
        existing.Series = updated.Series;
        existing.SeriesNumber = updated.SeriesNumber;
        existing.Volume = updated.Volume;
        existing.NumberOfVolumes = updated.NumberOfVolumes;
        existing.Edition = updated.Edition;
        existing.Place = updated.Place;
        existing.Publisher = updated.Publisher;
        existing.Date = updated.Date;
        existing.Pages = updated.Pages;
        existing.ISBN = updated.ISBN;
    }

    private static void UpdateDocumentFields(Document existing, Document updated)
    {
        if (updated == null)
        {
            throw new ArgumentNullException(nameof(updated), "Update Document cannot be null.");
        }
        
        existing.Publisher = updated.Publisher;
        existing.Date = updated.Date;
    }

    private static void UpdateJournalArticleFields(JournalArticle existing, JournalArticle updated)
    {
        if (updated == null)
        {
            throw new ArgumentNullException(nameof(updated), "Update Journal Article cannot be null.");
        }
        
        existing.PublicationTitle = updated.PublicationTitle;
        existing.Volume = updated.Volume;
        existing.Issue = updated.Issue;
        existing.Pages = updated.Pages;
        existing.Date = updated.Date;
        existing.SeriesTitle = updated.SeriesTitle;
        existing.Series = updated.Series;
        existing.SeriesText = updated.SeriesText;
        existing.JournalAbbreviation = updated.JournalAbbreviation;
        existing.DOI = updated.DOI;
        existing.ISSN = updated.ISSN;
    }

    private static void UpdateEncyclopediaArticleFields(EncyclopediaArticle existing, EncyclopediaArticle updated)
    {
        if (updated == null)
        {
            throw new ArgumentNullException(nameof(updated), "Update Encyclopedia Article cannot be null.");
        }
        
        existing.EncyclopediaTitle = updated.EncyclopediaTitle;
        existing.Series = updated.Series;
        existing.SeriesNumber = updated.SeriesNumber;
        existing.Volume = updated.Volume;
        existing.NumberOfVolumes = updated.NumberOfVolumes;
        existing.Edition = updated.Edition;
        existing.Place = updated.Place;
        existing.Publisher = updated.Publisher;
        existing.Date = updated.Date;
        existing.Pages = updated.Pages;
        existing.ISBN = updated.ISBN;
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