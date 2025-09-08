using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PocketSizedUniverseShared.Migrations
{
    /// <inheritdoc />
    public partial class IndependentFileEntries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_file_redirect_entries_chara_data_parent_id_parent_uploader_",
                table: "file_redirect_entries");

            migrationBuilder.DropForeignKey(
                name: "fk_torrent_file_entries_chara_data_parent_id_parent_uploader_u",
                table: "torrent_file_entries");

            migrationBuilder.DropPrimaryKey(
                name: "pk_torrent_file_entries",
                table: "torrent_file_entries");

            migrationBuilder.DropIndex(
                name: "ix_torrent_file_entries_parent_id",
                table: "torrent_file_entries");

            migrationBuilder.DropPrimaryKey(
                name: "pk_file_redirect_entries",
                table: "file_redirect_entries");

            migrationBuilder.DropIndex(
                name: "ix_file_redirect_entries_parent_id",
                table: "file_redirect_entries");

            migrationBuilder.DropColumn(
                name: "parent_id",
                table: "torrent_file_entries");

            migrationBuilder.DropColumn(
                name: "parent_uploader_uid",
                table: "torrent_file_entries");

            migrationBuilder.DropColumn(
                name: "parent_id",
                table: "file_redirect_entries");

            migrationBuilder.DropColumn(
                name: "parent_uploader_uid",
                table: "file_redirect_entries");

            migrationBuilder.AlterColumn<string>(
                name: "game_path",
                table: "torrent_file_entries",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<long>(
                name: "id",
                table: "torrent_file_entries",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<string>(
                name: "game_path",
                table: "file_redirect_entries",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<long>(
                name: "id",
                table: "file_redirect_entries",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "pk_torrent_file_entries",
                table: "torrent_file_entries",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_file_redirect_entries",
                table: "file_redirect_entries",
                column: "id");

            migrationBuilder.CreateTable(
                name: "chara_data_file_redirect_entry",
                columns: table => new
                {
                    file_redirects_id = table.Column<long>(type: "bigint", nullable: false),
                    chara_data_id = table.Column<string>(type: "text", nullable: false),
                    chara_data_uploader_uid = table.Column<string>(type: "character varying(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_chara_data_file_redirect_entry", x => new { x.file_redirects_id, x.chara_data_id, x.chara_data_uploader_uid });
                    table.ForeignKey(
                        name: "fk_chara_data_file_redirect_entry_chara_data_chara_data_id_cha",
                        columns: x => new { x.chara_data_id, x.chara_data_uploader_uid },
                        principalTable: "chara_data",
                        principalColumns: new[] { "id", "uploader_uid" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_chara_data_file_redirect_entry_file_redirect_entries_file_r",
                        column: x => x.file_redirects_id,
                        principalTable: "file_redirect_entries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "chara_data_torrent_file_entry",
                columns: table => new
                {
                    file_swaps_id = table.Column<long>(type: "bigint", nullable: false),
                    chara_data_id = table.Column<string>(type: "text", nullable: false),
                    chara_data_uploader_uid = table.Column<string>(type: "character varying(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_chara_data_torrent_file_entry", x => new { x.file_swaps_id, x.chara_data_id, x.chara_data_uploader_uid });
                    table.ForeignKey(
                        name: "fk_chara_data_torrent_file_entry_chara_data_chara_data_id_char",
                        columns: x => new { x.chara_data_id, x.chara_data_uploader_uid },
                        principalTable: "chara_data",
                        principalColumns: new[] { "id", "uploader_uid" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_chara_data_torrent_file_entry_torrent_file_entries_file_swa",
                        column: x => x.file_swaps_id,
                        principalTable: "torrent_file_entries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_torrent_file_entries_hash",
                table: "torrent_file_entries",
                column: "hash");

            migrationBuilder.CreateIndex(
                name: "ix_chara_data_file_redirect_entry_chara_data_id_chara_data_upl",
                table: "chara_data_file_redirect_entry",
                columns: new[] { "chara_data_id", "chara_data_uploader_uid" });

            migrationBuilder.CreateIndex(
                name: "ix_chara_data_torrent_file_entry_chara_data_id_chara_data_uplo",
                table: "chara_data_torrent_file_entry",
                columns: new[] { "chara_data_id", "chara_data_uploader_uid" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "chara_data_file_redirect_entry");

            migrationBuilder.DropTable(
                name: "chara_data_torrent_file_entry");

            migrationBuilder.DropPrimaryKey(
                name: "pk_torrent_file_entries",
                table: "torrent_file_entries");

            migrationBuilder.DropIndex(
                name: "ix_torrent_file_entries_hash",
                table: "torrent_file_entries");

            migrationBuilder.DropPrimaryKey(
                name: "pk_file_redirect_entries",
                table: "file_redirect_entries");

            migrationBuilder.DropColumn(
                name: "id",
                table: "torrent_file_entries");

            migrationBuilder.DropColumn(
                name: "id",
                table: "file_redirect_entries");

            migrationBuilder.AlterColumn<string>(
                name: "game_path",
                table: "torrent_file_entries",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "parent_id",
                table: "torrent_file_entries",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "parent_uploader_uid",
                table: "torrent_file_entries",
                type: "character varying(10)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "game_path",
                table: "file_redirect_entries",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "parent_id",
                table: "file_redirect_entries",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "parent_uploader_uid",
                table: "file_redirect_entries",
                type: "character varying(10)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "pk_torrent_file_entries",
                table: "torrent_file_entries",
                columns: new[] { "parent_id", "parent_uploader_uid", "game_path" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_file_redirect_entries",
                table: "file_redirect_entries",
                columns: new[] { "parent_id", "parent_uploader_uid", "game_path" });

            migrationBuilder.CreateIndex(
                name: "ix_torrent_file_entries_parent_id",
                table: "torrent_file_entries",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "ix_file_redirect_entries_parent_id",
                table: "file_redirect_entries",
                column: "parent_id");

            migrationBuilder.AddForeignKey(
                name: "fk_file_redirect_entries_chara_data_parent_id_parent_uploader_",
                table: "file_redirect_entries",
                columns: new[] { "parent_id", "parent_uploader_uid" },
                principalTable: "chara_data",
                principalColumns: new[] { "id", "uploader_uid" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_torrent_file_entries_chara_data_parent_id_parent_uploader_u",
                table: "torrent_file_entries",
                columns: new[] { "parent_id", "parent_uploader_uid" },
                principalTable: "chara_data",
                principalColumns: new[] { "id", "uploader_uid" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
