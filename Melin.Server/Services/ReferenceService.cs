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
    

    public async Task<bool> UpdateReferenceAsync(string userEmail, int referenceId, Reference updatedReference)
    {
        throw new NotImplementedException();
    }
    
    private async void UpdateGeneralFields(Reference existingReference, Reference updatedReference)
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
            
            if (existingReference.DatePublished != null && !existingReference.DatePublished.Equals(updatedReference.DatePublished))
            {
                existingReference.DatePublished = updatedReference.DatePublished;
            }

            if (existingReference.Tags != null)
            {
                if (!existingReference.Tags.Equals(updatedReference.Tags))
                {
                    existingReference.Tags = updatedReference.Tags;
                }
            }

            await UpdateCreatorsAsync(existingReference, updatedReference);

            await _referenceRepository.UpdateReferenceAsync(existingReference);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private async Task UpdateCreatorsAsync(Reference existingReference, Reference updatedReference)
    {
        if (existingReference.Creators == null || updatedReference.Creators == null)
            return;

        // Replace existing creators if the existing list is empty
        if (existingReference.Creators.Count == 0)
        {
            existingReference.Creators = new List<Creator>(updatedReference.Creators);
        }
        else
        {
            // Create a dictionary for efficient lookup of updated creators by ID
            var updatedCreatorsMap = updatedReference.Creators.ToDictionary(c => c.Id);

            // Update or detach existing creators
            for (int i = 0; i < existingReference.Creators.Count; i++)
            {
                var existingCreator = existingReference.Creators.ToList()[i];

                if (updatedCreatorsMap.TryGetValue(existingCreator.Id, out var updatedCreator))
                {
                    // Update existing creator's properties
                    existingCreator.Types = updatedCreator.Types;
                    existingCreator.FirstName = updatedCreator.FirstName;
                    existingCreator.LastName = updatedCreator.LastName;

                    // Remove from the map to mark as processed
                    updatedCreatorsMap.Remove(existingCreator.Id);
                }
                else
                {
                    // Detach the creator if it doesn't exist in the updated list
                    existingCreator.ReferenceId = null;
                }
            }

            // Add new creators from the updated reference that are not already in the existing list
            foreach (var newCreator in updatedCreatorsMap.Values)
            {
                existingReference.Creators.Add(newCreator);
            }
        }

        // Persist changes asynchronously
        await _referenceRepository.UpdateCreatorsAsync(existingReference);
    }


    private static void UpdateArtworkFields(Artwork existingReference, Artwork updatedReference)
    {
        existingReference.Medium = updatedReference.Medium;
        existingReference.Dimensions = updatedReference.Dimensions;
        existingReference.Scale = updatedReference.Scale;
        existingReference.MapType = updatedReference.MapType;
    }
    
    private static void UpdateAudioRecordingFields(AudioRecording existing, AudioRecording updated)
    {
        existing.AudioRecordingFormat = updated.AudioRecordingFormat;
        existing.SeriesTitle = updated.SeriesTitle;
        existing.Volume = updated.Volume;
        existing.NumberOfVolumes = updated.NumberOfVolumes;
        existing.Place = updated.Place;
        existing.Label = updated.Label;
        existing.RunningTime = updated.RunningTime;
    }

    private static void UpdateBookFields(Book existing, Book updated)
    {
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
        existing.Publisher = updated.Publisher;
        existing.Date = updated.Date;
    }

    private static void UpdateJournalArticleFields(JournalArticle existing, JournalArticle updated)
    {
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