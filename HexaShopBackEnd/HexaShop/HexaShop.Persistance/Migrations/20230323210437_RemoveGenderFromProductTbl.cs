using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HexaShop.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class RemoveGenderFromProductTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Score",
                table: "Products",
                type: "int",
                maxLength: 5,
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 5,
                oldNullable: true,
                oldDefaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Score",
                table: "Products",
                type: "int",
                maxLength: 5,
                nullable: true,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 5,
                oldDefaultValue: 0);
        }
    }
}
