using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSharpExpressionsExamples.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "producers",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_producers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ships",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    unique_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: true),
                    Active = table.Column<bool>(type: "INTEGER", nullable: false),
                    launched_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    number_guns = table.Column<int>(type: "INTEGER", nullable: false),
                    bore = table.Column<int>(type: "INTEGER", nullable: false),
                    normal_displacement = table.Column<decimal>(type: "TEXT", nullable: false),
                    country_id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ships", x => x.id);
                    table.ForeignKey(
                        name: "FK_ships_producers_country_id",
                        column: x => x.country_id,
                        principalTable: "producers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_producers_name",
                table: "producers",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_ship_name",
                table: "ships",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ships_country_id",
                table: "ships",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "ix_unique_id",
                table: "ships",
                column: "unique_id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ships");

            migrationBuilder.DropTable(
                name: "producers");
        }
    }
}
