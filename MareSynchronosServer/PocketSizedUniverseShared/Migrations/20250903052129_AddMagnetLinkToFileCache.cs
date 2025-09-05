using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PocketSizedUniverseServer.Migrations
{
    /// <inheritdoc />
    public partial class AddMagnetLinkToFileCache : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_forbidden",
                table: "file_caches",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "magnet_link",
                table: "file_caches",
                type: "character varying(2048)",
                maxLength: 2048,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_forbidden",
                table: "file_caches");

            migrationBuilder.DropColumn(
                name: "magnet_link",
                table: "file_caches");
        }
    }
}
