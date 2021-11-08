using Microsoft.EntityFrameworkCore.Migrations;

namespace WebASPCorePremiereApp.Migrations
{
    public partial class Auteur : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Auteur",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auteur", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuteurLivre",
                columns: table => new
                {
                    AuteursId = table.Column<int>(type: "int", nullable: false),
                    LivresId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuteurLivre", x => new { x.AuteursId, x.LivresId });
                    table.ForeignKey(
                        name: "FK_AuteurLivre_Auteur_AuteursId",
                        column: x => x.AuteursId,
                        principalTable: "Auteur",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuteurLivre_Livres_LivresId",
                        column: x => x.LivresId,
                        principalTable: "Livres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuteurLivre_LivresId",
                table: "AuteurLivre",
                column: "LivresId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuteurLivre");

            migrationBuilder.DropTable(
                name: "Auteur");
        }
    }
}
