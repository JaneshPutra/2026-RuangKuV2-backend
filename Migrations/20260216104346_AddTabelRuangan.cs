using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RuangKuApi.Migrations
{
    /// <inheritdoc />
    public partial class AddTabelRuangan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ruangan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NamaRuangan = table.Column<string>(type: "TEXT", nullable: false),
                    Kapasitas = table.Column<int>(type: "INTEGER", nullable: false),
                    Lokasi = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ruangan", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ruangan");
        }
    }
}
