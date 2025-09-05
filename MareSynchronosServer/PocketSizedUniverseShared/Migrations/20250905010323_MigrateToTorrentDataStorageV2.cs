using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PocketSizedUniverseServer.Migrations
{
    /// <inheritdoc />
    public partial class MigrateToTorrentDataStorageV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "file_extension",
                table: "torrent_file_entries",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "forbidden_by",
                table: "torrent_file_entries",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_forbidden",
                table: "torrent_file_entries",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<byte[]>(
                name: "torrent_data",
                table: "torrent_file_entries",
                type: "bytea",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "file_extension",
                table: "torrent_file_entries");

            migrationBuilder.DropColumn(
                name: "forbidden_by",
                table: "torrent_file_entries");

            migrationBuilder.DropColumn(
                name: "is_forbidden",
                table: "torrent_file_entries");

            migrationBuilder.DropColumn(
                name: "torrent_data",
                table: "torrent_file_entries");
        }
    }
}
