using Microsoft.EntityFrameworkCore.Migrations;

namespace WebASPCorePremiereApp.Migrations
{
    public partial class AuteurLivre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuteurLivre_Auteur_AuteursId",
                table: "AuteurLivre");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Auteur",
                table: "Auteur");

            migrationBuilder.RenameTable(
                name: "Auteur",
                newName: "Auteurs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Auteurs",
                table: "Auteurs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AuteurLivre_Auteurs_AuteursId",
                table: "AuteurLivre",
                column: "AuteursId",
                principalTable: "Auteurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuteurLivre_Auteurs_AuteursId",
                table: "AuteurLivre");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Auteurs",
                table: "Auteurs");

            migrationBuilder.RenameTable(
                name: "Auteurs",
                newName: "Auteur");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Auteur",
                table: "Auteur",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AuteurLivre_Auteur_AuteursId",
                table: "AuteurLivre",
                column: "AuteursId",
                principalTable: "Auteur",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
