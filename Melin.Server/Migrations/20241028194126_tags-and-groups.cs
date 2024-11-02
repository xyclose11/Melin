using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Melin.Server.Migrations
{
    /// <inheritdoc />
    public partial class tagsandgroups : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Creator_Reference_ReferenceId",
                table: "Creator");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Creator",
                table: "Creator");

            migrationBuilder.RenameTable(
                name: "Creator",
                newName: "Creators");

            migrationBuilder.RenameIndex(
                name: "IX_Creator_ReferenceId",
                table: "Creators",
                newName: "IX_Creators_ReferenceId");

            migrationBuilder.AlterColumn<int>(
                name: "ReferenceId",
                table: "Creators",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Creators",
                table: "Creators",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Text = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupReference",
                columns: table => new
                {
                    GroupsId = table.Column<int>(type: "integer", nullable: false),
                    ReferencesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupReference", x => new { x.GroupsId, x.ReferencesId });
                    table.ForeignKey(
                        name: "FK_GroupReference_Group_GroupsId",
                        column: x => x.GroupsId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupReference_Reference_ReferencesId",
                        column: x => x.ReferencesId,
                        principalTable: "Reference",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReferenceTag",
                columns: table => new
                {
                    ReferencesId = table.Column<int>(type: "integer", nullable: false),
                    TagsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferenceTag", x => new { x.ReferencesId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_ReferenceTag_Reference_ReferencesId",
                        column: x => x.ReferencesId,
                        principalTable: "Reference",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReferenceTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupReference_ReferencesId",
                table: "GroupReference",
                column: "ReferencesId");

            migrationBuilder.CreateIndex(
                name: "IX_ReferenceTag_TagsId",
                table: "ReferenceTag",
                column: "TagsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Creators_Reference_ReferenceId",
                table: "Creators",
                column: "ReferenceId",
                principalTable: "Reference",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Creators_Reference_ReferenceId",
                table: "Creators");

            migrationBuilder.DropTable(
                name: "GroupReference");

            migrationBuilder.DropTable(
                name: "ReferenceTag");

            migrationBuilder.DropTable(
                name: "Group");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Creators",
                table: "Creators");

            migrationBuilder.RenameTable(
                name: "Creators",
                newName: "Creator");

            migrationBuilder.RenameIndex(
                name: "IX_Creators_ReferenceId",
                table: "Creator",
                newName: "IX_Creator_ReferenceId");

            migrationBuilder.AlterColumn<int>(
                name: "ReferenceId",
                table: "Creator",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Creator",
                table: "Creator",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Creator_Reference_ReferenceId",
                table: "Creator",
                column: "ReferenceId",
                principalTable: "Reference",
                principalColumn: "Id");
        }
    }
}
