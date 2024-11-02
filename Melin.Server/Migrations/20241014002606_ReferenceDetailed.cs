using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Melin.Server.Migrations
{
    /// <inheritdoc />
    public partial class ReferenceDetailed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ShortTitle",
                table: "Reference",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<DateTime>(
                name: "DatePublished",
                table: "Reference",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string[]>(
                name: "ExtraFields",
                table: "Reference",
                type: "text[]",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Language",
                table: "Reference",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string[]>(
                name: "Rights",
                table: "Reference",
                type: "text[]",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Reference",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Creator",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    LastName = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    ReferenceId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Creator", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Creator_Reference_ReferenceId",
                        column: x => x.ReferenceId,
                        principalTable: "Reference",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Creator_ReferenceId",
                table: "Creator",
                column: "ReferenceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Creator");

            migrationBuilder.DropColumn(
                name: "DatePublished",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "ExtraFields",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Rights",
                table: "Reference");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Reference");

            migrationBuilder.AlterColumn<string>(
                name: "ShortTitle",
                table: "Reference",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
