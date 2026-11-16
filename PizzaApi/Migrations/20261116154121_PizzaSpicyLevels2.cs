using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzaApi.Migrations
{
    /// <inheritdoc />
    public partial class PizzaSpicyLevels2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_spicy_level",
                table: "spicy_level");

            migrationBuilder.RenameTable(
                name: "spicy_level",
                newName: "spicy_levels");

            migrationBuilder.AddPrimaryKey(
                name: "pk_spicy_levels",
                table: "spicy_levels",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_spicy_levels",
                table: "spicy_levels");

            migrationBuilder.RenameTable(
                name: "spicy_levels",
                newName: "spicy_level");

            migrationBuilder.AddPrimaryKey(
                name: "pk_spicy_level",
                table: "spicy_level",
                column: "id");
        }
    }
}
