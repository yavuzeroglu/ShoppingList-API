using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShoppingList.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class EntityImprovementsWithoutDummyData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "Baskets",
                newName: "ModifiedDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Products",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "EstimatedPrice",
                table: "Products",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Products",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Products",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Unit",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Images",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Images",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Images",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Images",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Categories",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Categories",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Categories",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Categories",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Brands",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Brands",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Brands",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "BasketUser",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "BasketUser",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "BasketUser",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "BasketUser",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Baskets",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Baskets",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Baskets",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PurchasedDate",
                table: "Baskets",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "AssignedUserId",
                table: "BasketItems",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "BasketItems",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "BasketItems",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "BasketItems",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPurchased",
                table: "BasketItems",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "BasketItems",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_AssignedUserId",
                table: "BasketItems",
                column: "AssignedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItems_AspNetUsers_AssignedUserId",
                table: "BasketItems",
                column: "AssignedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketItems_AspNetUsers_AssignedUserId",
                table: "BasketItems");

            migrationBuilder.DropIndex(
                name: "IX_BasketItems_AssignedUserId",
                table: "BasketItems");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "EstimatedPrice",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "BasketUser");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "BasketUser");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "BasketUser");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "BasketUser");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Baskets");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Baskets");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "Baskets");

            migrationBuilder.DropColumn(
                name: "PurchasedDate",
                table: "Baskets");

            migrationBuilder.DropColumn(
                name: "AssignedUserId",
                table: "BasketItems");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "BasketItems");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "BasketItems");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "BasketItems");

            migrationBuilder.DropColumn(
                name: "IsPurchased",
                table: "BasketItems");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "BasketItems");

            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "Baskets",
                newName: "UpdatedDate");

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "CreatedDate", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 25, 14, 51, 34, 426, DateTimeKind.Utc).AddTicks(2250), "TEST" },
                    { 2, new DateTime(2024, 10, 25, 14, 51, 34, 426, DateTimeKind.Utc).AddTicks(2253), "Reyoncunuz" },
                    { 3, new DateTime(2024, 10, 25, 14, 51, 34, 426, DateTimeKind.Utc).AddTicks(2255), "ETİ" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "ParentCategoryId" },
                values: new object[,]
                {
                    { 1, "Temel Gıda", null },
                    { 2, "Meyve Sebze", null },
                    { 8, "İçecek", null },
                    { 3, "Meyve", 2 },
                    { 9, "Gazlı İçecek", 8 },
                    { 10, "Gazsız İçecek", 8 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "CategoryId", "CreatedDate", "IsActive", "Name" },
                values: new object[] { 5, 1, 1, new DateTime(2024, 10, 25, 14, 51, 34, 426, DateTimeKind.Utc).AddTicks(8442), false, "Tarhana" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "ParentCategoryId" },
                values: new object[,]
                {
                    { 4, "Doğranmış, Ayıklanmış Meyveler", 3 },
                    { 5, "Egzotik Meyveler", 3 },
                    { 6, "Kavun ve Karpuz", 3 },
                    { 7, "Narenciye", 3 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "CategoryId", "CreatedDate", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, 1, 7, new DateTime(2024, 10, 25, 14, 51, 34, 426, DateTimeKind.Utc).AddTicks(8434), false, "Portakal" },
                    { 2, 1, 7, new DateTime(2024, 10, 25, 14, 51, 34, 426, DateTimeKind.Utc).AddTicks(8439), false, "Greyfurt" },
                    { 3, 1, 6, new DateTime(2024, 10, 25, 14, 51, 34, 426, DateTimeKind.Utc).AddTicks(8440), false, "Kavun" },
                    { 4, 1, 6, new DateTime(2024, 10, 25, 14, 51, 34, 426, DateTimeKind.Utc).AddTicks(8441), false, "Karpuz" }
                });
        }
    }
}
