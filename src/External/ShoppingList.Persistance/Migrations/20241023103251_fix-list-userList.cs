using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingList.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class fixlistuserList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lists_AspNetUsers_CreatedByUserId1",
                table: "Lists");

            migrationBuilder.DropIndex(
                name: "IX_Lists_CreatedByUserId1",
                table: "Lists");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ListUser");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId1",
                table: "Lists");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedByUserId",
                table: "Lists",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 10, 23, 10, 32, 50, 855, DateTimeKind.Utc).AddTicks(7517));

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 10, 23, 10, 32, 50, 855, DateTimeKind.Utc).AddTicks(7520));

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 10, 23, 10, 32, 50, 855, DateTimeKind.Utc).AddTicks(7521));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 10, 23, 10, 32, 50, 856, DateTimeKind.Utc).AddTicks(1856));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 10, 23, 10, 32, 50, 856, DateTimeKind.Utc).AddTicks(1863));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 10, 23, 10, 32, 50, 856, DateTimeKind.Utc).AddTicks(1864));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 10, 23, 10, 32, 50, 856, DateTimeKind.Utc).AddTicks(1865));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 10, 23, 10, 32, 50, 856, DateTimeKind.Utc).AddTicks(1866));

            migrationBuilder.CreateIndex(
                name: "IX_Lists_CreatedByUserId",
                table: "Lists",
                column: "CreatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lists_AspNetUsers_CreatedByUserId",
                table: "Lists",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lists_AspNetUsers_CreatedByUserId",
                table: "Lists");

            migrationBuilder.DropIndex(
                name: "IX_Lists_CreatedByUserId",
                table: "Lists");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ListUser",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreatedByUserId",
                table: "Lists",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId1",
                table: "Lists",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 10, 22, 15, 10, 29, 678, DateTimeKind.Utc).AddTicks(4652));

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 10, 22, 15, 10, 29, 678, DateTimeKind.Utc).AddTicks(4655));

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 10, 22, 15, 10, 29, 678, DateTimeKind.Utc).AddTicks(4657));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 10, 22, 15, 10, 29, 679, DateTimeKind.Utc).AddTicks(724));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 10, 22, 15, 10, 29, 679, DateTimeKind.Utc).AddTicks(729));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 10, 22, 15, 10, 29, 679, DateTimeKind.Utc).AddTicks(731));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 10, 22, 15, 10, 29, 679, DateTimeKind.Utc).AddTicks(732));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 10, 22, 15, 10, 29, 679, DateTimeKind.Utc).AddTicks(734));

            migrationBuilder.CreateIndex(
                name: "IX_Lists_CreatedByUserId1",
                table: "Lists",
                column: "CreatedByUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Lists_AspNetUsers_CreatedByUserId1",
                table: "Lists",
                column: "CreatedByUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
