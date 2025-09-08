using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PocketSizedUniverseShared.Migrations
{
    /// <inheritdoc />
    public partial class AddSuperSeederRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_super_seeder",
                table: "users",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_super_seeder",
                table: "users");
        }
    }
}
