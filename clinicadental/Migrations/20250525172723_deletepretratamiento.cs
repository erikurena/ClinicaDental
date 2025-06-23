using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace clinicadental.Migrations
{
    /// <inheritdoc />
    public partial class deletepretratamiento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_pagostratamiento_presupuestotratamiento1",
                table: "pagostratamiento");

            migrationBuilder.DropTable(
                name: "presupuestotratamiento");

            migrationBuilder.RenameColumn(
                name: "IdpresupuestoTratamiento",
                table: "pagostratamiento",
                newName: "IdTratamiento");

            migrationBuilder.RenameIndex(
                name: "fk_pagostratamiento_presupuestotratamiento1_idx",
                table: "pagostratamiento",
                newName: "fk_pagostratamiento_tratamiento1_idx");

            migrationBuilder.AddColumn<decimal>(
                name: "ACuenta",
                table: "tratamiento",
                type: "decimal(10)",
                precision: 10,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PresupuestoTotal",
                table: "tratamiento",
                type: "decimal(10)",
                precision: 10,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Saldo",
                table: "tratamiento",
                type: "decimal(10)",
                precision: 10,
                nullable: false,
                defaultValue: 0m);

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
                name: "fk_pagostratamiento_tratamiento1",
                table: "pagostratamiento",
                column: "IdTratamiento",
                principalTable: "tratamiento",
                principalColumn: "IdTratamiento");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_pagostratamiento_tratamiento1",
                table: "pagostratamiento");

            migrationBuilder.DropColumn(
                name: "ACuenta",
                table: "tratamiento");

            migrationBuilder.DropColumn(
                name: "PresupuestoTotal",
                table: "tratamiento");

            migrationBuilder.DropColumn(
                name: "Saldo",
                table: "tratamiento");

            migrationBuilder.RenameColumn(
                name: "IdTratamiento",
                table: "pagostratamiento",
                newName: "IdpresupuestoTratamiento");

            migrationBuilder.RenameIndex(
                name: "fk_pagostratamiento_tratamiento1_idx",
                table: "pagostratamiento",
                newName: "fk_pagostratamiento_presupuestotratamiento1_idx");

            migrationBuilder.AlterColumn<decimal>(
                name: "Monto",
                table: "pagostratamiento",
                type: "decimal(10,30)",
                precision: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10)",
                oldPrecision: 10);

            migrationBuilder.CreateTable(
                name: "presupuestotratamiento",
                columns: table => new
                {
                    IdPresupuestoTratamiento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdTratamiento = table.Column<int>(type: "int", nullable: false),
                    ACuenta = table.Column<decimal>(type: "decimal(10,30)", precision: 10, nullable: false),
                    PresupuestoTotal = table.Column<decimal>(type: "decimal(10,30)", precision: 10, nullable: false),
                    Saldo = table.Column<decimal>(type: "decimal(10,30)", precision: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.IdPresupuestoTratamiento);
                    table.ForeignKey(
                        name: "fk_presupuestotratamiento_tratamiento1",
                        column: x => x.IdTratamiento,
                        principalTable: "tratamiento",
                        principalColumn: "IdTratamiento");
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateIndex(
                name: "fk_presupuestotratamiento_tratamiento1_idx",
                table: "presupuestotratamiento",
                column: "IdTratamiento");

            migrationBuilder.AddForeignKey(
                name: "fk_pagostratamiento_presupuestotratamiento1",
                table: "pagostratamiento",
                column: "IdpresupuestoTratamiento",
                principalTable: "presupuestotratamiento",
                principalColumn: "IdPresupuestoTratamiento");
        }
    }
}
