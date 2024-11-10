using System.Linq.Expressions;
using Melin.Server.Filter;
using Melin.Server.Models.References;
using Melin.Server.Wrappers;

namespace Melin.Server.Models.Repository;

public interface IReferenceRepository : IGenericRepository<Reference>
{
    #region CRUD

        Task<Result<List<Reference>>> GetAllOwnedReferencesAsync(string userEmail);
        Task<Result<List<Reference>>> GetOwnedPaginatedReferencesAsync(PaginationFilter paginationFilter, string userEmail);

        Task<Result<Reference>> GetReferenceByIdAsync(string userEmail, int id);
        Task<Artwork> GetArtworkByIdAsync(string userEmail, int id);
        Task<AudioRecording> GetAudioRecordingByIdAsync(string userEmail, int id);
        Task<Bill> GetBillByIdAsync(string userEmail, int id);
        Task<BlogPost> GetBlogPostByIdAsync(string userEmail, int id);
        Task<Book> GetBookByIdAsync(string userEmail, int id);
        Task<BookSection> GetBookSectionByIdAsync(string userEmail, int id);
        Task<Case> GetCaseByIdAsync(string userEmail, int id);
        Task<ConferencePaper> GetConferencePaperByIdAsync(string userEmail, int id);
        Task<DictionaryEntry> GetDictionaryEntryByIdAsync(string userEmail, int id);
        Task<Document> GetDocumentByIdAsync(string userEmail, int id);
        Task<Email> GetEmailByIdAsync(string userEmail, int id);
        Task<EncyclopediaArticle> GetEncyclopediaArticleByIdAsync(string userEmail, int id);
        Task<Film> GetFilmByIdAsync(string userEmail, int id);
        Task<ForumPost> GetForumPostByIdAsync(string userEmail, int id);
        Task<Hearing> GetHearingByIdAsync(string userEmail, int id);
        Task<InstantMessage> GetInstantMessageByIdAsync(string userEmail, int id);
        Task<Interview> GetInterviewByIdAsync(string userEmail, int id);
        Task<JournalArticle> GetJournalArticleByIdAsync(string userEmail, int id);
        Task<Letter> GetLetterByIdAsync(string userEmail, int id);
        Task<MagazineArticle> GetMagazineArticleByIdAsync(string userEmail, int id);
        Task<Manuscript> GetManuscriptByIdAsync(string userEmail, int id);
        Task<Map> GetMapByIdAsync(string userEmail, int id);
        Task<NewspaperArticle> GetNewspaperArticleByIdAsync(string userEmail, int id);
        Task<Patent> GetPatentByIdAsync(string userEmail, int id);
        Task<Podcast> GetPodcastByIdAsync(string userEmail, int id);
        Task<Presentation> GetPresentationByIdAsync(string userEmail, int id);
        Task<RadioBroadcast> GetRadioBroadcastByIdAsync(string userEmail, int id);
        Task<Report> GetReportByIdAsync(string userEmail, int id);
        Task<Software> GetSoftwareByIdAsync(string userEmail, int id);
        Task<Statute> GetStatuteByIdAsync(string userEmail, int id);
        Task<Thesis> GetThesisByIdAsync(string userEmail, int id);
        Task<TVBroadcast> GetTVBroadcastByIdAsync(string userEmail, int id);
        Task<VideoRecording> GetVideoRecordingByIdAsync(string userEmail, int id);
        Task<Webpage> GetWebpageByIdAsync(string userEmail, int id);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reference"></param>
        /// <returns></returns>
        Task<bool> UpdateReferenceAsync(Reference reference);
        
        Task<bool> AddReferenceAsync(Reference reference);

        Task<bool> DeleteAsync(string userEmail, int id);
        Task<bool> DeleteRangeAsync(string userEmail, List<int> ids);

        #endregion

        Task<int> GetOwnedReferenceCount(string userEmail);

        Task<bool> DoesReferenceExist(int id);
}