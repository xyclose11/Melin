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
    Task<bool> AddAudioRecordingAsync(AudioRecording audioRecording);
    Task<bool> AddBillAsync(Bill bill);
    Task<bool> AddBlogPostAsync(BlogPost blogPost);
    Task<bool> AddBookSectionAsync(BookSection bookSection);
    Task<bool> AddCaseAsync(LegalCases cases);
    Task<bool> AddConferencePaperAsync(ConferencePaper conferencePaper);
    Task<bool> AddDictionaryEntryAsync(DictionaryEntry dictionaryEntry);
    Task<bool> AddDocumentAsync(Document document);
    Task<bool> AddEmailAsync(Email email);
    Task<bool> AddEncyclopediaArticleAsync(EncyclopediaArticle encyclopediaArticle);
    Task<bool> AddFilmAsync(Film film);
    Task<bool> AddForumPostAsync(ForumPost forumPost);
    Task<bool> AddHearingAsync(Hearing hearing);
    Task<bool> AddInstantMessageAsync(InstantMessage instantMessage);
    Task<bool> AddInterviewAsync(Interview interview);
    Task<bool> AddJournalArticleAsync(JournalArticle journalArticle);
    Task<bool> AddLetterAsync(Letter letter);
    Task<bool> AddMagazineArticleAsync(MagazineArticle magazineArticle);
    Task<bool> AddPatentAsync(Patent patent);
    Task<bool> AddPodcastAsync(Podcast podcast);
    Task<bool> AddPresentationAsync(Presentation presentation);
    Task<bool> AddRadioBroadcastAsync(RadioBroadcast radioBroadcast);
    Task<bool> AddReportAsync(Report report);
    Task<bool> AddSoftwareAsync(Software software);
    Task<bool> AddStatuteAsync(Statute statute);
    Task<bool> AddThesisAsync(Thesis thesis);
    Task<bool> AddTVBroadcastAsync(TVBroadcast tvBroadcast);
    Task<bool> AddVideoRecordingAsync(VideoRecording videoRecording);
    Task<bool> AddWebpageAsync(Webpage webpage);
    Task<Result<bool>> UpdateReferenceAsync(string userEmail, int referenceId, Reference updatedReference);
    Task<Result<bool>> UpdateArtworkAsync(string userEmail, int referenceId, Artwork updatedArtwork);
    Task<Result<bool>> UpdateAudioRecordingAsync(string userEmail, int referenceId, AudioRecording audioRecording);

    Task<Result<bool>> UpdateBillAsync(string userEmail, int referenceId, Bill bill);
    Task<Result<bool>> UpdateBlogPostAsync(string userEmail, int referenceId, BlogPost blogPost);
    Task<Result<bool>> UpdateBookAsync(string userEmail, int referenceId, Book updatedBook);
    Task<Result<bool>> UpdateBookSectionAsync(string userEmail, int referenceId, BookSection updatedReference);
    Task<Result<bool>> UpdateCaseAsync(string userEmail, int referenceId, Case cCase);
    Task<Result<bool>> UpdateConferencePaperAsync(string userEmail, int referenceId, ConferencePaper conferencePaper);
    Task<Result<bool>> UpdateDictionaryEntryAsync(string userEmail, int referenceId, DictionaryEntry dictionaryEntry);
    Task<Result<bool>> UpdateDocumentAsync(string userEmail, int referenceId, Document document);
    Task<Result<bool>> UpdateEmailAsync(string userEmail, int referenceId, Email email);
    Task<Result<bool>> UpdateEncyclopediaArticleAsync(string userEmail, int referenceId, EncyclopediaArticle article);
    Task<Result<bool>> UpdateFilmAsync(string userEmail, int referenceId, Film film);
    Task<Result<bool>> UpdateForumPostAsync(string userEmail, int referenceId, ForumPost forumPost);
    Task<Result<bool>> UpdateHearingAsync(string userEmail, int referenceId, Hearing hearing);
    Task<Result<bool>> UpdateInstantMessageAsync(string userEmail, int referenceId, InstantMessage instantMessage);
    Task<Result<bool>> UpdateInterviewAsync(string userEmail, int referenceId, Interview interview);
    Task<Result<bool>> UpdateJournalArticleAsync(string userEmail, int referenceId, JournalArticle journalArticle);
    Task<Result<bool>> UpdateLetterAsync(string userEmail, int referenceId, Letter letter);
    Task<Result<bool>> UpdateMagazineArticleAsync(string userEmail, int referenceId, MagazineArticle magazineArticle);
    Task<Result<bool>> UpdateManuscriptAsync(string userEmail, int referenceId, Manuscript manuscript);
    Task<Result<bool>> UpdateMapAsync(string userEmail, int referenceId, Map map);
    Task<Result<bool>> UpdateNewspaperArticleAsync(string userEmail, int referenceId, NewspaperArticle newspaperArticle);
    Task<Result<bool>> UpdatePatentAsync(string userEmail, int referenceId, Patent patent);
    Task<Result<bool>> UpdatePodcastAsync(string userEmail, int referenceId, Podcast podcast);
    Task<Result<bool>> UpdatePresentationAsync(string userEmail, int referenceId, Presentation presentation);
    Task<Result<bool>> UpdateRadioBroadcastAsync(string userEmail, int referenceId, RadioBroadcast radioBroadcast);
    Task<Result<bool>> UpdateReportAsync(string userEmail, int referenceId, Report report);
    Task<Result<bool>> UpdateSoftwareAsync(string userEmail, int referenceId, Software software);
    Task<Result<bool>> UpdateStatuteAsync(string userEmail, int referenceId, Statute statute);
    Task<Result<bool>> UpdateThesisAsync(string userEmail, int referenceId, Thesis thesis);
    Task<Result<bool>> UpdateTVBroadcastAsync(string userEmail, int referenceId, TVBroadcast tvBroadcast);
    Task<Result<bool>> UpdateVideoRecordingAsync(string userEmail, int referenceId, VideoRecording videoRecording);
    Task<Result<bool>> UpdateWebpageAsync(string userEmail, int referenceId, Webpage webpage);
    
    Task<bool> DeleteReferenceAsync(string userEmail, int referenceId);
    Task<bool> DeleteReferenceRangeAsync(string userEmail, List<int> referenceIds);

    Task<Reference> GetReferenceByIdAsync(int referenceId);
    Task<int> GetReferencesCountAsync();
    Task<bool> ReferenceExistsAsync(int referenceId);

    Task<int> GetOwnedReferenceCountAsync(string userEmail);


}