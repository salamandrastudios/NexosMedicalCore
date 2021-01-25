using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NexosMedical.Api.Migrations
{
    public partial class InicioDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NombreCompleto = table.Column<string>(maxLength: 100, nullable: false),
                    Especialidad = table.Column<string>(maxLength: 50, nullable: false),
                    NumeroCredencial = table.Column<int>(maxLength: 25, nullable: false),
                    HospitalDondeTrabaja = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pacientes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NombreCompleto = table.Column<string>(maxLength: 100, nullable: false),
                    NumeroSeguroSocial = table.Column<int>(maxLength: 25, nullable: false),
                    CodigoPostal = table.Column<int>(maxLength: 10, nullable: false),
                    TelefonoContacto = table.Column<string>(maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CitasMedicas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FechaCita = table.Column<DateTime>(nullable: false),
                    PacienteId = table.Column<int>(nullable: true),
                    DoctorId = table.Column<int>(nullable: true),
                    Notas = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CitasMedicas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CitasMedicas_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CitasMedicas_Pacientes_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CitasMedicas_DoctorId",
                table: "CitasMedicas",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_CitasMedicas_PacienteId",
                table: "CitasMedicas",
                column: "PacienteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CitasMedicas");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Pacientes");
        }
    }
}
