using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Melin.Server.Models.References;

// https://api.zotero.org/schema

/// <summary>
/// Covers Image, Artwork, and Maps
/// </summary>
public class Artwork : Reference
{
    [MaxLength(128)]
    public string Medium { get; set; } = "";
    
    [MaxLength(128)]
    public string Dimensions { get; set; } = "";
    
    [MaxLength(128)]
    public string? Scale { get; set; } = "";
    
    [MaxLength(128)]
    public string? MapType { get; set; } = "";
}

public class AudioRecording : Reference
{
    [MaxLength(512)]
    public string? AudioRecordingFormat { get; set; } = "medium";
    [MaxLength(512)]
    public string? SeriesTitle { get; set; }
    [MaxLength(512)]
    public string? Volume { get; set; }
    [Range(0, 99999, ErrorMessage = "Max number of volumes must be < than 100,000")]
    public int? NumberOfVolumes { get; set; }
    [MaxLength(512)]
    public string? Place { get; set; }
    [MaxLength(512)]
    public string? Label { get; set; } = "publisher";
    [MaxLength(512)]
    public string? RunningTime { get; set; }
}

public class Bill : Reference
{
    [MaxLength(512)]
    public string? BillNumber { get; set; } = "number";
    [MaxLength(512)]
    public string? Code { get; set; }
    [MaxLength(512)]
    public string? CodeVolume { get; set; } = "volume";
    [MaxLength(512)]
    public string? Section { get; set; }
    [MaxLength(512)]
    public string? CodePages { get; set; } = "pages";
    [MaxLength(512)]
    public string? LegislativeBody { get; set; } = "authority";
    [MaxLength(512)]
    public string? Session { get; set; }
    [MaxLength(512)]
    public string? History { get; set; }
}

public class BlogPost : Reference
{
    [MaxLength(512)]
    public string? BlogTitle { get; set; } = "publicationTitle";
    [MaxLength(512)]
    public string? WebsiteType { get; set; } = "type";
}

// Covers Books & Periodicals
public class Book : Reference
{
    [MaxLength(256)]
    public string? Publication { get; set; } = "";
    [MaxLength(256)]
    public string? BookTitle { get; set; } = "";
    [MaxLength(256)]
    public string? Volume { get; set; } = "";
    [MaxLength(256)]
    public string? Issue { get; set; } = "";
    [Range(1, 99999, ErrorMessage = "Max Pages are 99,999")]
    public int? Pages { get; set; } = 0;
    [MaxLength(256)]
    public string? Edition { get; set; } = "";
    [MaxLength(256)]
    public string? Series { get; set; } = "";
    [Range(1, 99999, ErrorMessage = "Max Series Number is 99,999")]
    public int? SeriesNumber { get; set; } = 0;
    [MaxLength(256)]
    public string? SeriesTitle { get; set; } = "";
    [Range(1, 99999, ErrorMessage = "Max Volume Amount is 99,999")]
    public int? VolumeAmount { get; set; } = 0;
    [Range(1, 99999, ErrorMessage = "Max Page Amount is 99,999")]
    public int? PageAmount { get; set; } = 0;
    [MaxLength(256)]
    public string? Section { get; set; } = "";
    [MaxLength(256)]
    public string? Place { get; set; } = "";
    [MaxLength(256)]
    public string? Publisher { get; set; } = "";
    [MaxLength(256)]
    public string? JournalAbbr { get; set; } = "";
    [MaxLength(256)]
    public string? ISBN { get; set; } = "";
    [MaxLength(256)]
    public string? ISSN { get; set; } = "";
}

public class BookSection : Reference
{
    [MaxLength(256)]
    public string? BookTitle { get; set; } = "publicationTitle";
    [MaxLength(256)]
    public string? Series { get; set; }
    [Range(1, 99999, ErrorMessage = "Max Series Number is 99,999")]
    public int? SeriesNumber { get; set; }
    [MaxLength(256)]
    public string? Volume { get; set; }
    [Range(1, 99999, ErrorMessage = "Max Number of Volumes is 99,999")]
    public int? NumberOfVolumes { get; set; }
    [MaxLength(256)]
    public string? Edition { get; set; }
    [MaxLength(256)]
    public string? Place { get; set; }
    [MaxLength(256)]
    public string? Publisher { get; set; }
    [MaxLength(256)]
    public string? Date { get; set; }
    [MaxLength(256)]
    public string? Pages { get; set; }
    [MaxLength(256)]
    public string? ISBN { get; set; }
}

