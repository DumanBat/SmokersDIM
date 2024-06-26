using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmokersDim.Migrations
{
    /// <inheritdoc />
    public partial class StatsKeysUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StatEntries_StatsBlocks_statsBlockId",
                table: "StatEntries");

            migrationBuilder.RenameColumn(
                name: "statsBlockId",
                table: "StatEntries",
                newName: "StatsBlockId");

            migrationBuilder.RenameIndex(
                name: "IX_StatEntries_statsBlockId",
                table: "StatEntries",
                newName: "IX_StatEntries_StatsBlockId");

            migrationBuilder.AddForeignKey(
                name: "FK_StatEntries_StatsBlocks_StatsBlockId",
                table: "StatEntries",
                column: "StatsBlockId",
                principalTable: "StatsBlocks",
                principalColumn: "statGroupHash",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StatEntries_StatsBlocks_StatsBlockId",
                table: "StatEntries");

            migrationBuilder.RenameColumn(
                name: "StatsBlockId",
                table: "StatEntries",
                newName: "statsBlockId");

            migrationBuilder.RenameIndex(
                name: "IX_StatEntries_StatsBlockId",
                table: "StatEntries",
                newName: "IX_StatEntries_statsBlockId");

            migrationBuilder.AddForeignKey(
                name: "FK_StatEntries_StatsBlocks_statsBlockId",
                table: "StatEntries",
                column: "statsBlockId",
                principalTable: "StatsBlocks",
                principalColumn: "statGroupHash",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
