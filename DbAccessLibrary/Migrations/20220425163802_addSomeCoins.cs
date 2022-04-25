using Microsoft.EntityFrameworkCore.Migrations;

namespace DbAccessLibrary.Migrations
{
    public partial class addSomeCoins : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Atom",
                table: "UserBalance",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Axs",
                table: "UserBalance",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Dogecoin",
                table: "UserBalance",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Litecoin",
                table: "UserBalance",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Mana",
                table: "UserBalance",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Xrp",
                table: "UserBalance",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Atom",
                table: "UserBalance");

            migrationBuilder.DropColumn(
                name: "Axs",
                table: "UserBalance");

            migrationBuilder.DropColumn(
                name: "Dogecoin",
                table: "UserBalance");

            migrationBuilder.DropColumn(
                name: "Litecoin",
                table: "UserBalance");

            migrationBuilder.DropColumn(
                name: "Mana",
                table: "UserBalance");

            migrationBuilder.DropColumn(
                name: "Xrp",
                table: "UserBalance");
        }
    }
}
