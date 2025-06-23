using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace clinicadental.Migrations
{
    /// <inheritdoc />
    public partial class addciudadclinica : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Sexo",
                table: "usuario",
                type: "enum('Masculino','Femenino')",
                nullable: false,
                collation: "utf8mb3_general_ci",
                oldClrType: typeof(string),
                oldType: "enum('Hombre','Mujer')")
                .Annotation("MySql:CharSet", "utf8mb3")
                .OldAnnotation("MySql:CharSet", "utf8mb3")
                .OldAnnotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "Sexo",
                table: "paciente",
                type: "enum('Masculino','Femenino')",
                nullable: false,
                collation: "utf8mb3_general_ci",
                oldClrType: typeof(string),
                oldType: "enum('Hombre','Mujer')")
                .Annotation("MySql:CharSet", "utf8mb3")
                .OldAnnotation("MySql:CharSet", "utf8mb3")
                .OldAnnotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "Ciudad",
                table: "clinica",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true,
                collation: "utf8mb3_general_ci")
                .Annotation("MySql:CharSet", "utf8mb3");

            migrationBuilder.AddColumn<string>(
                name: "Pais",
                table: "clinica",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                collation: "utf8mb3_general_ci")
                .Annotation("MySql:CharSet", "utf8mb3");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ciudad",
                table: "clinica");

            migrationBuilder.DropColumn(
                name: "Pais",
                table: "clinica");

            migrationBuilder.AlterColumn<string>(
                name: "Sexo",
                table: "usuario",
                type: "enum('Hombre','Mujer')",
                nullable: false,
                collation: "utf8mb3_general_ci",
                oldClrType: typeof(string),
                oldType: "enum('Masculino','Femenino')")
                .Annotation("MySql:CharSet", "utf8mb3")
                .OldAnnotation("MySql:CharSet", "utf8mb3")
                .OldAnnotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "Sexo",
                table: "paciente",
                type: "enum('Hombre','Mujer')",
                nullable: false,
                collation: "utf8mb3_general_ci",
                oldClrType: typeof(string),
                oldType: "enum('Masculino','Femenino')")
                .Annotation("MySql:CharSet", "utf8mb3")
                .OldAnnotation("MySql:CharSet", "utf8mb3")
                .OldAnnotation("Relational:Collation", "utf8mb3_general_ci");
        }
    }
}
