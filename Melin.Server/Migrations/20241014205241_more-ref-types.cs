using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Melin.Server.Migrations
{
    /// <inheritdoc />
    public partial class morereftypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Reference",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationNumber",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Artwork_Medium",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Assignee",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BillNumber",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BookTitle",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Book_Place",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Book_Section",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CaseName",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodeNumber",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodePages",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodeVolume",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Committee",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Company",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConferenceName",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Court",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateDecided",
                table: "Reference",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateEnacted",
                table: "Reference",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Dimensions",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Reference",
                type: "character varying(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Distributor",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DocketNumber",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DocumentNumber",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Edition",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EpisodeNumber",
                table: "Reference",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileFormat",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FilingDate",
                table: "Reference",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstPage",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ForumTitle",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "History",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ISBN",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ISSN",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Institution",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Issue",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "IssueDate",
                table: "Reference",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IssuingAuthority",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JournalAbbr",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Label",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LegalCases_History",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LegalStatus",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LegislativeBody",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MapType",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Medium",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameOfAct",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Network",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PageAmount",
                table: "Reference",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Pages",
                table: "Reference",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatentNumber",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Place",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostType",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PresentationType",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrimarySourceType",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PriorityNumber",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProceedingTitle",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProgramTitle",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProgrammingLanguage",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PublicLawNumber",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Publication",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Publisher",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string[]>(
                name: "References",
                table: "Reference",
                type: "text[]",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReportNumber",
                table: "Reference",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReportType",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Reporter",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReporterVolume",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RunningTime",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Scale",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Section",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Series",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SeriesNumber",
                table: "Reference",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeriesTitle",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Session",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Studio",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "System",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Version",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Volume",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VolumeAmount",
                table: "Reference",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WebsiteTitle",
                table: "Reference",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WebsiteType",
                table: "Reference",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationNumber",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Artwork_Medium",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Assignee",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "BillNumber",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "BookTitle",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Book_Place",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Book_Section",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "CaseName",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "CodeNumber",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "CodePages",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "CodeVolume",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Committee",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Company",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "ConferenceName",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Court",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "DateDecided",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "DateEnacted",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Dimensions",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Distributor",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "DocketNumber",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "DocumentNumber",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Edition",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "EpisodeNumber",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "FileFormat",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "FilingDate",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "FirstPage",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "ForumTitle",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Genre",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "History",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "ISBN",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "ISSN",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Institution",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Issue",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "IssueDate",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "IssuingAuthority",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "JournalAbbr",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Label",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "LegalCases_History",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "LegalStatus",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "LegislativeBody",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "MapType",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Medium",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "NameOfAct",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Network",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "PageAmount",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Pages",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "PatentNumber",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Place",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "PostType",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "PresentationType",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "PrimarySourceType",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "PriorityNumber",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "ProceedingTitle",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "ProgramTitle",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "ProgrammingLanguage",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "PublicLawNumber",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Publication",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Publisher",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "References",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "ReportNumber",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "ReportType",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Reporter",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "ReporterVolume",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "RunningTime",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Scale",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Section",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Series",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "SeriesNumber",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "SeriesTitle",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Session",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Studio",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Subject",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "System",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Volume",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "VolumeAmount",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "WebsiteTitle",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "WebsiteType",
                table: "Reference");

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Reference",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}
