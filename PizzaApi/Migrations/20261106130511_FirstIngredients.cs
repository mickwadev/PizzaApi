using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PizzaApi.Migrations
{
    /// <inheritdoc />
    public partial class FirstIngredients : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ingredients",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "żółty ser" },
                    { 2, "sos pomidorowy" },
                    { 3, "pieczarki" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ingredients",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ingredients",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ingredients",
                keyColumn: "id",
                keyValue: 3);
        }
    }
}
