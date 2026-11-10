using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PizzaApi.Migrations
{
    /// <inheritdoc />
    public partial class UserRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "my_role_id",
                table: "users",
                type: "integer",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateTable(
                name: "user_roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(42)", maxLength: 42, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_roles", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "user_roles",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Customer" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_users_id",
                table: "users",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_users_my_role_id",
                table: "users",
                column: "my_role_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_roles_id",
                table: "user_roles",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_users_user_roles_my_role_id",
                table: "users",
                column: "my_role_id",
                principalTable: "user_roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_users_user_roles_my_role_id",
                table: "users");

            migrationBuilder.DropTable(
                name: "user_roles");

            migrationBuilder.DropIndex(
                name: "ix_users_id",
                table: "users");

            migrationBuilder.DropIndex(
                name: "ix_users_my_role_id",
                table: "users");

            migrationBuilder.DropColumn(
                name: "my_role_id",
                table: "users");
        }
    }
}
