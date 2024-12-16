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
    public string? AudioRecordingFormat { get; set; } = "medium";
    public string? SeriesTitle { get; set; }
    public string? Volume { get; set; }
    public int? NumberOfVolumes { get; set; }
    public string? Place { get; set; }
    public string? Label { get; set; } = "publisher";
    public string? RunningTime { get; set; }

}

public class Bill : Reference
{
    public string? BillNumber { get; set; } = "number";
    public string? Code { get; set; }
    public string? CodeVolume { get; set; } = "volume";
    public string? Section { get; set; }
    public string? CodePages { get; set; } = "pages";
    public string? LegislativeBody { get; set; } = "authority";
    public string? Session { get; set; }
    public string? History { get; set; }


}

public class BlogPost : Reference
{
    public string? BlogTitle { get; set; } = "publicationTitle";
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
    public int? Pages { get; set; } = 0;
    [MaxLength(256)]

    public string? Edition { get; set; } = "";
    [MaxLength(256)]

    public string? Series { get; set; } = "";
    public int? SeriesNumber { get; set; } = 0;
    [MaxLength(256)]

    public string? SeriesTitle { get; set; } = "";
    public int? VolumeAmount { get; set; } = 0;
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
    public string? BookTitle { get; set; } = "publicationTitle";
    public string? Series { get; set; }
    public int? SeriesNumber { get; set; }
    public string? Volume { get; set; }
    public int? NumberOfVolumes { get; set; }
    public string? Edition { get; set; }
    public string? Place { get; set; }
    public string? Publisher { get; set; }
    public string? Date { get; set; }
    public string? Pages { get; set; }
    public string? ISBN { get; set; }
}

public class Case : Reference
{
    
}

public class ConferencePaper : Reference
{
    public string? ProceedingsTitle { get; set; } = "publicationTitle";
    public string? ConferenceName { get; set; }
    public string? Place { get; set; }
    public string? Publisher { get; set; }
    public string? Volume { get; set; }
    public string? Pages { get; set; }
    public string? Series { get; set; }
    public string? DOI { get; set; }
    public string? ISBN { get; set; }
}

public class DictionaryEntry : Reference
{
    public string? DictionaryTitle { get; set; } = "publicationTitle";
    public string? Series { get; set; }
    public int? SeriesNumber { get; set; }
    public string? Volume { get; set; }
    public string? NumberOfVolumes { get; set; }
    public string? Edition { get; set; }
    public string? Place { get; set; }
    public string? Publisher { get; set; }
    public string? Date { get; set; }
    public string? Pages { get; set; }
    public string? ISBN { get; set; }
}

public class Dataset : Reference
{
    public string? Identifier { get; set; } = "number";
    public string? DataType { get; set; }
    public string? VersionNumber { get; set; }
    public string? Date { get; set; }
    public string? Repository { get; set; } = "publisher";
    public string? RepositoryLocation { get; set; } = "place";
    public string? Format { get; set; } = "medium";
    public string? DOI { get; set; }
    public string? CitationKey { get; set; }
}

public class Document : Reference
{
    public string? Publisher { get; set; }
    public string? Date { get; set; }
}

public class Email : Reference
{
    public string? Subject { get; set; } = "title";
    public string? Date { get; set; }
}

public class EncyclopediaArticle : Reference
{
    public string? EncyclopediaTitle { get; set; } = "publicationTitle";
    public string? Series { get; set; }
    public string? SeriesNumber { get; set; }
    public string? Volume { get; set; }
    public string? NumberOfVolumes { get; set; }
    public string? Edition { get; set; }
    public string? Place { get; set; }
    public string? Publisher { get; set; }
    public string? Date { get; set; }
    public string? Pages { get; set; }
    public string? ISBN { get; set; }
}

public class Film : Reference
{
    public string? Distributor { get; set; } = "publisher";
    public string? Date { get; set; }
    public string? Genre { get; set; } = "type";
    public string? VideoRecordingFormat { get; set; } = "medium";
    public string? RunningTime { get; set; }
}

public class ForumPost : Reference
{
    public string? ForumTitle { get; set; } = "publicationTitle";
    public string? PostType { get; set; } = "type";
}

public class Hearing : Reference
{
    public string? Committee { get; set; }
    public string? Place { get; set; }
    public string? Publisher { get; set; }
    public string? NumberOfVolumes { get; set; }
    public string? DocumentNumber { get; set; } = "number";
    public string? Pages { get; set; }
    public string? LegislativeBody { get; set; } = "authority";
    public string? Session { get; set; }
    public string? History { get; set; }
}

public class InstantMessage : Reference
{
    
}

public class Interview : Reference
{
    public string? InterviewMedium { get; set; } = "medium";
}

public class JournalArticle : Reference
{
    public string? PublicationTitle { get; set; }
    public string? Volume { get; set; }
    public string? Issue { get; set; }
    public string? Pages { get; set; }
    public string? Date { get; set; }
    public string? Series { get; set; }
    public string? SeriesTitle { get; set; }
    public string? SeriesText { get; set; }
    public string? JournalAbbreviation { get; set; }
    public string? DOI { get; set; }
    public string? ISSN { get; set; }
}

