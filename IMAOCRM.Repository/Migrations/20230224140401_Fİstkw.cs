using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMAOCRM.Repository.Migrations
{
    /// <inheritdoc />
    public partial class Fİstkw : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Com48s",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Num = table.Column<int>(type: "int", nullable: false),
                    Num1 = table.Column<int>(type: "int", nullable: false),
                    Num2 = table.Column<int>(type: "int", nullable: false),
                    Num3 = table.Column<int>(type: "int", nullable: false),
                    BaudRate = table.Column<int>(type: "int", nullable: false),
                    DataBits = table.Column<int>(type: "int", nullable: false),
                    StopBits = table.Column<int>(type: "int", nullable: false),
                    Parity = table.Column<int>(type: "int", nullable: false),
                    OutCount = table.Column<int>(type: "int", nullable: false),
                    DevAddr = table.Column<int>(type: "int", nullable: false),
                    BytesToRead = table.Column<int>(type: "int", nullable: false),
                    InCount = table.Column<int>(type: "int", nullable: false),
                    Sources = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HexString = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Str1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Str2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PortName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Laohuaintval = table.Column<double>(type: "float", nullable: false),
                    Readinterval = table.Column<double>(type: "float", nullable: false),
                    Xunhuaintval = table.Column<double>(type: "float", nullable: false),
                    IsBackground = table.Column<bool>(type: "bit", nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    NumArray = table.Column<byte>(type: "tinyint", nullable: false),
                    ToHexByte = table.Column<byte>(type: "tinyint", nullable: false),
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
                    table.PrimaryKey("PK_Com48s", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Com48s");
        }
    }
}
