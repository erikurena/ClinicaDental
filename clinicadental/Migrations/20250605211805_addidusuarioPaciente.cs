using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace clinicadental.Migrations
{
    /// <inheritdoc />
    public partial class addidusuarioPaciente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "FechaPago",
                table: "pagostratamiento",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<int>(
                name: "IdUsuario",
                table: "paciente",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdUsuario",
                table: "paciente");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "FechaPago",
                table: "pagostratamiento",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);
        }
    }
}
