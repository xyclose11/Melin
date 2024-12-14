using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Melin.Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLaptop : Migration
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
                principalColumn: "Id");
            
            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Group",
                type: "integer",
                nullable: true);

            
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
    }
}
