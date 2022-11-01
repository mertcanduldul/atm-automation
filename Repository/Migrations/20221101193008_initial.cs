using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Automation.Repository.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Money",
                columns: table => new
                {
                    ID_MONEY = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MONEY_TYPE_ID = table.Column<int>(type: "int", nullable: false),
                    MONEY_NAME = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    MONEY_VALUE = table.Column<int>(type: "int", nullable: false),
                    ID_TAPE = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Money", x => x.ID_MONEY);
                });

            migrationBuilder.CreateTable(
                name: "Tape",
                columns: table => new
                {
                    ID_TAPE = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TAPE_STATE_TYPE_ID = table.Column<int>(type: "int", nullable: false),
                    TAPE_MONEY_TYPE_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tape", x => x.ID_TAPE);
                });

            migrationBuilder.InsertData(
                table: "Tape",
                columns: new[] { "ID_TAPE", "TAPE_MONEY_TYPE_ID", "TAPE_STATE_TYPE_ID" },
                values: new object[,]
                {
                    { 1, 4, 1 },
                    { 2, 1, 3 },
                    { 3, 2, 3 },
                    { 4, 3, 3 },
                    { 5, 4, 3 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Money");

            migrationBuilder.DropTable(
                name: "Tape");
        }
    }
}
