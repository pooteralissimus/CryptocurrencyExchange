using Microsoft.EntityFrameworkCore.Migrations;

namespace DbAccessLibrary.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserBalance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Usdt = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Bitcoin = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Etherium = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Luna = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Solana = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBalance", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserBalance");
        }
    }
}
 