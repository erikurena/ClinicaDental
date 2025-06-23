using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace clinicadental.Migrations
{
    /// <inheritdoc />
    public partial class stringenums : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Sexo",
                table: "usuario",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "utf8mb3_general_ci",
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "utf8mb3");

            migrationBuilder.AlterColumn<string>(
                name: "Sexo",
                table: "paciente",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "utf8mb3_general_ci",
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "utf8mb3");

            migrationBuilder.AlterColumn<string>(
                name: "IdGradoInstruccion",
                table: "paciente",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "utf8mb3_general_ci",
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "utf8mb3");

            migrationBuilder.AlterColumn<string>(
                name: "IdEstadoCivil",
                table: "paciente",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "utf8mb3_general_ci",
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "utf8mb3");

            migrationBuilder.AlterColumn<string>(
                name: "ProtesisDental",
                table: "examenintraoral",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "utf8mb3_general_ci",
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "utf8mb3");

            migrationBuilder.AlterColumn<string>(
                name: "SangradoEncias",
                table: "antecedentehigieneoral",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "utf8mb3_general_ci",
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "utf8mb3");

            migrationBuilder.AlterColumn<string>(
                name: "IdHiloDental",
                table: "antecedentehigieneoral",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "utf8mb3_general_ci",
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "utf8mb3");

            migrationBuilder.AlterColumn<string>(
                name: "IdHigieneBucal",
                table: "antecedentehigieneoral",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "utf8mb3_general_ci",
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "utf8mb3");

            migrationBuilder.AlterColumn<string>(
                name: "EnjuagueBucal",
                table: "antecedentehigieneoral",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "utf8mb3_general_ci",
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "utf8mb3");

            migrationBuilder.AlterColumn<string>(
                name: "CepilloDental",
                table: "antecedentehigieneoral",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "utf8mb3_general_ci",
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "utf8mb3");

            migrationBuilder.AlterColumn<string>(
                name: "HemorragiaDental",
                table: "antecedentegeneral",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "utf8mb3_general_ci",
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "utf8mb3");

            migrationBuilder.AlterColumn<string>(
                name: "EspecificacionHemorragia",
                table: "antecedentegeneral",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "utf8mb3_general_ci",
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "utf8mb3");

            migrationBuilder.AlterColumn<string>(
                name: "Embarazo",
                table: "antecedentegeneral",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "utf8mb3_general_ci",
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "utf8mb3");

            migrationBuilder.AlterColumn<string>(
                name: "Alergia",
                table: "antecedentegeneral",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "utf8mb3_general_ci",
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "utf8mb3");

            migrationBuilder.AlterColumn<string>(
                name: "Fuma",
                table: "antecedentebucodental",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "utf8mb3_general_ci",
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "utf8mb3");

            migrationBuilder.AlterColumn<string>(
                name: "Bebe",
                table: "antecedentebucodental",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                collation: "utf8mb3_general_ci",
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "utf8mb3");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Sexo",
                table: "usuario",
                type: "int",
                maxLength: 45,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .OldAnnotation("MySql:CharSet", "utf8mb3")
                .OldAnnotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.AlterColumn<int>(
                name: "Sexo",
                table: "paciente",
                type: "int",
                maxLength: 45,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .OldAnnotation("MySql:CharSet", "utf8mb3")
                .OldAnnotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.AlterColumn<int>(
                name: "IdGradoInstruccion",
                table: "paciente",
                type: "int",
                maxLength: 45,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .OldAnnotation("MySql:CharSet", "utf8mb3")
                .OldAnnotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.AlterColumn<int>(
                name: "IdEstadoCivil",
                table: "paciente",
                type: "int",
                maxLength: 45,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .OldAnnotation("MySql:CharSet", "utf8mb3")
                .OldAnnotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.AlterColumn<int>(
                name: "ProtesisDental",
                table: "examenintraoral",
                type: "int",
                maxLength: 45,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .OldAnnotation("MySql:CharSet", "utf8mb3")
                .OldAnnotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.AlterColumn<int>(
                name: "SangradoEncias",
                table: "antecedentehigieneoral",
                type: "int",
                maxLength: 45,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .OldAnnotation("MySql:CharSet", "utf8mb3")
                .OldAnnotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.AlterColumn<int>(
                name: "IdHiloDental",
                table: "antecedentehigieneoral",
                type: "int",
                maxLength: 45,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .OldAnnotation("MySql:CharSet", "utf8mb3")
                .OldAnnotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.AlterColumn<int>(
                name: "IdHigieneBucal",
                table: "antecedentehigieneoral",
                type: "int",
                maxLength: 45,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .OldAnnotation("MySql:CharSet", "utf8mb3")
                .OldAnnotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.AlterColumn<int>(
                name: "EnjuagueBucal",
                table: "antecedentehigieneoral",
                type: "int",
                maxLength: 45,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .OldAnnotation("MySql:CharSet", "utf8mb3")
                .OldAnnotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.AlterColumn<int>(
                name: "CepilloDental",
                table: "antecedentehigieneoral",
                type: "int",
                maxLength: 45,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .OldAnnotation("MySql:CharSet", "utf8mb3")
                .OldAnnotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.AlterColumn<int>(
                name: "HemorragiaDental",
                table: "antecedentegeneral",
                type: "int",
                maxLength: 45,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .OldAnnotation("MySql:CharSet", "utf8mb3")
                .OldAnnotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.AlterColumn<int>(
                name: "EspecificacionHemorragia",
                table: "antecedentegeneral",
                type: "int",
                maxLength: 45,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .OldAnnotation("MySql:CharSet", "utf8mb3")
                .OldAnnotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.AlterColumn<int>(
                name: "Embarazo",
                table: "antecedentegeneral",
                type: "int",
                maxLength: 45,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .OldAnnotation("MySql:CharSet", "utf8mb3")
                .OldAnnotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.AlterColumn<int>(
                name: "Alergia",
                table: "antecedentegeneral",
                type: "int",
                maxLength: 45,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .OldAnnotation("MySql:CharSet", "utf8mb3")
                .OldAnnotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.AlterColumn<int>(
                name: "Fuma",
                table: "antecedentebucodental",
                type: "int",
                maxLength: 45,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .OldAnnotation("MySql:CharSet", "utf8mb3")
                .OldAnnotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.AlterColumn<int>(
                name: "Bebe",
                table: "antecedentebucodental",
                type: "int",
                maxLength: 45,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .OldAnnotation("MySql:CharSet", "utf8mb3")
                .OldAnnotation("Relational:Collation", "utf8mb3_general_ci");
        }
    }
}
