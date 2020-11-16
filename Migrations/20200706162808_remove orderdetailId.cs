using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStoreWeb.Migrations
{
    public partial class removeorderdetailId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderDetailId",
                table: "OrderDetails");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderDetailId",
                table: "OrderDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
