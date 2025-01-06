using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Melin.Server.Migrations
{
    /// <inheritdoc />
    public partial class DesktopFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.DropColumn(
            //     name: "ProceedingTitle",
            //     table: "Reference");
            //
            // migrationBuilder.RenameColumn(
            //     name: "RadioBroadcast_ProgramTitle",
            //     table: "Reference",
            //     newName: "Recording_ProgramTitle");
            //
            // migrationBuilder.RenameColumn(
            //     name: "RadioBroadcast_Network",
            //     table: "Reference",
            //     newName: "Recording_Network");
            //
            // migrationBuilder.RenameColumn(
            //     name: "Podcast_EpisodeNumber",
            //     table: "Reference",
            //     newName: "Recording_EpisodeNumber");
            //
            // migrationBuilder.RenameColumn(
            //     name: "Hearing_DocumentNumber",
            //     table: "Reference",
            //     newName: "Legislation_DocumentNumber");
            //
            // migrationBuilder.RenameColumn(
            //     name: "Hearing_Committee",
            //     table: "Reference",
            //     newName: "Legislation_Committee");
            //
            // migrationBuilder.RenameColumn(
            //     name: "Film_Genre",
            //     table: "Reference",
            //     newName: "Recording_Genre");
            //
            // migrationBuilder.RenameColumn(
            //     name: "Film_Distributor",
            //     table: "Reference",
            //     newName: "Recording_Distributor");
            //
            // migrationBuilder.RenameColumn(
            //     name: "Email_Subject",
            //     table: "Reference",
            //     newName: "PrimarySource_Subject");
            //
            // migrationBuilder.RenameColumn(
            //     name: "Bill_Session",
            //     table: "Reference",
            //     newName: "Legislation_Session");
            //
            // migrationBuilder.RenameColumn(
            //     name: "Bill_Section",
            //     table: "Reference",
            //     newName: "Legislation_Section");
            //
            // migrationBuilder.RenameColumn(
            //     name: "Bill_LegislativeBody",
            //     table: "Reference",
            //     newName: "Legislation_LegislativeBody");
            //
            // migrationBuilder.RenameColumn(
            //     name: "Bill_History",
            //     table: "Reference",
            //     newName: "Legislation_History");
            //
            // migrationBuilder.RenameColumn(
            //     name: "Bill_CodeVolume",
            //     table: "Reference",
            //     newName: "Legislation_CodeVolume");
            //
            // migrationBuilder.RenameColumn(
            //     name: "Bill_CodePages",
            //     table: "Reference",
            //     newName: "Legislation_CodePages");
            //
            // migrationBuilder.RenameColumn(
            //     name: "Bill_Code",
            //     table: "Reference",
            //     newName: "Legislation_Code");
            //
            // migrationBuilder.RenameColumn(
            //     name: "BillNumber1",
            //     table: "Reference",
            //     newName: "Legislation_BillNumber");
            //
            // migrationBuilder.RenameColumn(
            //     name: "AudioRecording_Volume",
            //     table: "Reference",
            //     newName: "Book_Volume");
            //
            // migrationBuilder.RenameColumn(
            //     name: "AudioRecording_SeriesTitle",
            //     table: "Reference",
            //     newName: "Software_SeriesTitle");
            //
            // migrationBuilder.RenameColumn(
            //     name: "AudioRecording_RunningTime",
            //     table: "Reference",
            //     newName: "Recording_RunningTime");
            //
            // migrationBuilder.RenameColumn(
            //     name: "AudioRecording_Place",
            //     table: "Reference",
            //     newName: "Software_Place");
            //
            // migrationBuilder.RenameColumn(
            //     name: "AudioRecording_Label",
            //     table: "Reference",
            //     newName: "Recording_Label");
            //
            // migrationBuilder.RenameColumn(
            //     name: "Artwork_Medium",
            //     table: "Reference",
            //     newName: "PrimarySource_Medium");
            //
            // migrationBuilder.RenameColumn(
            //     name: "Version",
            //     table: "Reference",
            //     newName: "Book_SeriesTitle");
            //
            // migrationBuilder.RenameColumn(
            //     name: "ConferencePaper_ConferenceName",
            //     table: "Reference",
            //     newName: "Website_Date");
            //
            // migrationBuilder.AlterColumn<string>(
            //     name: "Website_ForumTitle",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true,
            //     oldClrType: typeof(string),
            //     oldType: "character varying(256)",
            //     oldMaxLength: 256,
            //     oldNullable: true);
            //
            // migrationBuilder.AlterColumn<string>(
            //     name: "WebsiteType1",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true,
            //     oldClrType: typeof(string),
            //     oldType: "character varying(256)",
            //     oldMaxLength: 256,
            //     oldNullable: true);
            //
            // migrationBuilder.AlterColumn<string>(
            //     name: "WebsiteTitle",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true,
            //     oldClrType: typeof(string),
            //     oldType: "character varying(256)",
            //     oldMaxLength: 256,
            //     oldNullable: true);
            //
            // migrationBuilder.AlterColumn<string>(
            //     name: "Volume",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true,
            //     oldClrType: typeof(string),
            //     oldType: "character varying(256)",
            //     oldMaxLength: 256,
            //     oldNullable: true);
            //
            // migrationBuilder.AlterColumn<string>(
            //     name: "System",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true,
            //     oldClrType: typeof(string),
            //     oldType: "character varying(256)",
            //     oldMaxLength: 256,
            //     oldNullable: true);
            //
            // migrationBuilder.AlterColumn<string>(
            //     name: "Subject",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true,
            //     oldClrType: typeof(string),
            //     oldType: "character varying(256)",
            //     oldMaxLength: 256,
            //     oldNullable: true);
            //
            // migrationBuilder.AlterColumn<string>(
            //     name: "Session",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true,
            //     oldClrType: typeof(string),
            //     oldType: "character varying(256)",
            //     oldMaxLength: 256,
            //     oldNullable: true);
            //
            // migrationBuilder.AlterColumn<string>(
            //     name: "SeriesTitle",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true,
            //     oldClrType: typeof(string),
            //     oldType: "character varying(256)",
            //     oldMaxLength: 256,
            //     oldNullable: true);
            //
            // migrationBuilder.AlterColumn<string>(
            //     name: "Section",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true,
            //     oldClrType: typeof(string),
            //     oldType: "character varying(256)",
            //     oldMaxLength: 256,
            //     oldNullable: true);
            //
            // migrationBuilder.AlterColumn<string>(
            //     name: "RunningTime",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true,
            //     oldClrType: typeof(string),
            //     oldType: "character varying(256)",
            //     oldMaxLength: 256,
            //     oldNullable: true);
            //
            // migrationBuilder.AlterColumn<string>(
            //     name: "ReportType",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true,
            //     oldClrType: typeof(string),
            //     oldType: "character varying(256)",
            //     oldMaxLength: 256,
            //     oldNullable: true);
            //
            // migrationBuilder.AlterColumn<string>(
            //     name: "ReportNumber",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true,
            //     oldClrType: typeof(int),
            //     oldType: "integer",
            //     oldNullable: true);
            //
            // migrationBuilder.AlterColumn<string>(
            //     name: "ProgrammingLanguage",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true,
            //     oldClrType: typeof(string),
            //     oldType: "character varying(256)",
            //     oldMaxLength: 256,
            //     oldNullable: true);
            //
            // migrationBuilder.AlterColumn<string>(
            //     name: "ProgramTitle",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true,
            //     oldClrType: typeof(string),
            //     oldType: "character varying(256)",
            //     oldMaxLength: 256,
            //     oldNullable: true);
            //
            // migrationBuilder.AlterColumn<string>(
            //     name: "PresentationType",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true,
            //     oldClrType: typeof(string),
            //     oldType: "character varying(256)",
            //     oldMaxLength: 256,
            //     oldNullable: true);
            //
            // migrationBuilder.AlterColumn<string>(
            //     name: "Place",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true,
            //     oldClrType: typeof(string),
            //     oldType: "character varying(256)",
            //     oldMaxLength: 256,
            //     oldNullable: true);
            //
            // migrationBuilder.AlterColumn<string>(
            //     name: "Network",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true,
            //     oldClrType: typeof(string),
            //     oldType: "character varying(256)",
            //     oldMaxLength: 256,
            //     oldNullable: true);
            //
            // migrationBuilder.AlterColumn<string>(
            //     name: "Medium",
            //     table: "Reference",
            //     type: "character varying(128)",
            //     maxLength: 128,
            //     nullable: true,
            //     oldClrType: typeof(string),
            //     oldType: "character varying(256)",
            //     oldMaxLength: 256,
            //     oldNullable: true);
            //
            // migrationBuilder.AlterColumn<string>(
            //     name: "LegislativeBody",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true,
            //     oldClrType: typeof(string),
            //     oldType: "character varying(256)",
            //     oldMaxLength: 256,
            //     oldNullable: true);
            //
            // migrationBuilder.AlterColumn<string>(
            //     name: "Label",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true,
            //     oldClrType: typeof(string),
            //     oldType: "character varying(256)",
            //     oldMaxLength: 256,
            //     oldNullable: true);
            //
            // migrationBuilder.AlterColumn<string>(
            //     name: "Institution",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true,
            //     oldClrType: typeof(string),
            //     oldType: "character varying(256)",
            //     oldMaxLength: 256,
            //     oldNullable: true);
            //
            // migrationBuilder.AlterColumn<string>(
            //     name: "History",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true,
            //     oldClrType: typeof(string),
            //     oldType: "character varying(256)",
            //     oldMaxLength: 256,
            //     oldNullable: true);
            //
            // migrationBuilder.AlterColumn<string>(
            //     name: "Genre",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true,
            //     oldClrType: typeof(string),
            //     oldType: "character varying(256)",
            //     oldMaxLength: 256,
            //     oldNullable: true);
            //
            // migrationBuilder.AlterColumn<string>(
            //     name: "EpisodeNumber",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true,
            //     oldClrType: typeof(int),
            //     oldType: "integer",
            //     oldNullable: true);
            //
            // migrationBuilder.AlterColumn<string>(
            //     name: "DocumentNumber",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true,
            //     oldClrType: typeof(string),
            //     oldType: "character varying(256)",
            //     oldMaxLength: 256,
            //     oldNullable: true);
            //
            // migrationBuilder.AlterColumn<string>(
            //     name: "Distributor",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true,
            //     oldClrType: typeof(string),
            //     oldType: "character varying(256)",
            //     oldMaxLength: 256,
            //     oldNullable: true);
            //
            // migrationBuilder.AlterColumn<string>(
            //     name: "ConferenceName",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true,
            //     oldClrType: typeof(string),
            //     oldType: "character varying(256)",
            //     oldMaxLength: 256,
            //     oldNullable: true);
            //
            // migrationBuilder.AlterColumn<string>(
            //     name: "Company",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true,
            //     oldClrType: typeof(string),
            //     oldType: "character varying(256)",
            //     oldMaxLength: 256,
            //     oldNullable: true);
            //
            // migrationBuilder.AlterColumn<string>(
            //     name: "Committee",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true,
            //     oldClrType: typeof(string),
            //     oldType: "character varying(256)",
            //     oldMaxLength: 256,
            //     oldNullable: true);
            //
            // migrationBuilder.AlterColumn<string>(
            //     name: "CodeVolume",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true,
            //     oldClrType: typeof(string),
            //     oldType: "character varying(256)",
            //     oldMaxLength: 256,
            //     oldNullable: true);
            //
            // migrationBuilder.AlterColumn<string>(
            //     name: "CodePages",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true,
            //     oldClrType: typeof(string),
            //     oldType: "character varying(256)",
            //     oldMaxLength: 256,
            //     oldNullable: true);
            //
            // migrationBuilder.AlterColumn<string>(
            //     name: "Code",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true,
            //     oldClrType: typeof(string),
            //     oldType: "character varying(256)",
            //     oldMaxLength: 256,
            //     oldNullable: true);
            //
            // migrationBuilder.AlterColumn<string>(
            //     name: "BillNumber",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true,
            //     oldClrType: typeof(string),
            //     oldType: "character varying(256)",
            //     oldMaxLength: 256,
            //     oldNullable: true);
            //
            // migrationBuilder.AlterColumn<string>(
            //     name: "Recording_ProgramTitle",
            //     table: "Reference",
            //     type: "character varying(256)",
            //     maxLength: 256,
            //     nullable: true,
            //     oldClrType: typeof(string),
            //     oldType: "text",
            //     oldNullable: true);
            //
            // migrationBuilder.AlterColumn<string>(
            //     name: "Recording_Network",
            //     table: "Reference",
            //     type: "character varying(256)",
            //     maxLength: 256,
            //     nullable: true,
            //     oldClrType: typeof(string),
            //     oldType: "text",
            //     oldNullable: true);
            //
            // migrationBuilder.AlterColumn<string>(
            //     name: "Legislation_DocumentNumber",
            //     table: "Reference",
            //     type: "character varying(256)",
            //     maxLength: 256,
            //     nullable: true,
            //     oldClrType: typeof(string),
            //     oldType: "text",
            //     oldNullable: true);
            //
            // migrationBuilder.AlterColumn<string>(
            //     name: "Legislation_Committee",
            //     table: "Reference",
            //     type: "character varying(256)",
            //     maxLength: 256,
            //     nullable: true,
            //     oldClrType: typeof(string),
            //     oldType: "text",
            //     oldNullable: true);
            //
            // migrationBuilder.AlterColumn<string>(
            //     name: "Recording_Genre",
            //     table: "Reference",
            //     type: "character varying(256)",
            //     maxLength: 256,
            //     nullable: true,
            //     oldClrType: typeof(string),
            //     oldType: "text",
            //     oldNullable: true);
            //
            // migrationBuilder.AlterColumn<string>(
            //     name: "Recording_Distributor",
            //     table: "Reference",
            //     type: "character varying(256)",
            //     maxLength: 256,
            //     nullable: true,
            //     oldClrType: typeof(string),
            //     oldType: "text",
            //     oldNullable: true);
            //
            // migrationBuilder.AlterColumn<string>(
            //     name: "PrimarySource_Subject",
            //     table: "Reference",
            //     type: "character varying(256)",
            //     maxLength: 256,
            //     nullable: true,
            //     oldClrType: typeof(string),
            //     oldType: "text",
            //     oldNullable: true);
            //
            // migrationBuilder.AlterColumn<string>(
            //     name: "Legislation_Session",
            //     table: "Reference",
            //     type: "character varying(256)",
            //     maxLength: 256,
            //     nullable: true,
            //     oldClrType: typeof(string),
            //     oldType: "text",
            //     oldNullable: true);
            //
            // migrationBuilder.AlterColumn<string>(
            //     name: "Legislation_Section",
            //     table: "Reference",
            //     type: "character varying(256)",
            //     maxLength: 256,
            //     nullable: true,
            //     oldClrType: typeof(string),
            //     oldType: "text",
            //     oldNullable: true);
            //
            // migrationBuilder.AlterColumn<string>(
            //     name: "Legislation_LegislativeBody",
            //     table: "Reference",
            //     type: "character varying(256)",
            //     maxLength: 256,
            //     nullable: true,
            //     oldClrType: typeof(string),
            //     oldType: "text",
            //     oldNullable: true);
            //
            // migrationBuilder.AlterColumn<string>(
            //     name: "Legislation_History",
            //     table: "Reference",
            //     type: "character varying(256)",
            //     maxLength: 256,
            //     nullable: true,
            //     oldClrType: typeof(string),
            //     oldType: "text",
            //     oldNullable: true);
            //
            // migrationBuilder.AlterColumn<string>(
            //     name: "Legislation_CodeVolume",
            //     table: "Reference",
            //     type: "character varying(256)",
            //     maxLength: 256,
            //     nullable: true,
            //     oldClrType: typeof(string),
            //     oldType: "text",
            //     oldNullable: true);
            //
            // migrationBuilder.AlterColumn<string>(
            //     name: "Legislation_CodePages",
            //     table: "Reference",
            //     type: "character varying(256)",
            //     maxLength: 256,
            //     nullable: true,
            //     oldClrType: typeof(string),
            //     oldType: "text",
            //     oldNullable: true);
            //
            // migrationBuilder.AlterColumn<string>(
            //     name: "Legislation_Code",
            //     table: "Reference",
            //     type: "character varying(256)",
            //     maxLength: 256,
            //     nullable: true,
            //     oldClrType: typeof(string),
            //     oldType: "text",
            //     oldNullable: true);
            //
            // migrationBuilder.AlterColumn<string>(
            //     name: "Legislation_BillNumber",
            //     table: "Reference",
            //     type: "character varying(256)",
            //     maxLength: 256,
            //     nullable: true,
            //     oldClrType: typeof(string),
            //     oldType: "text",
            //     oldNullable: true);
            //
            // migrationBuilder.AlterColumn<string>(
            //     name: "Book_Volume",
            //     table: "Reference",
            //     type: "character varying(256)",
            //     maxLength: 256,
            //     nullable: true,
            //     oldClrType: typeof(string),
            //     oldType: "text",
            //     oldNullable: true);
            //
            // migrationBuilder.AlterColumn<string>(
            //     name: "Recording_RunningTime",
            //     table: "Reference",
            //     type: "character varying(256)",
            //     maxLength: 256,
            //     nullable: true,
            //     oldClrType: typeof(string),
            //     oldType: "text",
            //     oldNullable: true);
            //
            // migrationBuilder.AlterColumn<string>(
            //     name: "Recording_Label",
            //     table: "Reference",
            //     type: "character varying(256)",
            //     maxLength: 256,
            //     nullable: true,
            //     oldClrType: typeof(string),
            //     oldType: "text",
            //     oldNullable: true);
            //
            // migrationBuilder.AlterColumn<string>(
            //     name: "PrimarySource_Medium",
            //     table: "Reference",
            //     type: "character varying(256)",
            //     maxLength: 256,
            //     nullable: true,
            //     oldClrType: typeof(string),
            //     oldType: "character varying(128)",
            //     oldMaxLength: 128,
            //     oldNullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "MeetingName",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "Presentation_Date",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "Presentation_Place",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "Report_Date",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "Report_Pages",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "Report_Place",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "Report_SeriesTitle",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "Software_Date",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
            //
            // migrationBuilder.AddColumn<string>(
            //     name: "VersionNumber",
            //     table: "Reference",
            //     type: "text",
            //     nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MeetingName",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Presentation_Date",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Presentation_Place",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Report_Date",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Report_Pages",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Report_Place",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Report_SeriesTitle",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Software_Date",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "VersionNumber",
                table: "Reference");

            migrationBuilder.RenameColumn(
                name: "Software_SeriesTitle",
                table: "Reference",
                newName: "AudioRecording_SeriesTitle");

            migrationBuilder.RenameColumn(
                name: "Software_Place",
                table: "Reference",
                newName: "AudioRecording_Place");

            migrationBuilder.RenameColumn(
                name: "Recording_RunningTime",
                table: "Reference",
                newName: "AudioRecording_RunningTime");

            migrationBuilder.RenameColumn(
                name: "Recording_ProgramTitle",
                table: "Reference",
                newName: "RadioBroadcast_ProgramTitle");

            migrationBuilder.RenameColumn(
                name: "Recording_Network",
                table: "Reference",
                newName: "RadioBroadcast_Network");

            migrationBuilder.RenameColumn(
                name: "Recording_Label",
                table: "Reference",
                newName: "AudioRecording_Label");

            migrationBuilder.RenameColumn(
                name: "Recording_Genre",
                table: "Reference",
                newName: "Film_Genre");

            migrationBuilder.RenameColumn(
                name: "Recording_EpisodeNumber",
                table: "Reference",
                newName: "Podcast_EpisodeNumber");

            migrationBuilder.RenameColumn(
                name: "Recording_Distributor",
                table: "Reference",
                newName: "Film_Distributor");

            migrationBuilder.RenameColumn(
                name: "PrimarySource_Subject",
                table: "Reference",
                newName: "Email_Subject");

            migrationBuilder.RenameColumn(
                name: "PrimarySource_Medium",
                table: "Reference",
                newName: "Artwork_Medium");

            migrationBuilder.RenameColumn(
                name: "Legislation_Session",
                table: "Reference",
                newName: "Bill_Session");

            migrationBuilder.RenameColumn(
                name: "Legislation_Section",
                table: "Reference",
                newName: "Bill_Section");

            migrationBuilder.RenameColumn(
                name: "Legislation_LegislativeBody",
                table: "Reference",
                newName: "Bill_LegislativeBody");

            migrationBuilder.RenameColumn(
                name: "Legislation_History",
                table: "Reference",
                newName: "Bill_History");

            migrationBuilder.RenameColumn(
                name: "Legislation_DocumentNumber",
                table: "Reference",
                newName: "Hearing_DocumentNumber");

            migrationBuilder.RenameColumn(
                name: "Legislation_Committee",
                table: "Reference",
                newName: "Hearing_Committee");

            migrationBuilder.RenameColumn(
                name: "Legislation_CodeVolume",
                table: "Reference",
                newName: "Bill_CodeVolume");

            migrationBuilder.RenameColumn(
                name: "Legislation_CodePages",
                table: "Reference",
                newName: "Bill_CodePages");

            migrationBuilder.RenameColumn(
                name: "Legislation_Code",
                table: "Reference",
                newName: "Bill_Code");

            migrationBuilder.RenameColumn(
                name: "Legislation_BillNumber",
                table: "Reference",
                newName: "BillNumber1");

            migrationBuilder.RenameColumn(
                name: "Book_Volume",
                table: "Reference",
                newName: "AudioRecording_Volume");

            migrationBuilder.RenameColumn(
                name: "Website_Date",
                table: "Reference",
                newName: "ConferencePaper_ConferenceName");

            migrationBuilder.RenameColumn(
                name: "Book_SeriesTitle",
                table: "Reference",
                newName: "Version");

            migrationBuilder.AlterColumn<string>(
                name: "Website_ForumTitle",
                table: "Reference",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "WebsiteType1",
                table: "Reference",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "WebsiteTitle",
                table: "Reference",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Volume",
                table: "Reference",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "System",
                table: "Reference",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Subject",
                table: "Reference",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Session",
                table: "Reference",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SeriesTitle",
                table: "Reference",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Section",
                table: "Reference",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RunningTime",
                table: "Reference",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ReportType",
                table: "Reference",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ReportNumber",
                table: "Reference",
                type: "integer",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProgrammingLanguage",
                table: "Reference",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProgramTitle",
                table: "Reference",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PresentationType",
                table: "Reference",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Place",
                table: "Reference",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Network",
                table: "Reference",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Medium",
                table: "Reference",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(128)",
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LegislativeBody",
                table: "Reference",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Label",
                table: "Reference",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Institution",
                table: "Reference",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "History",
                table: "Reference",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Genre",
                table: "Reference",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EpisodeNumber",
                table: "Reference",
                type: "integer",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DocumentNumber",
                table: "Reference",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Distributor",
                table: "Reference",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConferenceName",
                table: "Reference",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Company",
                table: "Reference",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Committee",
                table: "Reference",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CodeVolume",
                table: "Reference",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CodePages",
                table: "Reference",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Reference",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BillNumber",
                table: "Reference",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AudioRecording_RunningTime",
                table: "Reference",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RadioBroadcast_ProgramTitle",
                table: "Reference",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RadioBroadcast_Network",
                table: "Reference",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AudioRecording_Label",
                table: "Reference",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Film_Genre",
                table: "Reference",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Film_Distributor",
                table: "Reference",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email_Subject",
                table: "Reference",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Artwork_Medium",
                table: "Reference",
                type: "character varying(128)",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Bill_Session",
                table: "Reference",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Bill_Section",
                table: "Reference",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Bill_LegislativeBody",
                table: "Reference",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Bill_History",
                table: "Reference",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Hearing_DocumentNumber",
                table: "Reference",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Hearing_Committee",
                table: "Reference",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Bill_CodeVolume",
                table: "Reference",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Bill_CodePages",
                table: "Reference",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Bill_Code",
                table: "Reference",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BillNumber1",
                table: "Reference",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AudioRecording_Volume",
                table: "Reference",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProceedingTitle",
                table: "Reference",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true);
        }
    }
}
