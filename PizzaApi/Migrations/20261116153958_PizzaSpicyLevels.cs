using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PizzaApi.Migrations
{
    /// <inheritdoc />
    public partial class PizzaSpicyLevels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "spicy_level",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    description = table.Column<string>(type: "text", nullable: false),
                    lvl = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_spicy_level", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "spicy_level",
                columns: new[] { "id", "description", "lvl" },
                values: new object[,]
                {
                    { 1, "Łagodny jak Szczerbuś :)", 0 },
                    { 2, "Lekko ostra", 1 },
                    { 3, "Średnio ostra", 2 },
                    { 4, "Bardzo ostra >.<", 5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "spicy_level");
        }
    }
}
