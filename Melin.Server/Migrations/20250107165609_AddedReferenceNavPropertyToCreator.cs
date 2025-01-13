using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Melin.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddedReferenceNavPropertyToCreator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Creators_Reference_ReferenceId",
                table: "Creators");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Creators_Reference_ReferenceId",
                table: "Creators",
                column: "ReferenceId",
                principalTable: "Reference",
                principalColumn: "Id");
        }
    }
}
