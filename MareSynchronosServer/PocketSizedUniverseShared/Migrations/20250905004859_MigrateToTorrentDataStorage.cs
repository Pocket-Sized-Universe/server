using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PocketSizedUniverseServer.Migrations
{
    /// <inheritdoc />
    public partial class MigrateToTorrentDataStorage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "file_redirect_entries",
                columns: table => new
                {
                    game_path = table.Column<string>(type: "text", nullable: false),
                    parent_id = table.Column<string>(type: "text", nullable: false),
                    parent_uploader_uid = table.Column<string>(type: "character varying(10)", nullable: false),
                    swap_path = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_file_redirect_entries", x => new { x.parent_id, x.parent_uploader_uid, x.game_path });
                    table.ForeignKey(
                        name: "fk_file_redirect_entries_chara_data_parent_id_parent_uploader_",
                        columns: x => new { x.parent_id, x.parent_uploader_uid },
                        principalTable: "chara_data",
                        principalColumns: new[] { "id", "uploader_uid" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "torrent_file_entries",
                columns: table => new
                {
                    game_path = table.Column<string>(type: "text", nullable: false),
                    parent_id = table.Column<string>(type: "text", nullable: false),
                    parent_uploader_uid = table.Column<string>(type: "character varying(10)", nullable: false),
                    hash = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_torrent_file_entries", x => new { x.parent_id, x.parent_uploader_uid, x.game_path });
                    table.ForeignKey(
                        name: "fk_torrent_file_entries_chara_data_parent_id_parent_uploader_u",
                        columns: x => new { x.parent_id, x.parent_uploader_uid },
                        principalTable: "chara_data",
                        principalColumns: new[] { "id", "uploader_uid" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_file_redirect_entries_parent_id",
                table: "file_redirect_entries",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "ix_torrent_file_entries_parent_id",
                table: "torrent_file_entries",
                column: "parent_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "file_redirect_entries");

            migrationBuilder.DropTable(
                name: "torrent_file_entries");
        }
    }
}
