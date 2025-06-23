using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace clinicadental.Migrations
{
    /// <inheritdoc />
    public partial class addfechascreaciontablas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "FechaNacimiento",
                table: "usuario",
                type: "date",
                maxLength: 45,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45,
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb3")
                .OldAnnotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacionUsuario",
                table: "usuario",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacionUsuario",
                table: "usuario",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacionTratamiento",
                table: "tratamiento",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacionTratamiento",
                table: "tratamiento",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacionPaciente",
                table: "paciente",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacionPaciente",
                table: "paciente",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacionHistorial",
                table: "historialclinico",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacionHistorial",
                table: "historialclinico",
                type: "datetime(6)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaCreacionUsuario",
                table: "usuario");

            migrationBuilder.DropColumn(
                name: "FechaModificacionUsuario",
                table: "usuario");

            migrationBuilder.DropColumn(
                name: "FechaCreacionTratamiento",
                table: "tratamiento");

            migrationBuilder.DropColumn(
                name: "FechaModificacionTratamiento",
                table: "tratamiento");

            migrationBuilder.DropColumn(
                name: "FechaCreacionPaciente",
                table: "paciente");

            migrationBuilder.DropColumn(
                name: "FechaModificacionPaciente",
                table: "paciente");

            migrationBuilder.DropColumn(
                name: "FechaCreacionHistorial",
                table: "historialclinico");

            migrationBuilder.DropColumn(
                name: "FechaModificacionHistorial",
                table: "historialclinico");

            migrationBuilder.AlterColumn<string>(
                name: "FechaNacimiento",
                table: "usuario",
                type: "varchar(45)",
                maxLength: 45,
                nullable: true,
                collation: "utf8mb3_general_ci",
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldMaxLength: 45,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb3");
        }
    }
}
