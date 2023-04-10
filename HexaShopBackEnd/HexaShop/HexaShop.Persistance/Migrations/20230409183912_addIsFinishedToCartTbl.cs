using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HexaShop.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class addIsFinishedToCartTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFinished",
                table: "Carts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFinished",
                table: "Carts");
        }
    }
}
