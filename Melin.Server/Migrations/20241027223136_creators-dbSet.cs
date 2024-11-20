using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Melin.Server.Migrations
{
    /// <inheritdoc />
    public partial class creatorsdbSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.DropForeignKey(
            //     name: "FK_Creator_Reference_ReferenceId",
            //     table: "Creator");

            // migrationBuilder.DropPrimaryKey(
            //     name: "PK_Creator",
            //     table: "Creator");

            // migrationBuilder.RenameTable(
            //     name: "Creator",
            //     newName: "Creators");

            // migrationBuilder.RenameIndex(
            //     name: "IX_Creator_ReferenceId",
            //     table: "Creators",
            //     newName: "IX_Creators_ReferenceId");

            // migrationBuilder.AlterColumn<int>(
            //     name: "ReferenceId",
            //     table: "Creators",
            //     type: "integer",
            //     nullable: false,
            //     defaultValue: 0,
            //     oldClrType: typeof(int),
            //     oldType: "integer",
            //     oldNullable: true);

            // migrationBuilder.AddPrimaryKey(
            //     name: "PK_Creators",
            //     table: "Creators",
            //     column: "Id");

            // migrationBuilder.AddForeignKey(
            //     name: "FK_Creators_Reference_ReferenceId",
            //     table: "Creators",
            //     column: "ReferenceId",
            //     principalTable: "Reference",
            //     principalColumn: "Id",
            //     onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
