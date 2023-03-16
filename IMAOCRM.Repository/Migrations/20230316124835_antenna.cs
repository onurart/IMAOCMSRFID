using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMAOCRM.Repository.Migrations
{
    /// <inheritdoc />
    public partial class antenna : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IntAntenna",
                table: "RFIDDeviceAntennas");

            migrationBuilder.AddColumn<int>(
                name: "AntennaPower",
                table: "RFIDDeviceAntennas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AntennaPower",
                table: "RFIDDeviceAntennas");

            migrationBuilder.AddColumn<byte>(
                name: "IntAntenna",
                table: "RFIDDeviceAntennas",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}
