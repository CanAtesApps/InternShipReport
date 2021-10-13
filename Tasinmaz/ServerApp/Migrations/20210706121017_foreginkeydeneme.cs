using Microsoft.EntityFrameworkCore.Migrations;

namespace ServerApp.Migrations
{
    public partial class foreginkeydeneme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Ilce_CityID",
                table: "Ilce",
                column: "CityID");

            migrationBuilder.AddForeignKey(
                name: "FK_Ilce_Il_CityID",
                table: "Ilce",
                column: "CityID",
                principalTable: "Il",
                principalColumn: "CityID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ilce_Il_CityID",
                table: "Ilce");

            migrationBuilder.DropIndex(
                name: "IX_Ilce_CityID",
                table: "Ilce");
        }
    }
}
