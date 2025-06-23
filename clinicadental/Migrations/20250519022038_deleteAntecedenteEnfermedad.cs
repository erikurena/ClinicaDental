using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace clinicadental.Migrations
{
    /// <inheritdoc />
    public partial class deleteAntecedenteEnfermedad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_antecedenteenfermedad_antecedentepatologico1",
                table: "antecedenteenfermedad");

            migrationBuilder.AlterColumn<decimal>(
                name: "Saldo",
                table: "presupuestotratamiento",
                type: "decimal(10)",
                precision: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,30)",
                oldPrecision: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "PresupuestoTotal",
                table: "presupuestotratamiento",
                type: "decimal(10)",
                precision: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,30)",
                oldPrecision: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "ACuenta",
                table: "presupuestotratamiento",
                type: "decimal(10)",
                precision: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,30)",
                oldPrecision: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "Monto",
                table: "pagostratamiento",
                type: "decimal(10)",
                precision: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,30)",
                oldPrecision: 10);

            migrationBuilder.AddForeignKey(
                name: "fk_antecedenteenfermedad_antecedentepatologico1",
                table: "antecedenteenfermedad",
                column: "IdAntecedentePatologico",
                principalTable: "antecedentepatologico",
                principalColumn: "IdAntecedentePatologico",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_antecedenteenfermedad_antecedentepatologico1",
                table: "antecedenteenfermedad");

            migrationBuilder.AlterColumn<decimal>(
                name: "Saldo",
                table: "presupuestotratamiento",
                type: "decimal(10,30)",
                precision: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10)",
                oldPrecision: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "PresupuestoTotal",
                table: "presupuestotratamiento",
                type: "decimal(10,30)",
                precision: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10)",
                oldPrecision: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "ACuenta",
                table: "presupuestotratamiento",
                type: "decimal(10,30)",
                precision: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10)",
                oldPrecision: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "Monto",
                table: "pagostratamiento",
                type: "decimal(10,30)",
                precision: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10)",
                oldPrecision: 10);

            migrationBuilder.AddForeignKey(
                name: "fk_antecedenteenfermedad_antecedentepatologico1",
                table: "antecedenteenfermedad",
                column: "IdAntecedentePatologico",
                principalTable: "antecedentepatologico",
                principalColumn: "IdAntecedentePatologico");
        }
    }
}
