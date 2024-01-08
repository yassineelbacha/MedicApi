using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicApi.Migrations
{
    public partial class nouveau : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Personnes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    DateNaissance = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotDePasse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adresse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personnes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Appoins",
                columns: table => new
                {
                    Rid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Jour = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Heure = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Urgence = table.Column<bool>(type: "bit", nullable: false),
                    PersonneId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appoins", x => x.Rid);
                    table.ForeignKey(
                        name: "FK_Appoins_Personnes_PersonneId",
                        column: x => x.PersonneId,
                        principalTable: "Personnes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DossierMedicals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descriptions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Conclusion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Medicaments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Certificats = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PersonneId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DossierMedicals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DossierMedicals_Personnes_PersonneId",
                        column: x => x.PersonneId,
                        principalTable: "Personnes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Travails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrHeure = table.Column<int>(type: "int", nullable: false),
                    Jours = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Conge = table.Column<bool>(type: "bit", nullable: false),
                    PersonneId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Travails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Travails_Personnes_PersonneId",
                        column: x => x.PersonneId,
                        principalTable: "Personnes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appoins_PersonneId",
                table: "Appoins",
                column: "PersonneId");

            migrationBuilder.CreateIndex(
                name: "IX_DossierMedicals_PersonneId",
                table: "DossierMedicals",
                column: "PersonneId");

            migrationBuilder.CreateIndex(
                name: "IX_Travails_PersonneId",
                table: "Travails",
                column: "PersonneId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appoins");

            migrationBuilder.DropTable(
                name: "DossierMedicals");

            migrationBuilder.DropTable(
                name: "Travails");

            migrationBuilder.DropTable(
                name: "Personnes");
        }
    }
}
