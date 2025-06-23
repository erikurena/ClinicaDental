using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace clinicadental.Migrations
{
    /// <inheritdoc />
    public partial class deleteenumdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Sexo",
                table: "usuario",
                type: "int",
                maxLength: 45,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "enum('Masculino','Femenino')")
                .OldAnnotation("MySql:CharSet", "utf8mb3")
                .OldAnnotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.AlterColumn<int>(
                name: "Sexo",
                table: "paciente",
                type: "int",
                maxLength: 45,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "enum('Masculino','Femenino')")
                .OldAnnotation("MySql:CharSet", "utf8mb3")
                .OldAnnotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.AlterColumn<int>(
                name: "IdGradoInstruccion",
                table: "paciente",
                type: "int",
                maxLength: 45,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "enum('Inicial','Primaria', 'Secundaria', 'Universidad', 'Tecnico', 'Profesional', 'Otro')")
                .OldAnnotation("MySql:CharSet", "utf8mb3")
                .OldAnnotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.AlterColumn<int>(
                name: "IdEstadoCivil",
                table: "paciente",
                type: "int",
                maxLength: 45,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "enum('Soltero', 'Casado', 'Divorciado', 'Viudo', 'Concubino', 'Otro')")
                .OldAnnotation("MySql:CharSet", "utf8mb3")
                .OldAnnotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.AlterColumn<int>(
                name: "ProtesisDental",
                table: "examenintraoral",
                type: "int",
                maxLength: 45,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "enum('Si','No')")
                .OldAnnotation("MySql:CharSet", "utf8mb3")
                .OldAnnotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.AlterColumn<int>(
                name: "SangradoEncias",
                table: "antecedentehigieneoral",
                type: "int",
                maxLength: 45,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "enum('Si','No')")
                .OldAnnotation("MySql:CharSet", "utf8mb3")
                .OldAnnotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.AlterColumn<int>(
                name: "IdHiloDental",
                table: "antecedentehigieneoral",
                type: "int",
                maxLength: 45,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "enum('Si','No')")
                .OldAnnotation("MySql:CharSet", "utf8mb3")
                .OldAnnotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.AlterColumn<int>(
                name: "IdHigieneBucal",
                table: "antecedentehigieneoral",
                type: "int",
                maxLength: 45,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "enum('Buena','Regular','Mala')")
                .OldAnnotation("MySql:CharSet", "utf8mb3")
                .OldAnnotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.AlterColumn<int>(
                name: "EnjuagueBucal",
                table: "antecedentehigieneoral",
                type: "int",
                maxLength: 45,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "enum('Si','No')")
                .OldAnnotation("MySql:CharSet", "utf8mb3")
                .OldAnnotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.AlterColumn<int>(
                name: "CepilloDental",
                table: "antecedentehigieneoral",
                type: "int",
                maxLength: 45,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "enum('Si','No')")
                .OldAnnotation("MySql:CharSet", "utf8mb3")
                .OldAnnotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.AlterColumn<int>(
                name: "HemorragiaDental",
                table: "antecedentegeneral",
                type: "int",
                maxLength: 45,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "enum('Si','No')")
                .OldAnnotation("MySql:CharSet", "utf8mb3")
                .OldAnnotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.AlterColumn<int>(
                name: "EspecificacionHemorragia",
                table: "antecedentegeneral",
                type: "int",
                maxLength: 45,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "enum('Inmediata','Mediata','Ninguna')")
                .OldAnnotation("MySql:CharSet", "utf8mb3")
                .OldAnnotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.AlterColumn<int>(
                name: "Embarazo",
                table: "antecedentegeneral",
                type: "int",
                maxLength: 45,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "enum('Si','No')")
                .OldAnnotation("MySql:CharSet", "utf8mb3")
                .OldAnnotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.AlterColumn<int>(
                name: "Alergia",
                table: "antecedentegeneral",
                type: "int",
                maxLength: 45,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "enum('Si','No')")
                .OldAnnotation("MySql:CharSet", "utf8mb3")
                .OldAnnotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.AlterColumn<int>(
                name: "Fuma",
                table: "antecedentebucodental",
                type: "int",
                maxLength: 45,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "enum('Si','No')")
                .OldAnnotation("MySql:CharSet", "utf8mb3")
                .OldAnnotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.AlterColumn<int>(
                name: "Bebe",
                table: "antecedentebucodental",
                type: "int",
                maxLength: 45,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "enum('Si','No')")
                .OldAnnotation("MySql:CharSet", "utf8mb3")
                .OldAnnotation("Relational:Collation", "utf8mb3_general_ci");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Sexo",
                table: "usuario",
                type: "enum('Masculino','Femenino')",
                nullable: false,
                collation: "utf8mb3_general_ci",
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "utf8mb3");

            migrationBuilder.AlterColumn<string>(
                name: "Sexo",
                table: "paciente",
                type: "enum('Masculino','Femenino')",
                nullable: false,
                collation: "utf8mb3_general_ci",
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "utf8mb3");

            migrationBuilder.AlterColumn<string>(
                name: "IdGradoInstruccion",
                table: "paciente",
                type: "enum('Inicial','Primaria', 'Secundaria', 'Universidad', 'Tecnico', 'Profesional', 'Otro')",
                nullable: false,
                collation: "utf8mb3_general_ci",
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "utf8mb3");

            migrationBuilder.AlterColumn<string>(
                name: "IdEstadoCivil",
                table: "paciente",
                type: "enum('Soltero', 'Casado', 'Divorciado', 'Viudo', 'Concubino', 'Otro')",
                nullable: false,
                collation: "utf8mb3_general_ci",
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "utf8mb3");

            migrationBuilder.AlterColumn<string>(
                name: "ProtesisDental",
                table: "examenintraoral",
                type: "enum('Si','No')",
                nullable: false,
                collation: "utf8mb3_general_ci",
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "utf8mb3");

            migrationBuilder.AlterColumn<string>(
                name: "SangradoEncias",
                table: "antecedentehigieneoral",
                type: "enum('Si','No')",
                nullable: false,
                collation: "utf8mb3_general_ci",
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "utf8mb3");

            migrationBuilder.AlterColumn<string>(
                name: "IdHiloDental",
                table: "antecedentehigieneoral",
                type: "enum('Si','No')",
                nullable: false,
                collation: "utf8mb3_general_ci",
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "utf8mb3");

            migrationBuilder.AlterColumn<string>(
                name: "IdHigieneBucal",
                table: "antecedentehigieneoral",
                type: "enum('Buena','Regular','Mala')",
                nullable: false,
                collation: "utf8mb3_general_ci",
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "utf8mb3");

            migrationBuilder.AlterColumn<string>(
                name: "EnjuagueBucal",
                table: "antecedentehigieneoral",
                type: "enum('Si','No')",
                nullable: false,
                collation: "utf8mb3_general_ci",
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "utf8mb3");

            migrationBuilder.AlterColumn<string>(
                name: "CepilloDental",
                table: "antecedentehigieneoral",
                type: "enum('Si','No')",
                nullable: false,
                collation: "utf8mb3_general_ci",
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "utf8mb3");

            migrationBuilder.AlterColumn<string>(
                name: "HemorragiaDental",
                table: "antecedentegeneral",
                type: "enum('Si','No')",
                nullable: false,
                collation: "utf8mb3_general_ci",
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "utf8mb3");

            migrationBuilder.AlterColumn<string>(
                name: "EspecificacionHemorragia",
                table: "antecedentegeneral",
                type: "enum('Inmediata','Mediata','Ninguna')",
                nullable: false,
                collation: "utf8mb3_general_ci",
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "utf8mb3");

            migrationBuilder.AlterColumn<string>(
                name: "Embarazo",
                table: "antecedentegeneral",
                type: "enum('Si','No')",
                nullable: false,
                collation: "utf8mb3_general_ci",
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "utf8mb3");

            migrationBuilder.AlterColumn<string>(
                name: "Alergia",
                table: "antecedentegeneral",
                type: "enum('Si','No')",
                nullable: false,
                collation: "utf8mb3_general_ci",
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "utf8mb3");

            migrationBuilder.AlterColumn<string>(
                name: "Fuma",
                table: "antecedentebucodental",
                type: "enum('Si','No')",
                nullable: false,
                collation: "utf8mb3_general_ci",
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "utf8mb3");

            migrationBuilder.AlterColumn<string>(
                name: "Bebe",
                table: "antecedentebucodental",
                type: "enum('Si','No')",
                nullable: false,
                collation: "utf8mb3_general_ci",
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "utf8mb3");
        }
    }
}
