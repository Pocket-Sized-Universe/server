using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PocketSizedUniverseServer.Migrations
{
    /// <inheritdoc />
    public partial class MigrateFromStringToByteArrayHashTake2 : Migration
    {
        /// <inheritdoc />
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(name: "hash", table: "torrent_file_entries", type: "bytea", nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "hash", table: "torrent_file_entries");
        }
    }
}
