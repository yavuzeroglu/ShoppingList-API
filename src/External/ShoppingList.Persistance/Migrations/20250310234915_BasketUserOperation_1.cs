using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingList.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class BasketUserOperation_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketUser_AspNetUsers_AppUserId",
                table: "BasketUser");

            migrationBuilder.DropForeignKey(
                name: "FK_BasketUser_Baskets_BasketId",
                table: "BasketUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BasketUser",
                table: "BasketUser");

            migrationBuilder.RenameTable(
                name: "BasketUser",
                newName: "BasketUsers");

            migrationBuilder.RenameIndex(
                name: "IX_BasketUser_BasketId",
                table: "BasketUsers",
                newName: "IX_BasketUsers_BasketId");

            migrationBuilder.RenameIndex(
                name: "IX_BasketUser_AppUserId",
                table: "BasketUsers",
                newName: "IX_BasketUsers_AppUserId");

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "BasketUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BasketUsers",
                table: "BasketUsers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketUsers_AspNetUsers_AppUserId",
                table: "BasketUsers",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketUsers_Baskets_BasketId",
                table: "BasketUsers",
                column: "BasketId",
                principalTable: "Baskets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketUsers_AspNetUsers_AppUserId",
                table: "BasketUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_BasketUsers_Baskets_BasketId",
                table: "BasketUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BasketUsers",
                table: "BasketUsers");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "BasketUsers");

            migrationBuilder.RenameTable(
                name: "BasketUsers",
                newName: "BasketUser");

            migrationBuilder.RenameIndex(
                name: "IX_BasketUsers_BasketId",
                table: "BasketUser",
                newName: "IX_BasketUser_BasketId");

            migrationBuilder.RenameIndex(
                name: "IX_BasketUsers_AppUserId",
                table: "BasketUser",
                newName: "IX_BasketUser_AppUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BasketUser",
                table: "BasketUser",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketUser_AspNetUsers_AppUserId",
                table: "BasketUser",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketUser_Baskets_BasketId",
                table: "BasketUser",
                column: "BasketId",
                principalTable: "Baskets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
