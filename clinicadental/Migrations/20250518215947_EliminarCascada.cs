using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace clinicadental.Migrations
{
    /// <inheritdoc />
    public partial class EliminarCascada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {                  

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

            migrationBuilder.AlterColumn<string>(
                name: "IdLugarNacimiento",
                table: "paciente",
                type: "varchar(45)",
                maxLength: 45,
                nullable: true,
                collation: "utf8mb3_general_ci",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb3");

            migrationBuilder.AlterColumn<string>(
                name: "IdGradoInstruccion",
                table: "paciente",
                type: "enum('Inicial','Primaria', 'Secundaria', 'Universidad', 'Tecnico', 'Profesional', 'Otro')",
                nullable: false,
                collation: "utf8mb3_general_ci",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb3");

            migrationBuilder.AlterColumn<string>(
                name: "IdEstadoCivil",
                table: "paciente",
                type: "enum('Soltero', 'Casado', 'Divorciado', 'Viudo', 'Concubino', 'Otro')",
                nullable: false,
                collation: "utf8mb3_general_ci",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb3");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdRol",
                table: "usuario",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.AlterColumn<int>(
                name: "IdLugarNacimiento",
                table: "paciente",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45,
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb3")
                .OldAnnotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.AlterColumn<int>(
                name: "IdGradoInstruccion",
                table: "paciente",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "enum('Inicial','Primaria', 'Secundaria', 'Universidad', 'Tecnico', 'Profesional', 'Otro')")
                .OldAnnotation("MySql:CharSet", "utf8mb3")
                .OldAnnotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.AlterColumn<int>(
                name: "IdEstadoCivil",
                table: "paciente",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "enum('Soltero', 'Casado', 'Divorciado', 'Viudo', 'Concubino', 'Otro')")
                .OldAnnotation("MySql:CharSet", "utf8mb3")
                .OldAnnotation("Relational:Collation", "utf8mb3_general_ci");

            //migrationBuilder.CreateTable(
            //    name: "cuenta",
            //    columns: table => new
            //    {
            //        IdCuenta = table.Column<int>(type: "int", nullable: false),
            //        Cuenta = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb3_general_ci")
            //            .Annotation("MySql:CharSet", "utf8mb3"),
            //        IdUsuario = table.Column<int>(type: "int", nullable: false),
            //        Password = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb3_general_ci")
            //            .Annotation("MySql:CharSet", "utf8mb3")
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PRIMARY", x => x.IdCuenta);
            //        table.ForeignKey(
            //            name: "fk_cuenta_usuario1",
            //            column: x => x.IdCuenta,
            //            principalTable: "usuario",
            //            principalColumn: "IdUsuario",
            //            onDelete: ReferentialAction.Cascade);
            //    })
            //    .Annotation("MySql:CharSet", "utf8mb3")
            //    .Annotation("Relational:Collation", "utf8mb3_general_ci");

            //migrationBuilder.CreateTable(
            //    name: "estadocivil",
            //    columns: table => new
            //    {
            //        IdEstadoCivil = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            //        EstadoCivil = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb3_general_ci")
            //            .Annotation("MySql:CharSet", "utf8mb3")
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PRIMARY", x => x.IdEstadoCivil);
            //    })
            //    .Annotation("MySql:CharSet", "utf8mb3")
            //    .Annotation("Relational:Collation", "utf8mb3_general_ci");

            //migrationBuilder.CreateTable(
            //    name: "gradoinstruccion",
            //    columns: table => new
            //    {
            //        IdGradoInstruccion = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            //        GradoInstruccion = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb3_general_ci")
            //            .Annotation("MySql:CharSet", "utf8mb3")
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PRIMARY", x => x.IdGradoInstruccion);
            //    })
            //    .Annotation("MySql:CharSet", "utf8mb3")
            //    .Annotation("Relational:Collation", "utf8mb3_general_ci");

            //migrationBuilder.CreateTable(
            //    name: "lugarnacimiento",
            //    columns: table => new
            //    {
            //        IdLugarNacimiento = table.Column<int>(type: "int", nullable: false),
            //        LugarNacimiento = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb3_general_ci")
            //            .Annotation("MySql:CharSet", "utf8mb3")
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PRIMARY", x => x.IdLugarNacimiento);
            //    })
            //    .Annotation("MySql:CharSet", "utf8mb3")
            //    .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "rol",
                columns: table => new
                {
                    IdRol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TipoRol = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.IdRol);
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");


          


        }
    }
}
