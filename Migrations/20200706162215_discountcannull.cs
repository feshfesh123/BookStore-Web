using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStoreWeb.Migrations
{
    public partial class discountcannull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Discounts_DiscountId",
                table: "OrderDetails");

            migrationBuilder.AlterColumn<int>(
                name: "DiscountId",
                table: "OrderDetails",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Discounts_DiscountId",
                table: "OrderDetails",
                column: "DiscountId",
                principalTable: "Discounts",
                principalColumn: "DiscountId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Discounts_DiscountId",
                table: "OrderDetails");

            migrationBuilder.AlterColumn<int>(
                name: "DiscountId",
                table: "OrderDetails",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Discounts_DiscountId",
                table: "OrderDetails",
                column: "DiscountId",
                principalTable: "Discounts",
                principalColumn: "DiscountId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
