using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PizzaApi.Migrations
{
    /// <inheritdoc />
    public partial class Pizzas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "pizzas",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    stars = table.Column<int>(type: "integer", nullable: false),
                    price = table.Column<decimal>(type: "numeric", nullable: false),
                    spicy_level_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pizzas", x => x.id);
                    table.ForeignKey(
                        name: "fk_pizzas_spicy_levels_spicy_level_id",
                        column: x => x.spicy_level_id,
                        principalTable: "spicy_levels",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "pizzas",
                columns: new[] { "id", "name", "price", "spicy_level_id", "stars" },
                values: new object[,]
                {
                    { 1, "Margherita", 12.12m, 1, 4 },
                    { 2, "Fungi", 12.34m, 1, 5 },
                    { 3, "4 sery", 22.32m, 2, 5 },
                    { 4, "Piekielne papryczki", 72.44m, 4, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "ix_pizzas_spicy_level_id",
                table: "pizzas",
                column: "spicy_level_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pizzas");
        }
    }
}
