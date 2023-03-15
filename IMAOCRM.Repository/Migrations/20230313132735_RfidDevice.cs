using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMAOCRM.Repository.Migrations
{
    /// <inheritdoc />
    public partial class RfidDevice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RFIDDevices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Portnum = table.Column<int>(type: "int", nullable: false),
                    Baud = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_RFIDDevices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RFIDDevices_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RFIDDevices_Users_DeleterId",
                        column: x => x.DeleterId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RFIDDevices_Users_UpdaterId",
                        column: x => x.UpdaterId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RFIDDeviceAntennas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RFIDDeviceId = table.Column<int>(type: "int", nullable: false),
                    IntAntenna = table.Column<byte>(type: "tinyint", nullable: false),
                    Antenna = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_RFIDDeviceAntennas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RFIDDeviceAntennas_RFIDDevices_RFIDDeviceId",
                        column: x => x.RFIDDeviceId,
                        principalTable: "RFIDDevices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RFIDDeviceAntennas_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RFIDDeviceAntennas_Users_DeleterId",
                        column: x => x.DeleterId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RFIDDeviceAntennas_Users_UpdaterId",
                        column: x => x.UpdaterId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RFIDDeviceAntennas_CreatorId",
                table: "RFIDDeviceAntennas",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_RFIDDeviceAntennas_DeleterId",
                table: "RFIDDeviceAntennas",
                column: "DeleterId");

            migrationBuilder.CreateIndex(
                name: "IX_RFIDDeviceAntennas_RFIDDeviceId",
                table: "RFIDDeviceAntennas",
                column: "RFIDDeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_RFIDDeviceAntennas_UpdaterId",
                table: "RFIDDeviceAntennas",
                column: "UpdaterId");

            migrationBuilder.CreateIndex(
                name: "IX_RFIDDevices_CreatorId",
                table: "RFIDDevices",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_RFIDDevices_DeleterId",
                table: "RFIDDevices",
                column: "DeleterId");

            migrationBuilder.CreateIndex(
                name: "IX_RFIDDevices_UpdaterId",
                table: "RFIDDevices",
                column: "UpdaterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RFIDDeviceAntennas");

            migrationBuilder.DropTable(
                name: "RFIDDevices");
        }
    }
}
