using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameServerLauncher.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServerStatistics",
                columns: table => new
                {
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CPU = table.Column<float>(type: "real", nullable: false),
                    RAMAmount = table.Column<float>(type: "real", nullable: false),
                    RAMUsage = table.Column<float>(type: "real", nullable: false),
                    Bandwidth = table.Column<float>(type: "real", nullable: false),
                    StorageAmount = table.Column<float>(type: "real", nullable: false),
                    StorageUsage = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerStatistics", x => x.Time);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServerStatistics");
        }
    }
}
