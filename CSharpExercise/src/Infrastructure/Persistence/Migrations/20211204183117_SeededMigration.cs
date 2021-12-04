using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CSharpExercise.src.Infrastructure.Persistence.Migrations
{
    public partial class SeededMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user_info",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    login = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    password = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    first_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    last_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    email = table.Column<string>(type: "varchar(320)", maxLength: 320, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_info", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "user_info",
                columns: new[] { "id", "email", "first_name", "last_name", "login", "password" },
                values: new object[,]
                {
                    { 1, "l.g@oplead.com", "Laurent", "G.", "LGilly", "$2a$11$EyF9ThzYp2z4HPtf71/LVe2B0S9QCMCMSLdsNws6SsR8OQ2V7xOfS" },
                    { 2, "maxime@andre.com", "Maxime", "Andre", "MAndre", "$2a$11$XB2PBbfsLToRUBmcTvFZeekt42z8kNwJjpt0l495GTV0boe7cHoCS" },
                    { 3, "emma@taume.com", "Emma", "Taume", "ETaume", "$2a$11$huEOgHoMQVXPivLtJIsZ6eeCVW1EKZau9exMPPLcvLp5yacLi.1EC" }
                });

            migrationBuilder.CreateIndex(
                name: "id",
                table: "user_info",
                column: "id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_info");
        }
    }
}
