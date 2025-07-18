using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SyncHms.Domain.EntityFramework.Sqlite.Migrations
{
    /// <inheritdoc />
    public partial class AddLastSuccessDateTimeSchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastSuccess",
                table: "Schedules",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastSuccess",
                table: "Schedules");
        }
    }
}
