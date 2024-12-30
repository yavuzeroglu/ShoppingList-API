using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingList.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class BasketProperties1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BasketName",
                table: "Baskets",
                newName: "Name");
        }
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
