using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace URLShortener.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DestinationURL",
                table: "URLs");

            migrationBuilder.DropColumn(
                name: "SourceURL",
                table: "URLs");

            migrationBuilder.AddColumn<string>(
                name: "OriginalUrl",
                table: "URLs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShortenedUrl",
                table: "URLs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OriginalUrl",
                table: "URLs");

            migrationBuilder.DropColumn(
                name: "ShortenedUrl",
                table: "URLs");

            migrationBuilder.AddColumn<string>(
                name: "DestinationURL",
                table: "URLs",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SourceURL",
                table: "URLs",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");
        }
    }
}
