using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExoAPI.Migrations
{
    public partial class AddEntrepotTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EntrepotId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Entrepots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Longitude = table.Column<double>(type: "double", nullable: false),
                    Latitude = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entrepots", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Products_EntrepotId",
                table: "Products",
                column: "EntrepotId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Entrepots_EntrepotId",
                table: "Products",
                column: "EntrepotId",
                principalTable: "Entrepots",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Entrepots_EntrepotId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Entrepots");

            migrationBuilder.DropIndex(
                name: "IX_Products_EntrepotId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "EntrepotId",
                table: "Products");
        }
    }
}
