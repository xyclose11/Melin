using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Melin.Server.Migrations
{
    /// <inheritdoc />
    public partial class creatortypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Types",
                table: "Creator",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Types",
                table: "Creator");
        }
    }
}
