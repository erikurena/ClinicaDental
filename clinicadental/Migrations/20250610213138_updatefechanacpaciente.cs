using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace clinicadental.Migrations
{
    /// <inheritdoc />
    public partial class updatefechanacpaciente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaNacimiento",
                table: "paciente");

            migrationBuilder.AddColumn<DateOnly>(
                name: "FechaNacimientoPaciente",
                table: "paciente",
                type: "date",
                maxLength: 45,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaNacimientoPaciente",
                table: "paciente");

            migrationBuilder.AddColumn<DateOnly>(
                name: "FechaNacimiento",
                table: "paciente",
                type: "date",
                nullable: true);
        }
    }
}
