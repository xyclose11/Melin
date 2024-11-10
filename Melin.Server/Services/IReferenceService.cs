using Melin.Server.Filter;
using Melin.Server.Models.References;
using Melin.Server.Wrappers;

namespace Melin.Server.Services;
using Melin.Server.Models;

public interface IReferenceService
{
    Task<Result<Reference>> GetReferenceByIdAsync(string userEmail, int id);
    Task<Result<Artwork>> GetArtworkByIdAsync(string userEmail, int id);
    Task<Result<AudioRecording>> GetAudioRecordingByIdAsync(string userEmail, int id);
    Task<Result<Bill>> GetBillByIdAsync(string userEmail, int id);
    Task<Result<BlogPost>> GetBlogPostByIdAsync(string userEmail, int id);
    Task<Result<Book>> GetBookByIdAsync(string userEmail, int id);
    Task<Result<BookSection>> GetBookSectionByIdAsync(string userEmail, int id);
    Task<Result<LegalCases>> GetCaseByIdAsync(string userEmail, int id);
    Task<Result<ConferencePaper>> GetConferencePaperByIdAsync(string userEmail, int id);
    Task<Result<DictionaryEntry>> GetDictionaryEntryByIdAsync(string userEmail, int id);
    Task<Result<Document>> GetDocumentByIdAsync(string userEmail, int id);
    Task<Result<Email>> GetEmailByIdAsync(string userEmail, int id);
    Task<Result<EncyclopediaArticle>> GetEncyclopediaArticleByIdAsync(string userEmail, int id);
    Task<Result<Film>> GetFilmByIdAsync(string userEmail, int id);
    Task<Result<ForumPost>> GetForumPostByIdAsync(string userEmail, int id);
    Task<Result<Hearing>> GetHearingByIdAsync(string userEmail, int id);
    Task<Result<InstantMessage>> GetInstantMessageByIdAsync(string userEmail, int id);
    Task<Result<Interview>> GetInterviewByIdAsync(string userEmail, int id);
    Task<Result<JournalArticle>> GetJournalArticleByIdAsync(string userEmail, int id);
    Task<Result<Letter>> GetLetterByIdAsync(string userEmail, int id);
    Task<Result<MagazineArticle>> GetMagazineArticleByIdAsync(string userEmail, int id);
    Task<Result<Manuscript>> GetManuscriptByIdAsync(string userEmail, int id);
    Task<Result<Map>> GetMapByIdAsync(string userEmail, int id);
    Task<Result<NewspaperArticle>> GetNewspaperArticleByIdAsync(string userEmail, int id);
    Task<Result<Patent>> GetPatentByIdAsync(string userEmail, int id);
    Task<Result<Podcast>> GetPodcastByIdAsync(string userEmail, int id);
    Task<Result<Presentation>> GetPresentationByIdAsync(string userEmail, int id);
    Task<Result<RadioBroadcast>> GetRadioBroadcastByIdAsync(string userEmail, int id);
    Task<Result<Report>> GetReportByIdAsync(string userEmail, int id);
    Task<Result<Software>> GetSoftwareByIdAsync(string userEmail, int id);
    Task<Result<Statute>> GetStatuteByIdAsync(string userEmail, int id);
    Task<Result<Thesis>> GetThesisByIdAsync(string userEmail, int id);
    Task<Result<TVBroadcast>> GetTVBroadcastByIdAsync(string userEmail, int id);
    Task<Result<VideoRecording>> GetVideoRecordingByIdAsync(string userEmail, int id);
    Task<Result<Website>> GetWebsiteByIdAsync(string userEmail, int id);

    Task<ICollection<Reference>> GetOwnedReferencesAsync(PaginationFilter paginationFilter, string userEmail);
    Task<bool> AddReferenceAsync(Reference newReference);
    Task<bool> AddArtworkAsync(Artwork newArtwork);
    Task<bool> AddBookAsync(Book newBook);

    Task<bool> UpdateReferenceAsync(string userEmail, int referenceId, Reference updatedReference);
    Task<bool> UpdateArtworkAsync(string userEmail, int referenceId, Artwork updatedArtwork);
    Task<bool> UpdateBookAsync(string userEmail, int referenceId, Book updatedBook);

    Task<bool> DeleteReferenceAsync(string userEmail, int referenceId);
    Task<bool> DeleteReferenceRangeAsync(string userEmail, List<int> referenceIds);

    Task<Reference> GetReferenceByIdAsync(int referenceId);
    Task<int> GetReferencesCountAsync();
    Task<bool> ReferenceExistsAsync(int referenceId);

    Task<int> GetOwnedReferenceCountAsync(string userEmail);


}