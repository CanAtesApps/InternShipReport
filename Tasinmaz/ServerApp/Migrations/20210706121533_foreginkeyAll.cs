using Microsoft.EntityFrameworkCore.Migrations;

namespace ServerApp.Migrations
{
    public partial class foreginkeyAll : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Tasinmaz_MahalleID",
                table: "Tasinmaz",
                column: "MahalleID");

            migrationBuilder.CreateIndex(
                name: "IX_Mahalle_CountyID",
                table: "Mahalle",
                column: "CountyID");

            migrationBuilder.AddForeignKey(
                name: "FK_Mahalle_Ilce_CountyID",
                table: "Mahalle",
                column: "CountyID",
                principalTable: "Ilce",
                principalColumn: "CountyID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasinmaz_Mahalle_MahalleID",
                table: "Tasinmaz",
                column: "MahalleID",
                principalTable: "Mahalle",
                principalColumn: "AreaID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mahalle_Ilce_CountyID",
                table: "Mahalle");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasinmaz_Mahalle_MahalleID",
                table: "Tasinmaz");

            migrationBuilder.DropIndex(
                name: "IX_Tasinmaz_MahalleID",
                table: "Tasinmaz");

            migrationBuilder.DropIndex(
                name: "IX_Mahalle_CountyID",
                table: "Mahalle");
        }
    }
}