public class Case : Reference
{
    
}

public class ConferencePaper : Reference
{
    [MaxLength(256)]
    public string? ProceedingsTitle { get; set; } = "publicationTitle";
    [MaxLength(256)]
    public string? ConferenceName { get; set; }
    [MaxLength(256)]
    public string? Place { get; set; }
    [MaxLength(256)]
    public string? Publisher { get; set; }
    [MaxLength(256)]
    public string? Volume { get; set; }
    [MaxLength(256)]
    public string? Pages { get; set; }
    [MaxLength(256)]
    public string? Series { get; set; }
    [MaxLength(256)]
    public string? DOI { get; set; }
    [MaxLength(256)]
    public string? ISBN { get; set; }
}

public class DictionaryEntry : Reference
{
    [MaxLength(256)]
    public string? DictionaryTitle { get; set; } = "publicationTitle";
    [MaxLength(256)]
    public string? Series { get; set; }
    [Range(1, 99999, ErrorMessage = "Max Series Number is 99,999")]
    public int? SeriesNumber { get; set; }
    [MaxLength(256)]
    public string? Volume { get; set; }
    [MaxLength(256)]
    public string? NumberOfVolumes { get; set; }
    [MaxLength(256)]
    public string? Edition { get; set; }
    [MaxLength(256)]
    public string? Place { get; set; }
    [MaxLength(256)]
    public string? Publisher { get; set; }
    [MaxLength(256)]
    public string? Date { get; set; }
    [MaxLength(256)]
    public string? Pages { get; set; }
    [MaxLength(256)]
    public string? ISBN { get; set; }
}

public class Dataset : Reference
{
    [MaxLength(512)]
    public string? Identifier { get; set; } = "number";
    [MaxLength(512)]
    public string? DataType { get; set; }
    [MaxLength(512)]
    public string? VersionNumber { get; set; }
    [MaxLength(512)]
    public string? Date { get; set; }
    [MaxLength(512)]
    public string? Repository { get; set; } = "publisher";
    [MaxLength(512)]
    public string? RepositoryLocation { get; set; } = "place";
    [MaxLength(512)]
    public string? Format { get; set; } = "medium";
    [MaxLength(512)]
    public string? DOI { get; set; }
    [MaxLength(512)]
    public string? CitationKey { get; set; }
}

public class Document : Reference
{
    [MaxLength(512)]
    public string? Publisher { get; set; }
    [MaxLength(512)]
    public string? Date { get; set; }
}

public class Email : Reference
{
    [MaxLength(512)]
    public string? Subject { get; set; } = "title";
    [MaxLength(512)]
    public string? Date { get; set; }
}

public class EncyclopediaArticle : Reference
{
    [MaxLength(512)]
    public string? EncyclopediaTitle { get; set; } = "publicationTitle";
    [MaxLength(512)]
    public string? Series { get; set; }
    [MaxLength(512)]
    public string? SeriesNumber { get; set; }
    [MaxLength(512)]
    public string? Volume { get; set; }
    [MaxLength(512)]
    public string? NumberOfVolumes { get; set; }
    [MaxLength(512)]
    public string? Edition { get; set; }
    [MaxLength(512)]
    public string? Place { get; set; }
    [MaxLength(512)]
    public string? Publisher { get; set; }
    [MaxLength(512)]
    public string? Date { get; set; }
    [MaxLength(512)]
    public string? Pages { get; set; }
    [MaxLength(512)]
    public string? ISBN { get; set; }
}

public class Film : Reference
{
    [MaxLength(512)]
    public string? Distributor { get; set; } = "publisher";
    [MaxLength(512)]
    public string? Date { get; set; }
    [MaxLength(512)]
    public string? Genre { get; set; } = "type";
    [MaxLength(512)]
    public string? VideoRecordingFormat { get; set; } = "medium";
    [MaxLength(512)]
    public string? RunningTime { get; set; }
}

public class ForumPost : Reference
{
    [MaxLength(512)]
    public string? ForumTitle { get; set; } = "publicationTitle";
    [MaxLength(512)]
    public string? PostType { get; set; } = "type";
}

