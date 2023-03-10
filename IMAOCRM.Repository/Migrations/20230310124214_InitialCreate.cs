using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMAOCRM.Repository.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EPCReadTemps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Epc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rssi = table.Column<int>(type: "int", nullable: false),
                    Ant = table.Column<byte>(type: "tinyint", nullable: false),
                    EpcDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EPCReadTemps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EpcReadDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Epc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rssi = table.Column<int>(type: "int", nullable: false),
                    Ant = table.Column<byte>(type: "tinyint", nullable: false),
                    EpcDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdaterId = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleterId = table.Column<int>(type: "int", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EpcReadDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EpcReadDatas_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EpcReadDatas_Users_DeleterId",
                        column: x => x.DeleterId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EpcReadDatas_Users_UpdaterId",
                        column: x => x.UpdaterId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EpcReadDatas_CreatorId",
                table: "EpcReadDatas",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_EpcReadDatas_DeleterId",
                table: "EpcReadDatas",
                column: "DeleterId");

            migrationBuilder.CreateIndex(
                name: "IX_EpcReadDatas_UpdaterId",
                table: "EpcReadDatas",
                column: "UpdaterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EpcReadDatas");

            migrationBuilder.DropTable(
                name: "EPCReadTemps");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
