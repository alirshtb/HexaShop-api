using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HexaShop.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class editDiscountTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Discount_DiscountId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Discount",
                table: "Discount");

            migrationBuilder.RenameTable(
                name: "Discount",
                newName: "Discounts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Discounts",
                table: "Discounts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Discounts_DiscountId",
                table: "Products",
                column: "DiscountId",
                principalTable: "Discounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Discounts_DiscountId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Discounts",
                table: "Discounts");

            migrationBuilder.RenameTable(
                name: "Discounts",
                newName: "Discount");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Discount",
                table: "Discount",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Discount_DiscountId",
                table: "Products",
                column: "DiscountId",
                principalTable: "Discount",
                principalColumn: "Id");
        }
    }
}
