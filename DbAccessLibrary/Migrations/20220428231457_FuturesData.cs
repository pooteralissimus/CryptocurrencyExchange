using Microsoft.EntityFrameworkCore.Migrations;

namespace DbAccessLibrary.Migrations
{
    public partial class FuturesData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "futuresDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoinName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LongSHort = table.Column<string>(type: "nvarchar(5)", nullable: true),
                    Leverage = table.Column<int>(type: "int", nullable: false),
                    OpenPrice = table.Column<decimal>(type: "decimal(18,5)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,5)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_futuresDatas", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FuturesDatas");
        }
    }
}
