using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMAOCRM.Repository.Migrations
{
    /// <inheritdoc />
    public partial class EpcReaderDataAddCountColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "EpcReadDatas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "EpcReadDatas");
        }
    }
}
