using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "adulto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sexo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaNac = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Edad = table.Column<int>(type: "int", nullable: false),
                    EstadoCivil = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discapacidad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Domicilio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<int>(type: "int", nullable: false),
                    AdultoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_adulto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_adulto_adulto_AdultoId",
                        column: x => x.AdultoId,
                        principalTable: "adulto",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "fichaEnf",
                columns: table => new
                {
                    CodEnf = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PresionArterial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FrecuenciaCardiaca = table.Column<int>(type: "int", nullable: false),
                    FrecuenciaRespiratoria = table.Column<int>(type: "int", nullable: false),
                    Pulso = table.Column<float>(type: "real", nullable: false),
                    Temperatura = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tratamiento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PesoTalla = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdAdulto = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdultoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fichaEnf", x => x.CodEnf);
                    table.ForeignKey(
                        name: "FK_fichaEnf_adulto_AdultoId",
                        column: x => x.AdultoId,
                        principalTable: "adulto",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "fichaOri",
                columns: table => new
                {
                    CodOri = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaOrientacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoOrientacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdAdulto = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdultoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fichaOri", x => x.CodOri);
                    table.ForeignKey(
                        name: "FK_fichaOri_adulto_AdultoId",
                        column: x => x.AdultoId,
                        principalTable: "adulto",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "fichaPro",
                columns: table => new
                {
                    CodPro = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaProteccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoProteccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdAdulto = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdultoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fichaPro", x => x.CodPro);
                    table.ForeignKey(
                        name: "FK_fichaPro_adulto_AdultoId",
                        column: x => x.AdultoId,
                        principalTable: "adulto",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_adulto_AdultoId",
                table: "adulto",
                column: "AdultoId");

            migrationBuilder.CreateIndex(
                name: "IX_fichaEnf_AdultoId",
                table: "fichaEnf",
                column: "AdultoId");

            migrationBuilder.CreateIndex(
                name: "IX_fichaOri_AdultoId",
                table: "fichaOri",
                column: "AdultoId");

            migrationBuilder.CreateIndex(
                name: "IX_fichaPro_AdultoId",
                table: "fichaPro",
                column: "AdultoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "fichaEnf");

            migrationBuilder.DropTable(
                name: "fichaOri");

            migrationBuilder.DropTable(
                name: "fichaPro");

            migrationBuilder.DropTable(
                name: "adulto");
        }
    }
}