public class Letter : Reference
{
    public string? LetterType { get; set; } = "type";
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
    public string? PublicationTitle { get; set; }
    public string? Volume { get; set; }
    public string? Issue { get; set; }
    public string? Date { get; set; }
    public string? Pages { get; set; }
    public string? ISSN { get; set; }
}

public class Manuscript : Reference
{
    public string? ManuscriptType { get; set; } = "type";
    public string? Place { get; set; }
    public string? Date { get; set; }
    public string? NumberOfPages { get; set; }
}

public class Map : Reference
{
    public string? MapType { get; set; } = "type";
    public string? Scale { get; set; }
    public string? SeriesTitle { get; set; }
    public string? Edition { get; set; }
    public string? Place { get; set; }
    public string? Publisher { get; set; }
    public string? Date { get; set; }
    public string? ISBN { get; set; }
}



public class NewspaperArticle : Reference
{
    public string? PublicationTitle { get; set; }
    public string? Place { get; set; }
    public string? Edition { get; set; }
    public string? Date { get; set; }
    public string? Section { get; set; }
    public string? Pages { get; set; }
    public string? ISSN { get; set; }
}

public class Podcast : Reference
{
    public string? SeriesTitle { get; set; }
    public string? EpisodeNumber { get; set; } = "number";
    public string? AudioFileType { get; set; } = "medium";
    public string? RunningTime { get; set; }
}

public class Presentation : Reference
{
    public string? PresentationType { get; set; }
    public string? Date { get; set; }
    public string? Place { get; set; }
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
    public string? ProgramTitle { get; set; } = "publicationTitle";
    public string? EpisodeNumber { get; set; } = "number";
    public string? AudioRecordingFormat { get; set; } = "medium";
    public string? Place { get; set; }
    public string? Network { get; set; } = "publisher";
    public string? Date { get; set; }
    public string? RunningTime { get; set; }
}

public class Software : Reference
{
    public string? VersionNumber { get; set; }
    public string? SeriesTitle { get; set; }
    public string? Date { get; set; }
    public string? System { get; set; }
    public string? Place { get; set; }
    public string? Company { get; set; } = "publisher";
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
    public string? ReportNumber { get; set; } = "number";
    public string? ReportType { get; set; } = "type";
    public string? SeriesTitle { get; set; }
    public string? Place { get; set; }
    public string? Institution { get; set; } = "publisher";
    public string? Date { get; set; }
    public string? Pages { get; set; }
}

public class Standard : Reference
{
    public string? Organization { get; set; } = "authority";
    public string? Committee { get; set; }
    public string? StandardType { get; set; }
    public string? Number { get; set; }
    public string? VersionNumber { get; set; }
    public string? Status { get; set; }
    public string? Date { get; set; }
    public string? Publisher { get; set; }
    public string? Place { get; set; }
    public string? DOI { get; set; }
    public string? CitationKey { get; set; }
    public string? NumberOfPages { get; set; }
}

public class Statute : Reference
{
    public string? NameOfAct { get; set; } = "title";
    public string? Code { get; set; }
    public string? CodeNumber { get; set; }
    public string? PublicLawNumber { get; set; } = "number";
    public string? DateEnacted { get; set; } = "date";
    public string? Pages { get; set; }
    public string? Section { get; set; }
    public string? Session { get; set; }
    public string? History { get; set; }
}

public class Thesis : Reference
{
    public string? ThesisType { get; set; } = "type";
    public string? University { get; set; } = "publisher";
    public string? Place { get; set; }
    public string? Date { get; set; }
    public string? NumberOfPages { get; set; }
}

public class TVBroadcast : Reference
{
    public string? ProgramTitle { get; set; } = "publicationTitle";
    public string? EpisodeNumber { get; set; } = "episodeNumber";
    public string? VideoRecordingFormat { get; set; } = "medium";
    public string? Place { get; set; }
    public string? Network { get; set; } = "publisher";
    public string? Date { get; set; }
    public string? RunningTime { get; set; }
}

public class VideoRecording : Reference
{
    public string? VideoRecordingFormat { get; set; } = "medium";
    public string? SeriesTitle { get; set; }
    public string? Volume { get; set; }
    public string? NumberOfVolumes { get; set; }
    public string? Place { get; set; }
    public string? Studio { get; set; } = "publisher";
    public string? Date { get; set; }
    public string? RunningTime { get; set; }
    public string? ISBN { get; set; }
}

public class Website : Reference
{
    public string? WebsiteTitle { get; set; } = "publicationTitle";
    public string? ForumTitle { get; set; } = "forumTitle";
    [MaxLength(256)]
    public string? PostType { get; set; } = "";
    public string? WebsiteType { get; set; } = "type";
    public string? Date { get; set; }
}

public class Attachment : Reference
{
    
}

public class Note : Reference
{
    
}