public class Hearing : Reference
{
    [MaxLength(512)]
    public string? Committee { get; set; }
    [MaxLength(512)]
    public string? Place { get; set; }
    [MaxLength(512)]
    public string? Publisher { get; set; }
    [MaxLength(512)]
    public string? NumberOfVolumes { get; set; }
    [MaxLength(512)]
    public string? DocumentNumber { get; set; } = "number";
    [MaxLength(512)]
    public string? Pages { get; set; }
    [MaxLength(512)]
    public string? LegislativeBody { get; set; } = "authority";
    [MaxLength(512)]
    public string? Session { get; set; }
    [MaxLength(512)]
    public string? History { get; set; }
}

public class InstantMessage : Reference
{
    
}

public class Interview : Reference
{
    [MaxLength(512)]
    public string? InterviewMedium { get; set; } = "medium";
}

public class JournalArticle : Reference
{
    [MaxLength(512)]
    public string? PublicationTitle { get; set; }
    [MaxLength(512)]
    public string? Volume { get; set; }
    [MaxLength(512)]
    public string? Issue { get; set; }
    [MaxLength(512)]
    public string? Pages { get; set; }
    [MaxLength(512)]
    public string? Date { get; set; }
    [MaxLength(512)]
    public string? Series { get; set; }
    [MaxLength(512)]
    public string? SeriesTitle { get; set; }
    [MaxLength(512)]
    public string? SeriesText { get; set; }
    [MaxLength(512)]
    public string? JournalAbbreviation { get; set; }
    [MaxLength(512)]
    public string? DOI { get; set; }
    [MaxLength(512)]
    public string? ISSN { get; set; }
}

public class Letter : Reference
{
    [MaxLength(512)]
    public string? LetterType { get; set; } = "type";
    [MaxLength(512)]
    public string? Date { get; set; }
}

// Covers Legislation & Hearings
public class Legislation : Reference
{
    [MaxLength(256)]
    public string NameOfAct { get; set; } = "";
    [MaxLength(256)]
    public string BillNumber { get; set; } = "";
    [MaxLength(256)]
    public string Code { get; set; } = "";
    [MaxLength(256)]
    public string CodeVolume { get; set; } = "";
    [MaxLength(256)]
    public string CodeNumber { get; set; } = "";
    [MaxLength(256)]
    public string PublicLawNumber { get; set; } = "";
    public DateTime? DateEnacted { get; set; }
    [MaxLength(256)]
    public string? Section { get; set; } = "";
    [MaxLength(256)]
    public string? Committee { get; set; } = "";
    [MaxLength(256)]
    public string? DocumentNumber { get; set; } = "";
    [MaxLength(256)]
    public string? CodePages { get; set; } = "";
    [MaxLength(256)]
    public string? LegislativeBody { get; set; } = "";
    [MaxLength(256)]
    public string? Session { get; set; } = "";
    [MaxLength(256)]
    public string? History { get; set; } = "";
}

public class LegalCases : Reference
{
    [MaxLength(256)]
    public string? History { get; set; } = "";
    [MaxLength(256)]
    public string? CaseName { get; set; } = "";
    [MaxLength(256)]
    public string? Court { get; set; } = "";
    public DateTime? DateDecided { get; set; }
    [MaxLength(256)]
    public string? DocketNumber { get; set; } = "";
    [MaxLength(256)]
    public string? Reporter { get; set; } = "";
    [MaxLength(256)]
    public string? ReporterVolume { get; set; } = "";
    [MaxLength(256)]
    public string? FirstPage { get; set; } = "";
}

public class Patent : Reference
{
    [MaxLength(256)]
    public string Country { get; set; } = "United States of America";
    [MaxLength(256)]
    public string? Assignee { get; set; } = "";
    [MaxLength(256)]
    public string? IssuingAuthority { get; set; } = "";
    [MaxLength(256)]
    public string? PatentNumber { get; set; } = "";
    public DateTime? FilingDate { get; set; }
    public DateTime? IssueDate { get; set; }
    [MaxLength(256)]
    public string? ApplicationNumber { get; set; } = "";
    [MaxLength(256)]
    public string? PriorityNumber { get; set; } = "";
    public string[]? References { get; set; }
    [MaxLength(256)]
    public string? LegalStatus { get; set; } = "";

}

public class MagazineArticle : Reference
{
    [MaxLength(512)]
    public string? PublicationTitle { get; set; }
    [MaxLength(512)]
    public string? Volume { get; set; }
    [MaxLength(512)]
    public string? Issue { get; set; }
    [MaxLength(512)]
    public string? Date { get; set; }
    [MaxLength(512)]
    public string? Pages { get; set; }
    [MaxLength(512)]
    public string? ISSN { get; set; }
}

