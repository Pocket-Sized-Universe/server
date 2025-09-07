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
            // Step 1: Rename the existing column to a temporary name
            migrationBuilder.RenameColumn(
                name: "hash",
                table: "torrent_file_entries",
                newName: "hash_old");

            // Step 2: Add the new bytea column
            migrationBuilder.AddColumn<byte[]>(
                name: "hash",
                table: "torrent_file_entries",
                type: "bytea",
                nullable: true);

            // Step 3: Convert data from the old column to the new one
            migrationBuilder.Sql("UPDATE torrent_file_entries SET hash = hash_old::bytea");

            // Step 4: Make the new column non-nullable if the original was non-nullable
            migrationBuilder.AlterColumn<byte[]>(
                name: "hash",
                table: "torrent_file_entries",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "bytea",
                oldNullable: true);

            // Step 5: Drop the old column
            migrationBuilder.DropColumn(
                name: "hash_old",
                table: "torrent_file_entries");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Revert the changes in reverse order
            // Step 1: Add back the old string column
            migrationBuilder.AddColumn<string>(
                name: "hash_old",
                table: "torrent_file_entries",
                type: "text",
                nullable: true);

            // Step 2: Convert data from bytea back to string
            migrationBuilder.Sql("UPDATE torrent_file_entries SET hash_old = encode(hash, 'escape')");

            // Step 3: Drop the bytea column
            migrationBuilder.DropColumn(
                name: "hash",
                table: "torrent_file_entries");

            // Step 4: Rename the old column back to the original name
            migrationBuilder.RenameColumn(
                name: "hash_old",
                table: "torrent_file_entries",
                newName: "hash");
        }
    }
}
