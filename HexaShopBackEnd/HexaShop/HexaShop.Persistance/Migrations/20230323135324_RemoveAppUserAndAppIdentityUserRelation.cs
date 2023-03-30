using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HexaShop.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAppUserAndAppIdentityUserRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_AspNetUsers_AppIdentityUserId",
                table: "AppUsers");

            migrationBuilder.DropIndex(
                name: "IX_AppUsers_AppIdentityUserId",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "AppIdentityUserId",
                table: "AppUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "AppIdentityUserId",
                table: "AppUsers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_AppIdentityUserId",
                table: "AppUsers",
                column: "AppIdentityUserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_AspNetUsers_AppIdentityUserId",
                table: "AppUsers",
                column: "AppIdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