public class Manuscript : Reference
{
    [MaxLength(512)]
    public string? ManuscriptType { get; set; } = "type";
    [MaxLength(512)]
    public string? Place { get; set; }
    [MaxLength(512)]
    public string? Date { get; set; }
    [MaxLength(512)]
    public string? NumberOfPages { get; set; }
}

public class Map : Reference
{
    [MaxLength(512)]
    public string? MapType { get; set; } = "type";
    [MaxLength(512)]
    public string? Scale { get; set; }
    [MaxLength(512)]
    public string? SeriesTitle { get; set; }
    [MaxLength(512)]
    public string? Edition { get; set; }
    [MaxLength(512)]
    public string? Place { get; set; }
    [MaxLength(512)]
    public string? Publisher { get; set; }
    [MaxLength(512)]
    public string? Date { get; set; }
    [MaxLength(512)]
    public string? ISBN { get; set; }
}



public class NewspaperArticle : Reference
{
    [MaxLength(512)]
    public string? PublicationTitle { get; set; }
    [MaxLength(512)]
    public string? Place { get; set; }
    [MaxLength(512)]
    public string? Edition { get; set; }
    [MaxLength(512)]
    public string? Date { get; set; }
    [MaxLength(512)]
    public string? Section { get; set; }
    [MaxLength(512)]
    public string? Pages { get; set; }
    [MaxLength(512)]
    public string? ISSN { get; set; }
}

public class Podcast : Reference
{
    [MaxLength(512)]
    public string? SeriesTitle { get; set; }
    [MaxLength(512)]
    public string? EpisodeNumber { get; set; } = "number";
    [MaxLength(512)]
    public string? AudioFileType { get; set; } = "medium";
    [MaxLength(512)]
    public string? RunningTime { get; set; }
}

public class Presentation : Reference
{
    [MaxLength(512)]
    public string? PresentationType { get; set; }
    [MaxLength(512)]
    public string? Date { get; set; }
    [MaxLength(512)]
    public string? Place { get; set; }
    [MaxLength(512)]
    public string? MeetingName { get; set; }
}

// Covers Primary Sources & Personal Communications
public class PrimarySource : Reference
{
    [MaxLength(256)]
    public string Medium { get; set; } = "";
    [MaxLength(256)]
    public string PrimarySourceType { get; set; } = "";
    [MaxLength(256)]
    public string Subject { get; set; } = "";
}

public class RadioBroadcast : Reference
{
    [MaxLength(512)]
    public string? ProgramTitle { get; set; } = "publicationTitle";
    [MaxLength(512)]
    public string? EpisodeNumber { get; set; } = "number";
    [MaxLength(512)]
    public string? AudioRecordingFormat { get; set; } = "medium";
    [MaxLength(512)]
    public string? Place { get; set; }
    [MaxLength(512)]
    public string? Network { get; set; } = "publisher";
    [MaxLength(512)]
    public string? Date { get; set; }
    [MaxLength(512)]
    public string? RunningTime { get; set; }
}

public class Software : Reference
{
    [MaxLength(512)]
    public string? VersionNumber { get; set; }
    [MaxLength(512)]
    public string? SeriesTitle { get; set; }
    [MaxLength(512)]
    public string? Date { get; set; }
    [MaxLength(512)]
    public string? System { get; set; }
    [MaxLength(512)]
    public string? Place { get; set; }
    [MaxLength(512)]
    public string? Company { get; set; } = "publisher";
    [MaxLength(512)]
    public string? ProgrammingLanguage { get; set; }
}

public class Recording : Reference
{
    [MaxLength(256)]
    public string FileFormat { get; set; } = ""; // DVD, CD, MP3
    [MaxLength(256)]
    public string RunningTime { get; set; } = ""; // 120 mins
    [MaxLength(256)]
    public string ProgramTitle { get; set; } = "";
    [MaxLength(512)]
    public string? EpisodeNumber { get; set; } = "";
    [MaxLength(256)]
    public string Network { get; set; } = "";
    [MaxLength(256)]
    public string Label { get; set; } = "";
    [MaxLength(256)]
    public string Distributor { get; set; } = "";
    [MaxLength(256)]
    public string Genre { get; set; } = "";
    [MaxLength(256)]
    public string Studio { get; set; } = "";
}

