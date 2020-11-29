using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStoreWeb.Migrations
{
    public partial class addpaypalid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Payment",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "PaymentMethod",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaypalId",
                table: "Orders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PaypalId",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "Payment",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
