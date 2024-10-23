using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Melin.Server.Migrations
{
    /// <inheritdoc />
    public partial class creatortypesedit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Creators_Reference_ReferenceId",
                table: "Creators");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddPrimaryKey(
                name: "PK_Creators",
                table: "Creators",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Creators_Reference_ReferenceId",
                table: "Creators",
                column: "ReferenceId",
                principalTable: "Reference",
                principalColumn: "Id");
        }
    }
}
