using Microsoft.EntityFrameworkCore.Migrations;

namespace DbAccessLibrary.Migrations
{
    public partial class createOpenPriceModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OpenPrices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoinName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OpenPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenPrices", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OpenPrices");
        }
    }
}
