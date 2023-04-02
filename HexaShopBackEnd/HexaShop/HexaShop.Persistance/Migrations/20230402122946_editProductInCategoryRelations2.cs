using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HexaShop.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class editProductInCategoryRelations2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductInCategory_Category_CategoryId",
                table: "ProductInCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductInCategory_Products_ProductId",
                table: "ProductInCategory");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInCategory_Category_CategoryId",
                table: "ProductInCategory",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInCategory_Products_ProductId",
                table: "ProductInCategory",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductInCategory_Category_CategoryId",
                table: "ProductInCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductInCategory_Products_ProductId",
                table: "ProductInCategory");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInCategory_Category_CategoryId",
                table: "ProductInCategory",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInCategory_Products_ProductId",
                table: "ProductInCategory",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
