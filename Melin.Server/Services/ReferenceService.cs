using Melin.Server.Filter;
using Melin.Server.Models;
using Melin.Server.Models.References;
using Melin.Server.Models.Repository;
using Melin.Server.Wrappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Presentation = Melin.Server.Models.Presentation;
using Report = Melin.Server.Models.Report;
using Software = Melin.Server.Models.Software;

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

    public async Task<Result<Artwork>> GetArtworkByIdAsync(string userEmail, int id)
    {
        try
        {
            var res = await _referenceRepository.GetArtworkByIdAsync(userEmail, id);
            return Result<Artwork>.SuccessResult(res);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result<Artwork>.FailureResult("Unable to get artwork by ID");
        }
    }

    public async Task<Result<AudioRecording>> GetAudioRecordingByIdAsync(string userEmail, int id)
    {
        try
        {
            var res = await _referenceRepository.GetAudioRecordingByIdAsync(userEmail, id);
            return Result<AudioRecording>.SuccessResult(res);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result<AudioRecording>.FailureResult("Unable to get Audio Recording by ID");
        }
    }

    public async Task<Result<Bill>> GetBillByIdAsync(string userEmail, int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<BlogPost>> GetBlogPostByIdAsync(string userEmail, int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<Book>> GetBookByIdAsync(string userEmail, int id)
    {
        try
        {
            var res = await _referenceRepository.GetBookByIdAsync(userEmail, id);
            return Result<Book>.SuccessResult(res);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result<Book>.FailureResult("Unable to get book by ID");
        }
    }

    public async Task<Result<BookSection>> GetBookSectionByIdAsync(string userEmail, int id)
    {
        try
        {
            var res = await _referenceRepository.GetBookSectionByIdAsync(userEmail, id);
            return Result<BookSection>.SuccessResult(res);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result<BookSection>.FailureResult("Unable to get book by ID");
        }
    }

    public async Task<Result<LegalCases>> GetCaseByIdAsync(string userEmail, int id)
    {
        try
        {
            var res = await _referenceRepository.GetCaseByIdAsync(userEmail, id);
            return Result<LegalCases>.SuccessResult(res);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result<LegalCases>.FailureResult("Unable to get book by ID");
        }
    }

    public async Task<Result<ConferencePaper>> GetConferencePaperByIdAsync(string userEmail, int id)
    {
        try
        {
            var res = await _referenceRepository.GetConferencePaperByIdAsync(userEmail, id);
            return Result<ConferencePaper>.SuccessResult(res);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result<ConferencePaper>.FailureResult("Unable to get book by ID");
        }
        
    }

    public async Task<Result<DictionaryEntry>> GetDictionaryEntryByIdAsync(string userEmail, int id)
    {
        try
        {
            var res = await _referenceRepository.GetDictionaryEntryByIdAsync(userEmail, id);
            return Result<DictionaryEntry>.SuccessResult(res);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result<DictionaryEntry>.FailureResult("Unable to get book by ID");
        }
    }

    public async Task<Result<Document>> GetDocumentByIdAsync(string userEmail, int id)
    {
        try
        {
            var res = await _referenceRepository.GetDocumentByIdAsync(userEmail, id);
            return Result<Document>.SuccessResult(res);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result<Document>.FailureResult("Unable to get book by ID");
        }
    }

    public async Task<Result<Email>> GetEmailByIdAsync(string userEmail, int id)
    {
        try
        {
            var res = await _referenceRepository.GetEmailByIdAsync(userEmail, id);
            return Result<Email>.SuccessResult(res);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result<Email>.FailureResult("Unable to get book by ID");
        }
    }

    public async Task<Result<EncyclopediaArticle>> GetEncyclopediaArticleByIdAsync(string userEmail, int id)
    {
        try
        {
            var res = await _referenceRepository.GetEncyclopediaArticleByIdAsync(userEmail, id);
            return Result<EncyclopediaArticle>.SuccessResult(res);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result<EncyclopediaArticle>.FailureResult("Unable to get book by ID");
        }
    }

    public async Task<Result<Film>> GetFilmByIdAsync(string userEmail, int id)
    {
        try
        {
            var res = await _referenceRepository.GetFilmByIdAsync(userEmail, id);
            return Result<Film>.SuccessResult(res);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result<Film>.FailureResult("Unable to get book by ID");
        }
    }

    public async Task<Result<ForumPost>> GetForumPostByIdAsync(string userEmail, int id)
    {
        try
        {
            var res = await _referenceRepository.GetForumPostByIdAsync(userEmail, id);
            return Result<ForumPost>.SuccessResult(res);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result<ForumPost>.FailureResult("Unable to get book by ID");
        }
    }

    public async Task<Result<Hearing>> GetHearingByIdAsync(string userEmail, int id)
    {
        try
        {
            var res = await _referenceRepository.GetHearingByIdAsync(userEmail, id);
            return Result<Hearing>.SuccessResult(res);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result<Hearing>.FailureResult("Unable to get book by ID");
        }
    }

    public async Task<Result<InstantMessage>> GetInstantMessageByIdAsync(string userEmail, int id)
    {
        try
        {
            var res = await _referenceRepository.GetInstantMessageByIdAsync(userEmail, id);
            return Result<InstantMessage>.SuccessResult(res);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result<InstantMessage>.FailureResult("Unable to get book by ID");
        }
    }

    public async Task<Result<Interview>> GetInterviewByIdAsync(string userEmail, int id)
    {
        try
        {
            var res = await _referenceRepository.GetInterviewByIdAsync(userEmail, id);
            return Result<Interview>.SuccessResult(res);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result<Interview>.FailureResult("Unable to get book by ID");
        }
    }

    public async Task<Result<JournalArticle>> GetJournalArticleByIdAsync(string userEmail, int id)
    {
        try
        {
            var res = await _referenceRepository.GetJournalArticleByIdAsync(userEmail, id);
            return Result<JournalArticle>.SuccessResult(res);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result<JournalArticle>.FailureResult("Unable to get book by ID");
        }
    }

    public async Task<Result<Letter>> GetLetterByIdAsync(string userEmail, int id)
    {
        try
        {
            var res = await _referenceRepository.GetLetterByIdAsync(userEmail, id);
            return Result<Letter>.SuccessResult(res);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result<Letter>.FailureResult("Unable to get book by ID");
        }
    }

    public async Task<Result<MagazineArticle>> GetMagazineArticleByIdAsync(string userEmail, int id)
    {
        try
        {
            var res = await _referenceRepository.GetMagazineArticleByIdAsync(userEmail, id);
            return Result<MagazineArticle>.SuccessResult(res);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result<MagazineArticle>.FailureResult("Unable to get book by ID");
        }
    }

    public async Task<Result<Manuscript>> GetManuscriptByIdAsync(string userEmail, int id)
    {
        try
        {
            var res = await _referenceRepository.GetManuscriptByIdAsync(userEmail, id);
            return Result<Manuscript>.SuccessResult(res);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result<Manuscript>.FailureResult("Unable to get book by ID");
        }
    }

    public async Task<Result<Map>> GetMapByIdAsync(string userEmail, int id)
    {
        try
        {
            var res = await _referenceRepository.GetMapByIdAsync(userEmail, id);
            return Result<Map>.SuccessResult(res);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result<Map>.FailureResult("Unable to get book by ID");
        }
    }

    public async Task<Result<NewspaperArticle>> GetNewspaperArticleByIdAsync(string userEmail, int id)
    {
        try
        {
            var res = await _referenceRepository.GetNewspaperArticleByIdAsync(userEmail, id);
            return Result<NewspaperArticle>.SuccessResult(res);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result<NewspaperArticle>.FailureResult("Unable to get book by ID");
        }
    }

    public async Task<Result<Patent>> GetPatentByIdAsync(string userEmail, int id)
    {
        try
        {
            var res = await _referenceRepository.GetPatentByIdAsync(userEmail, id);
            return Result<Patent>.SuccessResult(res);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result<Patent>.FailureResult("Unable to get book by ID");
        }
    }

    public async Task<Result<Podcast>> GetPodcastByIdAsync(string userEmail, int id)
    {
        try
        {
            var res = await _referenceRepository.GetPodcastByIdAsync(userEmail, id);
            return Result<Podcast>.SuccessResult(res);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result<Podcast>.FailureResult("Unable to get book by ID");
        }
    }

    public async Task<Result<Presentation>> GetPresentationByIdAsync(string userEmail, int id)
    {
        try
        {
            var res = await _referenceRepository.GetPresentationByIdAsync(userEmail, id);
            return Result<Presentation>.SuccessResult(res);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result<Presentation>.FailureResult("Unable to get book by ID");
        }
    }

    public async Task<Result<RadioBroadcast>> GetRadioBroadcastByIdAsync(string userEmail, int id)
    {
        try
        {
            var res = await _referenceRepository.GetRadioBroadcastByIdAsync(userEmail, id);
            return Result<RadioBroadcast>.SuccessResult(res);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result<RadioBroadcast>.FailureResult("Unable to get book by ID");
        }
    }

    public async Task<Result<Report>> GetReportByIdAsync(string userEmail, int id)
    {
        try
        {
            var res = await _referenceRepository.GetReportByIdAsync(userEmail, id);
            return Result<Report>.SuccessResult(res);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result<Report>.FailureResult("Unable to get book by ID");
        }
    }

    public async Task<Result<Software>> GetSoftwareByIdAsync(string userEmail, int id)
    {
        try
        {
            var res = await _referenceRepository.GetSoftwareByIdAsync(userEmail, id);
            return Result<Software>.SuccessResult(res);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result<Software>.FailureResult("Unable to get book by ID");
        }
    }

    public async Task<Result<Statute>> GetStatuteByIdAsync(string userEmail, int id)
    {
        try
        {
            var res = await _referenceRepository.GetStatuteByIdAsync(userEmail, id);
            return Result<Statute>.SuccessResult(res);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result<Statute>.FailureResult("Unable to get book by ID");
        }
    }

    public async Task<Result<Thesis>> GetThesisByIdAsync(string userEmail, int id)
    {
        try
        {
            var res = await _referenceRepository.GetThesisByIdAsync(userEmail, id);
            return Result<Thesis>.SuccessResult(res);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result<Thesis>.FailureResult("Unable to get book by ID");
        }
    }

    public async Task<Result<TVBroadcast>> GetTVBroadcastByIdAsync(string userEmail, int id)
    {
        try
        {
            var res = await _referenceRepository.GetTVBroadcastByIdAsync(userEmail, id);
            return Result<TVBroadcast>.SuccessResult(res);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result<TVBroadcast>.FailureResult("Unable to get book by ID");
        }
    }

    public async Task<Result<VideoRecording>> GetVideoRecordingByIdAsync(string userEmail, int id)
    {
        try
        {
            var res = await _referenceRepository.GetVideoRecordingByIdAsync(userEmail, id);
            return Result<VideoRecording>.SuccessResult(res);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result<VideoRecording>.FailureResult("Unable to get book by ID");
        }
    }

    public async Task<Result<Website>> GetWebsiteByIdAsync(string userEmail, int id)
    {
        try
        {
            var res = await _referenceRepository.GetWebsiteByIdAsync(userEmail, id);
            return Result<Website>.SuccessResult(res);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result<Website>.FailureResult("Unable to get book by ID");
        }
    }

    public async Task<ICollection<Reference>> GetOwnedReferencesAsync(PaginationFilter paginationFilter, string userEmail)
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
            var res = await GetArtworkByIdAsync(userEmail, artworkId);
            Artwork artwork;
            
            if (res.Success)
            {
                artwork = res.Data;
            }
            else
            {
                return false;
            }
            
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