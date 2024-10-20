using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SyncHms.Domain.EntityFramework.Sqlite.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDescriptionFromSchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Schedules");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Schedules",
                type: "TEXT",
                nullable: true);
        }
    }
}
