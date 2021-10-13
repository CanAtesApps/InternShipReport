using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ServerApp.Migrations
{
    public partial class tasinmazstart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Il",
                columns: table => new
                {
                    CityID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CountryID = table.Column<int>(type: "integer", nullable: false),
                    CityName = table.Column<string>(type: "text", nullable: true),
                    PlateNo = table.Column<string>(type: "text", nullable: true),
                    PhoneCode = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Il", x => x.CityID);
                });

            migrationBuilder.CreateTable(
                name: "Ilce",
                columns: table => new
                {
                    CountyID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CityID = table.Column<int>(type: "integer", nullable: false),
                    CountyName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ilce", x => x.CountyID);
                });

            migrationBuilder.CreateTable(
                name: "Mahalle",
                columns: table => new
                {
                    AreaID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CountyID = table.Column<int>(type: "integer", nullable: false),
                    AreaName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mahalle", x => x.AreaID);
                });

            migrationBuilder.CreateTable(
                name: "Tasinmazs",
                columns: table => new
                {
                    TasinmazID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CityID = table.Column<int>(type: "integer", nullable: false),
                    CountyID = table.Column<int>(type: "integer", nullable: false),
                    MahalleID = table.Column<int>(type: "integer", nullable: false),
                    Ada = table.Column<int>(type: "integer", nullable: false),
                    Parsel = table.Column<int>(type: "integer", nullable: false),
                    Nitelik = table.Column<string>(type: "text", nullable: true),
                    Adres = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasinmazs", x => x.TasinmazID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Il");

            migrationBuilder.DropTable(
                name: "Ilce");

            migrationBuilder.DropTable(
                name: "Mahalle");

            migrationBuilder.DropTable(
                name: "Tasinmazs");
        }
    }
}
