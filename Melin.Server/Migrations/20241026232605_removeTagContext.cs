using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Melin.Server.Migrations
{
    /// <inheritdoc />
    public partial class removeTagContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.AlterColumn<int>(
            //     name: "Id",
            //     table: "Tags",
            //     type: "integer",
            //     nullable: false,
            //     oldClrType: typeof(string),
            //     oldType: "character varying(256)",
            //     oldMaxLength: 256)
            //     .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            // migrationBuilder.AlterColumn<int>(
            //     name: "TagsId",
            //     table: "ReferenceTag",
            //     type: "integer",
            //     nullable: false,
            //     oldClrType: typeof(string),
            //     oldType: "character varying(256)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Tags",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<string>(
                name: "TagsId",
                table: "ReferenceTag",
                type: "character varying(256)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}
