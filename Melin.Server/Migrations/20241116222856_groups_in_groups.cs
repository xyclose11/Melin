using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Melin.Server.Migrations
{
    /// <inheritdoc />
    public partial class groups_in_groups : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "WebsiteType",
                table: "Reference",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PostType",
                table: "Reference",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ForumTitle",
                table: "Reference",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Discriminator",
                table: "Reference",
                type: "character varying(21)",
                maxLength: 21,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(13)",
                oldMaxLength: 13);

            // migrationBuilder.AddColumn<string>(
            //     name: "AbstractNote",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);

            // migrationBuilder.AddColumn<string>(
            //     name: "Accessed",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);

            // migrationBuilder.AddColumn<string>(
            //     name: "Archive",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "ArchiveLocation",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);

            // migrationBuilder.AddColumn<string>(
            //     name: "AudioFileType",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);

            // migrationBuilder.AddColumn<string>(
            //     name: "AudioRecordingFormat",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "AudioRecording_Label",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "AudioRecording_Place",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "AudioRecording_RunningTime",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "AudioRecording_SeriesTitle",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "AudioRecording_Volume",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "BillNumber1",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "Bill_Code",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "Bill_CodePages",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "Bill_CodeVolume",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "Bill_History",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "Bill_LegislativeBody",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "Bill_Section",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "Bill_Session",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "BlogTitle",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "BookSection_BookTitle",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "BookSection_Edition",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "BookSection_ISBN",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<int>(
            //     name: "BookSection_NumberOfVolumes",
            //     table: "Reference",
            //     type: "integer",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "BookSection_Pages",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "BookSection_Place",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "BookSection_Publisher",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "BookSection_Series",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<int>(
            //     name: "BookSection_SeriesNumber",
            //     table: "Reference",
            //     type: "integer",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "BookSection_Volume",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);

            // migrationBuilder.AddColumn<string>(
            //     name: "CallNumber",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "ConferencePaper_ConferenceName",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "ConferencePaper_ISBN",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "ConferencePaper_Pages",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "ConferencePaper_Place",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "ConferencePaper_Publisher",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "ConferencePaper_Series",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "ConferencePaper_Volume",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "DOI",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "Date",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "DictionaryEntry_Date",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "DictionaryEntry_Edition",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "DictionaryEntry_ISBN",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "DictionaryEntry_NumberOfVolumes",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "DictionaryEntry_Pages",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "DictionaryEntry_Place",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "DictionaryEntry_Publisher",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "DictionaryEntry_Series",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<int>(
            //     name: "DictionaryEntry_SeriesNumber",
            //     table: "Reference",
            //     type: "integer",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "DictionaryEntry_Volume",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "DictionaryTitle",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "Document_Date",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "Document_Publisher",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "Email_Date",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "Email_Subject",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "EncyclopediaArticle_Date",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "EncyclopediaArticle_Edition",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "EncyclopediaArticle_ISBN",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "EncyclopediaArticle_NumberOfVolumes",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "EncyclopediaArticle_Pages",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "EncyclopediaArticle_Place",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "EncyclopediaArticle_Publisher",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "EncyclopediaArticle_Series",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "EncyclopediaArticle_SeriesNumber",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "EncyclopediaArticle_Volume",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "EncyclopediaTitle",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "Film_Date",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "Film_Distributor",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "Film_Genre",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "Film_RunningTime",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "Hearing_Committee",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "Hearing_DocumentNumber",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "Hearing_History",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "Hearing_LegislativeBody",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "Hearing_NumberOfVolumes",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "Hearing_Pages",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "Hearing_Place",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "Hearing_Publisher",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "Hearing_Session",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "InterviewMedium",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "JournalAbbreviation",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "JournalArticle_DOI",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "JournalArticle_Date",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "JournalArticle_ISSN",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "JournalArticle_Issue",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "JournalArticle_Pages",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "JournalArticle_Series",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "JournalArticle_SeriesTitle",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "JournalArticle_Volume",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "LetterType",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "Letter_Date",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "LibraryCatalog",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "LocationStored",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "MagazineArticle_Date",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "MagazineArticle_ISSN",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "MagazineArticle_Issue",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "MagazineArticle_Pages",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "MagazineArticle_PublicationTitle",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "MagazineArticle_Volume",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "ManuscriptType",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "Manuscript_Date",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "Manuscript_Place",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "MapType1",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "Map_Date",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "Map_Edition",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "Map_ISBN",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "Map_Place",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "Map_Publisher",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "Map_Scale",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "Map_SeriesTitle",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "NewspaperArticle_Date",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "NewspaperArticle_Edition",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "NewspaperArticle_ISSN",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "NewspaperArticle_Pages",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "NewspaperArticle_Place",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "NewspaperArticle_PublicationTitle",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "NewspaperArticle_Section",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "NumberOfPages",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<int>(
            //     name: "NumberOfVolumes",
            //     table: "Reference",
            //     type: "integer",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "Podcast_EpisodeNumber",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "Podcast_RunningTime",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "Podcast_SeriesTitle",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "ProceedingsTitle",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "PublicationTitle",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "RadioBroadcast_AudioRecordingFormat",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "RadioBroadcast_Date",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "RadioBroadcast_EpisodeNumber",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "RadioBroadcast_Network",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "RadioBroadcast_Place",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "RadioBroadcast_ProgramTitle",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "RadioBroadcast_RunningTime",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "SeriesText",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "Statute_Code",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "Statute_CodeNumber",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "Statute_DateEnacted",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "Statute_History",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "Statute_NameOfAct",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "Statute_Pages",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "Statute_PublicLawNumber",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "Statute_Section",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "Statute_Session",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "TVBroadcast_Date",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "TVBroadcast_EpisodeNumber",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "TVBroadcast_Network",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "TVBroadcast_Place",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "TVBroadcast_ProgramTitle",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "TVBroadcast_RunningTime",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "TVBroadcast_VideoRecordingFormat",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "ThesisType",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);

            // migrationBuilder.AddColumn<string>(
            //     name: "Thesis_Date",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "Thesis_NumberOfPages",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "Thesis_Place",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "URL",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "University",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "VideoRecordingFormat",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "VideoRecordingFormat1",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "VideoRecording_Date",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "VideoRecording_ISBN",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "VideoRecording_NumberOfVolumes",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "VideoRecording_Place",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "VideoRecording_RunningTime",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "VideoRecording_SeriesTitle",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "VideoRecording_Studio",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "VideoRecording_Volume",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "WebsiteType1",
            //     table: "Reference",
            //     type: "character varying(256)",
            //     maxLength: 256,
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "Website_ForumTitle",
            //     table: "Reference",
            //     type: "character varying(256)",
            //     maxLength: 256,
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "Website_PostType",
            //     table: "Reference",
            //     type: "character varying(256)",
            //     maxLength: 256,
            //     nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Group",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ReferenceId",
                table: "Creators",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_Group_GroupId",
                table: "Group",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Group_Group_GroupId",
                table: "Group",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Group_Group_GroupId",
                table: "Group");

            migrationBuilder.DropIndex(
                name: "IX_Group_GroupId",
                table: "Group");

            migrationBuilder.DropColumn(
                name: "AbstractNote",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Accessed",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Archive",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "ArchiveLocation",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "AudioFileType",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "AudioRecordingFormat",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "AudioRecording_Label",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "AudioRecording_Place",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "AudioRecording_RunningTime",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "AudioRecording_SeriesTitle",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "AudioRecording_Volume",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "BillNumber1",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Bill_Code",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Bill_CodePages",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Bill_CodeVolume",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Bill_History",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Bill_LegislativeBody",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Bill_Section",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Bill_Session",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "BlogTitle",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "BookSection_BookTitle",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "BookSection_Edition",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "BookSection_ISBN",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "BookSection_NumberOfVolumes",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "BookSection_Pages",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "BookSection_Place",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "BookSection_Publisher",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "BookSection_Series",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "BookSection_SeriesNumber",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "BookSection_Volume",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "CallNumber",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "ConferencePaper_ConferenceName",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "ConferencePaper_ISBN",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "ConferencePaper_Pages",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "ConferencePaper_Place",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "ConferencePaper_Publisher",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "ConferencePaper_Series",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "ConferencePaper_Volume",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "DOI",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "DictionaryEntry_Date",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "DictionaryEntry_Edition",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "DictionaryEntry_ISBN",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "DictionaryEntry_NumberOfVolumes",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "DictionaryEntry_Pages",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "DictionaryEntry_Place",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "DictionaryEntry_Publisher",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "DictionaryEntry_Series",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "DictionaryEntry_SeriesNumber",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "DictionaryEntry_Volume",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "DictionaryTitle",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Document_Date",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Document_Publisher",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Email_Date",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Email_Subject",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "EncyclopediaArticle_Date",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "EncyclopediaArticle_Edition",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "EncyclopediaArticle_ISBN",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "EncyclopediaArticle_NumberOfVolumes",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "EncyclopediaArticle_Pages",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "EncyclopediaArticle_Place",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "EncyclopediaArticle_Publisher",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "EncyclopediaArticle_Series",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "EncyclopediaArticle_SeriesNumber",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "EncyclopediaArticle_Volume",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "EncyclopediaTitle",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Film_Date",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Film_Distributor",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Film_Genre",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Film_RunningTime",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Hearing_Committee",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Hearing_DocumentNumber",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Hearing_History",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Hearing_LegislativeBody",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Hearing_NumberOfVolumes",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Hearing_Pages",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Hearing_Place",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Hearing_Publisher",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Hearing_Session",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "InterviewMedium",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "JournalAbbreviation",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "JournalArticle_DOI",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "JournalArticle_Date",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "JournalArticle_ISSN",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "JournalArticle_Issue",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "JournalArticle_Pages",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "JournalArticle_Series",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "JournalArticle_SeriesTitle",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "JournalArticle_Volume",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "LetterType",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Letter_Date",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "LibraryCatalog",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "LocationStored",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "MagazineArticle_Date",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "MagazineArticle_ISSN",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "MagazineArticle_Issue",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "MagazineArticle_Pages",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "MagazineArticle_PublicationTitle",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "MagazineArticle_Volume",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "ManuscriptType",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Manuscript_Date",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Manuscript_Place",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "MapType1",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Map_Date",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Map_Edition",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Map_ISBN",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Map_Place",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Map_Publisher",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Map_Scale",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Map_SeriesTitle",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "NewspaperArticle_Date",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "NewspaperArticle_Edition",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "NewspaperArticle_ISSN",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "NewspaperArticle_Pages",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "NewspaperArticle_Place",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "NewspaperArticle_PublicationTitle",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "NewspaperArticle_Section",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "NumberOfPages",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "NumberOfVolumes",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Podcast_EpisodeNumber",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Podcast_RunningTime",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Podcast_SeriesTitle",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "ProceedingsTitle",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "PublicationTitle",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "RadioBroadcast_AudioRecordingFormat",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "RadioBroadcast_Date",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "RadioBroadcast_EpisodeNumber",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "RadioBroadcast_Network",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "RadioBroadcast_Place",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "RadioBroadcast_ProgramTitle",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "RadioBroadcast_RunningTime",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "SeriesText",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Statute_Code",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Statute_CodeNumber",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Statute_DateEnacted",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Statute_History",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Statute_NameOfAct",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Statute_Pages",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Statute_PublicLawNumber",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Statute_Section",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Statute_Session",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "TVBroadcast_Date",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "TVBroadcast_EpisodeNumber",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "TVBroadcast_Network",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "TVBroadcast_Place",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "TVBroadcast_ProgramTitle",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "TVBroadcast_RunningTime",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "TVBroadcast_VideoRecordingFormat",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "ThesisType",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Thesis_Date",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Thesis_NumberOfPages",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Thesis_Place",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "URL",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "University",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "VideoRecordingFormat",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "VideoRecordingFormat1",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "VideoRecording_Date",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "VideoRecording_ISBN",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "VideoRecording_NumberOfVolumes",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "VideoRecording_Place",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "VideoRecording_RunningTime",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "VideoRecording_SeriesTitle",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "VideoRecording_Studio",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "VideoRecording_Volume",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "WebsiteType1",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Website_ForumTitle",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Website_PostType",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Group");

            migrationBuilder.AlterColumn<string>(
                name: "WebsiteType",
                table: "Reference",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PostType",
                table: "Reference",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ForumTitle",
                table: "Reference",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Discriminator",
                table: "Reference",
                type: "character varying(13)",
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(21)",
                oldMaxLength: 21);

            migrationBuilder.AlterColumn<int>(
                name: "ReferenceId",
                table: "Creators",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}
