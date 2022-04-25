using Microsoft.EntityFrameworkCore.Migrations;

namespace DbAccessLibrary.Migrations
{
    public partial class redoUserBalanceModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Atom",
                table: "UserBalance");

            migrationBuilder.DropColumn(
                name: "Axs",
                table: "UserBalance");

            migrationBuilder.DropColumn(
                name: "Bitcoin",
                table: "UserBalance");

            migrationBuilder.DropColumn(
                name: "Dogecoin",
                table: "UserBalance");

            migrationBuilder.DropColumn(
                name: "Etherium",
                table: "UserBalance");

            migrationBuilder.DropColumn(
                name: "Litecoin",
                table: "UserBalance");

            migrationBuilder.DropColumn(
                name: "Luna",
                table: "UserBalance");

            migrationBuilder.DropColumn(
                name: "Mana",
                table: "UserBalance");

            migrationBuilder.DropColumn(
                name: "Solana",
                table: "UserBalance");

            migrationBuilder.DropColumn(
                name: "Usdt",
                table: "UserBalance");

            migrationBuilder.RenameColumn(
                name: "Xrp",
                table: "UserBalance",
                newName: "CoinQuantity");

            migrationBuilder.AddColumn<string>(
                name: "CoinName",
                table: "UserBalance",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoinName",
                table: "UserBalance");

            migrationBuilder.RenameColumn(
                name: "CoinQuantity",
                table: "UserBalance",
                newName: "Xrp");

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
                name: "Bitcoin",
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
                name: "Etherium",
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
                name: "Luna",
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
                name: "Solana",
                table: "UserBalance",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Usdt",
                table: "UserBalance",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
