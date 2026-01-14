using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class Inicial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "fichaFis",
                columns: table => new
                {
                    CodFis = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FechaProgramacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumeroSesiones = table.Column<int>(type: "int", nullable: false),
                    MotivoConsulta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EquiposEmpleados = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdAdulto = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdultoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fichaFis", x => x.CodFis);
                    table.ForeignKey(
                        name: "FK_fichaFis_adulto_AdultoId",
                        column: x => x.AdultoId,
                        principalTable: "adulto",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_fichaFis_AdultoId",
                table: "fichaFis",
                column: "AdultoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "fichaFis");
        }
    }
}
