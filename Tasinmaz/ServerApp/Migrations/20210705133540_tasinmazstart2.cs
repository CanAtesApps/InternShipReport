using Microsoft.EntityFrameworkCore.Migrations;

namespace ServerApp.Migrations
{
    public partial class tasinmazstart2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Tasinmazs",
                table: "Tasinmazs");

            migrationBuilder.RenameTable(
                name: "Tasinmazs",
                newName: "Tasinmaz");

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "Tasinmaz",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tasinmaz",
                table: "Tasinmaz",
                column: "TasinmazID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Tasinmaz",
                table: "Tasinmaz");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "Tasinmaz");

            migrationBuilder.RenameTable(
                name: "Tasinmaz",
                newName: "Tasinmazs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tasinmazs",
                table: "Tasinmazs",
                column: "TasinmazID");
        }
    }
}
