using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SyncHms.Bus.EntityFramework.Sqlite.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exchanges",
                columns: table => new
                {
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exchanges", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Queues",
                columns: table => new
                {
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ExchangeName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Queues", x => new { x.Name, x.ExchangeName });
                    table.ForeignKey(
                        name: "FK_Queues_Exchanges_ExchangeName",
                        column: x => x.ExchangeName,
                        principalTable: "Exchanges",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    ExchangeName = table.Column<string>(type: "TEXT", nullable: false),
                    QueueName = table.Column<string>(type: "TEXT", nullable: false),
                    DateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Json = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Queues_QueueName_ExchangeName",
                        columns: x => new { x.QueueName, x.ExchangeName },
                        principalTable: "Queues",
                        principalColumns: new[] { "Name", "ExchangeName" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_QueueName_ExchangeName",
                table: "Messages",
                columns: new[] { "QueueName", "ExchangeName" });

            migrationBuilder.CreateIndex(
                name: "IX_Queues_ExchangeName",
                table: "Queues",
                column: "ExchangeName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Queues");

            migrationBuilder.DropTable(
                name: "Exchanges");
        }
    }
}
