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

    public async Task<bool> AddAudioRecordingAsync(AudioRecording audioRecording)
    {
        try
        {
            audioRecording.Language = Language.English;
            audioRecording.Type = ReferenceType.AudioRecording;
            return await _referenceRepository.AddAsync(audioRecording);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> AddBillAsync(Bill bill)
    {
        try
        {
            bill.Language = Language.English;
            bill.Type = ReferenceType.Bill;
            return await _referenceRepository.AddAsync(bill);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> AddBlogPostAsync(BlogPost blogPost)
    {
        try
        {
            blogPost.Language = Language.English;
            blogPost.Type = ReferenceType.BlogPost;
            return await _referenceRepository.AddAsync(blogPost);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> AddBookSectionAsync(BookSection bookSection)
    {
        try
        {
            bookSection.Language = Language.English;
            bookSection.Type = ReferenceType.BookSection;
            return await _referenceRepository.AddAsync(bookSection);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> AddCaseAsync(LegalCases cases)
    {
        try
        {
            cases.Language = Language.English;
            cases.Type = ReferenceType.Case;
            return await _referenceRepository.AddAsync(cases);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> AddConferencePaperAsync(ConferencePaper conferencePaper)
    {
        try
        {
            conferencePaper.Language = Language.English;
            conferencePaper.Type = ReferenceType.ConferencePaper;
            return await _referenceRepository.AddAsync(conferencePaper);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> AddDictionaryEntryAsync(DictionaryEntry dictionaryEntry)
    {
        try
        {
            dictionaryEntry.Language = Language.English;
            dictionaryEntry.Type = ReferenceType.DictionaryEntry;
            return await _referenceRepository.AddAsync(dictionaryEntry);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> AddDocumentAsync(Document document)
    {
        try
        {
            document.Language = Language.English;
            document.Type = ReferenceType.Document;
            return await _referenceRepository.AddAsync(document);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> AddEmailAsync(Email email)
    {
        try
        {
            email.Language = Language.English;
            email.Type = ReferenceType.Email;
            return await _referenceRepository.AddAsync(email);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> AddEncyclopediaArticleAsync(EncyclopediaArticle encyclopediaArticle)
    {
        try
        {
            encyclopediaArticle.Language = Language.English;
            encyclopediaArticle.Type = ReferenceType.EncyclopediaArticle;
            return await _referenceRepository.AddAsync(encyclopediaArticle);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> AddFilmAsync(Film film)
    {
        try
        {
            film.Language = Language.English;
            film.Type = ReferenceType.Film;
            return await _referenceRepository.AddAsync(film);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> AddForumPostAsync(ForumPost forumPost)
    {
        try
        {
            forumPost.Language = Language.English;
            forumPost.Type = ReferenceType.ForumPost;
            return await _referenceRepository.AddAsync(forumPost);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> AddHearingAsync(Hearing hearing)
    {
        try
        {
            hearing.Language = Language.English;
            hearing.Type = ReferenceType.Hearing;
            return await _referenceRepository.AddAsync(hearing);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> AddInstantMessageAsync(InstantMessage instantMessage)
    {
        try
        {
            instantMessage.Language = Language.English;
            instantMessage.Type = ReferenceType.InstantMessage;
            return await _referenceRepository.AddAsync(instantMessage);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> AddInterviewAsync(Interview interview)
    {
        try
        {
            interview.Language = Language.English;
            interview.Type = ReferenceType.Interview;
            return await _referenceRepository.AddAsync(interview);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> AddJournalArticleAsync(JournalArticle journalArticle)
    {
        try
        {
            journalArticle.Language = Language.English;
            journalArticle.Type = ReferenceType.JournalArticle;
            return await _referenceRepository.AddAsync(journalArticle);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> AddLetterAsync(Letter letter)
    {
        try
        {
            letter.Language = Language.English;
            letter.Type = ReferenceType.Letter;
            return await _referenceRepository.AddAsync(letter);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> AddMagazineArticleAsync(MagazineArticle magazineArticle)
    {
        try
        {
            magazineArticle.Language = Language.English;
            magazineArticle.Type = ReferenceType.MagazineArticle;
            return await _referenceRepository.AddAsync(magazineArticle);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> AddPatentAsync(Patent patent)
    {
        try
        {
            patent.Language = Language.English;
            patent.Type = ReferenceType.Patent;
            return await _referenceRepository.AddAsync(patent);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> AddPodcastAsync(Podcast podcast)
    {
        try
        {
            podcast.Language = Language.English;
            podcast.Type = ReferenceType.Podcast;
            return await _referenceRepository.AddAsync(podcast);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> AddPresentationAsync(Presentation presentation)
    {
        try
        {
            presentation.Language = Language.English;
            presentation.Type = ReferenceType.Presentation;
            return await _referenceRepository.AddAsync(presentation);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> AddRadioBroadcastAsync(RadioBroadcast radioBroadcast)
    {
        try
        {
            radioBroadcast.Language = Language.English;
            radioBroadcast.Type = ReferenceType.RadioBroadcast;
            return await _referenceRepository.AddAsync(radioBroadcast);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> AddReportAsync(Report report)
    {
        try
        {
            report.Language = Language.English;
            report.Type = ReferenceType.Report;
            return await _referenceRepository.AddAsync(report);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> AddSoftwareAsync(Software software)
    {
        try
        {
            software.Language = Language.English;
            software.Type = ReferenceType.Software;
            return await _referenceRepository.AddAsync(software);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> AddStatuteAsync(Statute statute)
    {
        try
        {
            statute.Language = Language.English;
            statute.Type = ReferenceType.Statute;
            return await _referenceRepository.AddAsync(statute);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> AddThesisAsync(Thesis thesis)
    {
        try
        {
            thesis.Language = Language.English;
            thesis.Type = ReferenceType.Thesis;
            return await _referenceRepository.AddAsync(thesis);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> AddTVBroadcastAsync(TVBroadcast tvBroadcast)
    {
        try
        {
            tvBroadcast.Language = Language.English;
            tvBroadcast.Type = ReferenceType.TVBroadcast;
            return await _referenceRepository.AddAsync(tvBroadcast);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> AddVideoRecordingAsync(VideoRecording videoRecording)
    {
        try
        {
            videoRecording.Language = Language.English;
            videoRecording.Type = ReferenceType.VideoRecording;
            return await _referenceRepository.AddAsync(videoRecording);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> AddWebpageAsync(Webpage webpage)
    {
        try
        {
            webpage.Language = Language.English;
            webpage.Type = ReferenceType.Webpage;
            return await _referenceRepository.AddAsync(webpage);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    async Task<Result<bool>> IReferenceService.UpdateReferenceAsync(string userEmail, int referenceId, Reference updatedReference)
    {
        throw new NotImplementedException();
    }

    async Task<Result<bool>> IReferenceService.UpdateArtworkAsync(string userEmail, int referenceId, Artwork updatedArtwork)
    {
        try
        {
            var res = await GetArtworkByIdAsync(userEmail, referenceId);
            Artwork artwork;
            
            if (res.Success)
            {
                artwork = res.Data;
            }
            else
            {
                return Result<bool>.FailureResult("Failed to update artwork");
            }
            
            await UpdateGeneralFields( artwork,updatedArtwork);
            
            if (!updatedArtwork.Medium.Equals(artwork.Medium))
            {
                artwork.Medium = updatedArtwork.Medium;
            }

            if (updatedArtwork.MapType != null)
            {
                if (!updatedArtwork.MapType.Equals(artwork.MapType))
                {
                    artwork.MapType = updatedArtwork.MapType;
                }
            }
            
            if (!updatedArtwork.Dimensions.Equals(artwork.Dimensions))
            {
                artwork.Dimensions = updatedArtwork.Dimensions;
            }

            if (updatedArtwork.Scale != null)
            {
                if (!updatedArtwork.Scale.Equals(artwork.Scale))
                {
                    artwork.Scale = updatedArtwork.Scale;
                }
            }
            
            artwork.UpdatedAt = DateTime.UtcNow;

            await _referenceRepository.UpdateReferenceAsync(artwork);

            return Result<bool>.SuccessResult(true);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result<bool>.FailureResult("Failed to update artwork");
            throw;
        }
    }

    public async Task<Result<bool>> UpdateAudioRecordingAsync(string userEmail, int referenceId, AudioRecording audioRecording)
    {
        try
        {
            var res = await GetAudioRecordingByIdAsync(userEmail, referenceId);
            AudioRecording originalAudio;
            
            if (res.Success)
            {
                originalAudio = res.Data;
            }
            else
            {
                return Result<bool>.FailureResult("Failed to update artwork");
            }
            
            await UpdateGeneralFields( originalAudio,audioRecording);

            if (audioRecording.AudioRecordingFormat != null)
            {
                if (!audioRecording.AudioRecordingFormat.Equals(originalAudio.AudioRecordingFormat))
                {
                    audioRecording.AudioRecordingFormat = originalAudio.AudioRecordingFormat;
                }
            }

            if (audioRecording.SeriesTitle != null)
            {
                if (!audioRecording.SeriesTitle.Equals(originalAudio.SeriesTitle))
                {
                    audioRecording.SeriesTitle = originalAudio.SeriesTitle;
                }
            }

            if (audioRecording.Volume != null)
            {
                if (!audioRecording.Volume.Equals(originalAudio.Volume))
                {
                    audioRecording.Volume = originalAudio.Volume;
                }
            }
            
            if (audioRecording.NumberOfVolumes != null)
            {
                if (!audioRecording.NumberOfVolumes.Equals(originalAudio.NumberOfVolumes))
                {
                    audioRecording.NumberOfVolumes = originalAudio.NumberOfVolumes;
                }
            }
            
            if (audioRecording.Place != null)
            {
                if (!audioRecording.Place.Equals(originalAudio.Place))
                {
                    audioRecording.Place = originalAudio.Place;
                }
            }
            
            if (audioRecording.Label != null)
            {
                if (!audioRecording.Label.Equals(originalAudio.Label))
                {
                    audioRecording.Label = originalAudio.Label;
                }
            }
            
            if (audioRecording.RunningTime != null)
            {
                if (!audioRecording.RunningTime.Equals(originalAudio.RunningTime))
                {
                    audioRecording.RunningTime = originalAudio.RunningTime;
                }
            }
            
            
            audioRecording.UpdatedAt = DateTime.UtcNow;

            await _referenceRepository.UpdateReferenceAsync(audioRecording);

            return Result<bool>.SuccessResult(true);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<bool>> UpdateBillAsync(string userEmail, int referenceId, Bill bill)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<bool>> UpdateBlogPostAsync(string userEmail, int referenceId, BlogPost blogPost)
    {
        throw new NotImplementedException();
    }

    async Task<Result<bool>> IReferenceService.UpdateBookAsync(string userEmail, int referenceId, Book updatedBook)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<bool>> UpdateBookSectionAsync(string userEmail, int referenceId, BookSection updatedReference)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<bool>> UpdateCaseAsync(string userEmail, int referenceId, Case cCase)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<bool>> UpdateConferencePaperAsync(string userEmail, int referenceId, ConferencePaper conferencePaper)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<bool>> UpdateDictionaryEntryAsync(string userEmail, int referenceId, DictionaryEntry dictionaryEntry)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<bool>> UpdateDocumentAsync(string userEmail, int referenceId, Document document)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<bool>> UpdateEmailAsync(string userEmail, int referenceId, Email email)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<bool>> UpdateEncyclopediaArticleAsync(string userEmail, int referenceId, EncyclopediaArticle article)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<bool>> UpdateFilmAsync(string userEmail, int referenceId, Film film)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<bool>> UpdateForumPostAsync(string userEmail, int referenceId, ForumPost forumPost)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<bool>> UpdateHearingAsync(string userEmail, int referenceId, Hearing hearing)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<bool>> UpdateInstantMessageAsync(string userEmail, int referenceId, InstantMessage instantMessage)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<bool>> UpdateInterviewAsync(string userEmail, int referenceId, Interview interview)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<bool>> UpdateJournalArticleAsync(string userEmail, int referenceId, JournalArticle journalArticle)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<bool>> UpdateLetterAsync(string userEmail, int referenceId, Letter letter)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<bool>> UpdateMagazineArticleAsync(string userEmail, int referenceId, MagazineArticle magazineArticle)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<bool>> UpdateManuscriptAsync(string userEmail, int referenceId, Manuscript manuscript)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<bool>> UpdateMapAsync(string userEmail, int referenceId, Map map)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<bool>> UpdateNewspaperArticleAsync(string userEmail, int referenceId, NewspaperArticle newspaperArticle)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<bool>> UpdatePatentAsync(string userEmail, int referenceId, Patent patent)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<bool>> UpdatePodcastAsync(string userEmail, int referenceId, Podcast podcast)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<bool>> UpdatePresentationAsync(string userEmail, int referenceId, Presentation presentation)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<bool>> UpdateRadioBroadcastAsync(string userEmail, int referenceId, RadioBroadcast radioBroadcast)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<bool>> UpdateReportAsync(string userEmail, int referenceId, Report report)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<bool>> UpdateSoftwareAsync(string userEmail, int referenceId, Software software)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<bool>> UpdateStatuteAsync(string userEmail, int referenceId, Statute statute)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<bool>> UpdateThesisAsync(string userEmail, int referenceId, Thesis thesis)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<bool>> UpdateTVBroadcastAsync(string userEmail, int referenceId, TVBroadcast tvBroadcast)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<bool>> UpdateVideoRecordingAsync(string userEmail, int referenceId, VideoRecording videoRecording)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<bool>> UpdateWebpageAsync(string userEmail, int referenceId, Webpage webpage)
    {
        throw new NotImplementedException();
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

            await _referenceRepository.UpdateReferenceAsync(prevReference);

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