public class Report : Reference
{
    [MaxLength(512)]
    public string? ReportNumber { get; set; } = "number";
    [MaxLength(512)]
    public string? ReportType { get; set; } = "type";
    [MaxLength(512)]
    public string? SeriesTitle { get; set; }
    [MaxLength(512)]
    public string? Place { get; set; }
    [MaxLength(512)]
    public string? Institution { get; set; } = "publisher";
    [MaxLength(512)]
    public string? Date { get; set; }
    [MaxLength(512)]
    public string? Pages { get; set; }
}

public class Standard : Reference
{
    [MaxLength(512)]
    public string? Organization { get; set; } = "authority";
    [MaxLength(512)]
    public string? Committee { get; set; }
    [MaxLength(512)]
    public string? StandardType { get; set; }
    [MaxLength(512)]
    public string? Number { get; set; }
    [MaxLength(512)]
    public string? VersionNumber { get; set; }
    [MaxLength(512)]
    public string? Status { get; set; }
    [MaxLength(512)]
    public string? Date { get; set; }
    [MaxLength(512)]
    public string? Publisher { get; set; }
    [MaxLength(512)]
    public string? Place { get; set; }
    [MaxLength(512)]
    public string? DOI { get; set; }
    [MaxLength(512)]
    public string? CitationKey { get; set; }
    [MaxLength(512)]
    public string? NumberOfPages { get; set; }
}

public class Statute : Reference
{
    [MaxLength(512)]
    public string? NameOfAct { get; set; } = "title";
    [MaxLength(512)]
    public string? Code { get; set; }
    [MaxLength(512)]
    public string? CodeNumber { get; set; }
    [MaxLength(512)]
    public string? PublicLawNumber { get; set; } = "number";
    [MaxLength(512)]
    public string? DateEnacted { get; set; } = "date";
    [MaxLength(512)]
    public string? Pages { get; set; }
    [MaxLength(512)]
    public string? Section { get; set; }
    [MaxLength(512)]
    public string? Session { get; set; }
    [MaxLength(512)]
    public string? History { get; set; }
}

public class Thesis : Reference
{
    [MaxLength(512)]
    public string? ThesisType { get; set; } = "type";
    [MaxLength(512)]
    public string? University { get; set; } = "publisher";
    [MaxLength(512)]
    public string? Place { get; set; }
    [MaxLength(512)]
    public string? Date { get; set; }
    [MaxLength(512)]
    public string? NumberOfPages { get; set; }
}

public class TVBroadcast : Reference
{
    [MaxLength(512)]
    public string? ProgramTitle { get; set; } = "publicationTitle";
    [MaxLength(512)]
    public string? EpisodeNumber { get; set; } = "episodeNumber";
    [MaxLength(512)]
    public string? VideoRecordingFormat { get; set; } = "medium";
    [MaxLength(512)]
    public string? Place { get; set; }
    [MaxLength(512)]
    public string? Network { get; set; } = "publisher";
    [MaxLength(512)]
    public string? Date { get; set; }
    [MaxLength(512)]
    public string? RunningTime { get; set; }
}

public class VideoRecording : Reference
{
    [MaxLength(512)]
    public string? VideoRecordingFormat { get; set; } = "medium";
    [MaxLength(512)]
    public string? SeriesTitle { get; set; }
    [MaxLength(512)]
    public string? Volume { get; set; }
    [MaxLength(512)]
    public string? NumberOfVolumes { get; set; }
    [MaxLength(512)]
    public string? Place { get; set; }
    [MaxLength(512)]
    public string? Studio { get; set; } = "publisher";
    [MaxLength(512)]
    public string? Date { get; set; }
    [MaxLength(512)]
    public string? RunningTime { get; set; }
    [MaxLength(512)]
    public string? ISBN { get; set; }
}

public class Website : Reference
{
    [MaxLength(512)]
    public string? WebsiteTitle { get; set; } = "publicationTitle";
    [MaxLength(512)]
    public string? ForumTitle { get; set; } = "forumTitle";
    [MaxLength(256)]
    public string? PostType { get; set; } = "";
    [MaxLength(512)]
    public string? WebsiteType { get; set; } = "type";
    [MaxLength(512)]
    public string? Date { get; set; }
}

public class Attachment : Reference
{
    
}

public class Note : Reference
{
    
}