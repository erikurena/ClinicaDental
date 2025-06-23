using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace clinicadental.Migrations
{
    /// <inheritdoc />
    public partial class identiymigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb3");

            migrationBuilder.CreateTable(
                name: "afeccion",
                columns: table => new
                {
                    IdAfeccion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Afeccion = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.IdAfeccion);
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "antecedentebucodental",
                columns: table => new
                {
                    IdAntecedenteBucoDental = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UltimaVisitaDental = table.Column<DateTime>(type: "datetime", nullable: true),
                    Otro = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    Fuma = table.Column<string>(type: "enum('Si','No')", nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    Bebe = table.Column<string>(type: "enum('Si','No')", nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.IdAntecedenteBucoDental);
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "antecedentegeneral",
                columns: table => new
                {
                    IdAntecedenteGeneral = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Alergia = table.Column<string>(type: "enum('Si','No')", nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    TipoAlergia = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    HemorragiaDental = table.Column<string>(type: "enum('Si','No')", nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    EspecificacionHemorragia = table.Column<string>(type: "enum('Inmediata','Mediata','Ninguna')", nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    Embarazo = table.Column<string>(type: "enum('Si','No')", nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    SemanaEmbarazo = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.IdAntecedenteGeneral);
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "antecedentehigieneoral",
                columns: table => new
                {
                    IdAntecedenteHigieneOral = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdHigieneBucal = table.Column<string>(type: "enum('Buena','Regular','Mala')", nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    FrecuenciaCepilladoDental = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    CepilloDental = table.Column<string>(type: "enum('Si','No')", nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    EnjuagueBucal = table.Column<string>(type: "enum('Si','No')", nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    IdHiloDental = table.Column<string>(type: "enum('Si','No')", nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    SangradoEncias = table.Column<string>(type: "enum('Si','No')", nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.IdAntecedenteHigieneOral);
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "antecedentepatologico",
                columns: table => new
                {
                    IdAntecedentePatologico = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Otro = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    AntecedentePatologicoFamiliar = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    TratamientoMedico = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    RecibeMedicacion = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.IdAntecedentePatologico);
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    NormalizedName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    UserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    NormalizedUserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    Email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    NormalizedEmail = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    SecurityStamp = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "enfermerdad",
                columns: table => new
                {
                    IdEnfermerdad = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Enfermedad = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.IdEnfermerdad);
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "estadocivil",
                columns: table => new
                {
                    IdEstadoCivil = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EstadoCivil = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.IdEstadoCivil);
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "examenintraoral",
                columns: table => new
                {
                    IdExamenIntraoral = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Encia = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    Labio = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    Lengua = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    MucosaYugal = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    Paladar = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    PisoDeBoca = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    ProtesisDental = table.Column<string>(type: "enum('Si','No')", nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.IdExamenIntraoral);
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "gradoinstruccion",
                columns: table => new
                {
                    IdGradoInstruccion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    GradoInstruccion = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.IdGradoInstruccion);
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "lugarnacimiento",
                columns: table => new
                {
                    IdLugarNacimiento = table.Column<int>(type: "int", nullable: false),
                    LugarNacimiento = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.IdLugarNacimiento);
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "respiracion",
                columns: table => new
                {
                    IdRespiracion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TipoRespiracion = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.IdRespiracion);
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

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

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    ProviderKey = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    ProviderDisplayName = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    Value = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "antecedenteenfermedad",
                columns: table => new
                {
                    IdAntecedenteEnfermedad = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdEnfermerdad = table.Column<int>(type: "int", nullable: false),
                    IdAntecedentePatologico = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.IdAntecedenteEnfermedad);
                    table.ForeignKey(
                        name: "fk_antecedenteenfermedad_antecedentepatologico1",
                        column: x => x.IdAntecedentePatologico,
                        principalTable: "antecedentepatologico",
                        principalColumn: "IdAntecedentePatologico");
                    table.ForeignKey(
                        name: "fk_antecedenteenfermedad_enfermerdad1",
                        column: x => x.IdEnfermerdad,
                        principalTable: "enfermerdad",
                        principalColumn: "IdEnfermerdad");
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "paciente",
                columns: table => new
                {
                    IdPaciente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Sexo = table.Column<string>(type: "enum('Hombre','Mujer')", nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    IdLugarNacimiento = table.Column<int>(type: "int", nullable: false),
                    IdEstadoCivil = table.Column<int>(type: "int", nullable: false),
                    IdGradoInstruccion = table.Column<int>(type: "int", nullable: false),
                    ApellidoMaterno = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    ApellidoPaterno = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    Celular = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    Direccion = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    FechaNacimiento = table.Column<DateOnly>(type: "date", nullable: true),
                    Nombres = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    Ocupacion = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    CodigoPaciente = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.IdPaciente);                  
                    table.ForeignKey(
                        name: "fk_paciente_gradoinstruccion1",
                        column: x => x.IdGradoInstruccion,
                        principalTable: "gradoinstruccion",
                        principalColumn: "IdGradoInstruccion");
                    table.ForeignKey(
                        name: "fk_paciente_lugarnacimiento1",
                        column: x => x.IdLugarNacimiento,
                        principalTable: "lugarnacimiento",
                        principalColumn: "IdLugarNacimiento");
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "examenextraoral",
                columns: table => new
                {
                    IdExamenExtraOral = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Atm = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    GanglioLinfatico = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    Otro = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    IdRespiracion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.IdExamenExtraOral);
                    table.ForeignKey(
                        name: "fk_examenextraoral_respiracion1",
                        column: x => x.IdRespiracion,
                        principalTable: "respiracion",
                        principalColumn: "IdRespiracion");
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "usuario",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PrimerNombre = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    SegundoNombre = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    ApellidoPaterno = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    ApellidoMaterno = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    Celular = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    FechaNacimiento = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    Especialidad = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    IdRol = table.Column<int>(type: "int", nullable: false),
                    Sexo = table.Column<string>(type: "enum('Hombre','Mujer')", nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    CodigoUsuario = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    FotoUsuario = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.IdUsuario);
                    table.ForeignKey(
                        name: "fk_usuario_rol1",
                        column: x => x.IdRol,
                        principalTable: "rol",
                        principalColumn: "IdRol");
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "cita",
                columns: table => new
                {
                    IdCita = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FechaHoraCita = table.Column<DateOnly>(type: "date", nullable: false),
                    HorainicioCita = table.Column<DateTime>(type: "datetime", nullable: false),
                    HorafinCita = table.Column<DateTime>(type: "datetime", nullable: false),
                    NombrePaciente = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    MotivoCita = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    EstadoCita = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    FechaRegistro = table.Column<DateTime>(type: "datetime", nullable: false),
                    CorreoElectronico = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    Telefono = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.IdCita);
                    table.ForeignKey(
                        name: "fk_cita_usuario1",
                        column: x => x.IdUsuario,
                        principalTable: "usuario",
                        principalColumn: "IdUsuario");
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "clinica",
                columns: table => new
                {
                    IdClinica = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    Nit = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    Ujsedes = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    Pmc = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    Direccion = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    celular = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    IdUsuario = table.Column<int>(type: "int", nullable: true),
                    FotoClinica = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.IdClinica);
                    table.ForeignKey(
                        name: "fk_clinica_usuario1",
                        column: x => x.IdUsuario,
                        principalTable: "usuario",
                        principalColumn: "IdUsuario");
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "cuenta",
                columns: table => new
                {
                    IdCuenta = table.Column<int>(type: "int", nullable: false),
                    Cuenta = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    Password = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.IdCuenta);
                    table.ForeignKey(
                        name: "fk_cuenta_usuario1",
                        column: x => x.IdCuenta,
                        principalTable: "usuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "historialclinico",
                columns: table => new
                {
                    IdHistorialClinico = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdPaciente = table.Column<int>(type: "int", nullable: false),
                    IdExamenExtraOral = table.Column<int>(type: "int", nullable: false),
                    IdExamenIntraoral = table.Column<int>(type: "int", nullable: false),
                    IdAntecedenteBucoDental = table.Column<int>(type: "int", nullable: false),
                    IdAntecedenteHigieneOral = table.Column<int>(type: "int", nullable: false),
                    IdAntecedentePatologico = table.Column<int>(type: "int", nullable: false),
                    Observacion = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    IdAntecedenteGeneral = table.Column<int>(type: "int", nullable: false),
                    FechaHistorial = table.Column<DateOnly>(type: "date", nullable: true),
                    CodigoHistorial = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    MotivoCita = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    Ci = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.IdHistorialClinico);
                    table.ForeignKey(
                        name: "fk_HistorialClinico_antecedentebucodental1",
                        column: x => x.IdAntecedenteBucoDental,
                        principalTable: "antecedentebucodental",
                        principalColumn: "IdAntecedenteBucoDental",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_HistorialClinico_antecedentehigieneoral1",
                        column: x => x.IdAntecedenteHigieneOral,
                        principalTable: "antecedentehigieneoral",
                        principalColumn: "IdAntecedenteHigieneOral",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_HistorialClinico_antecedentepatologico1",
                        column: x => x.IdAntecedentePatologico,
                        principalTable: "antecedentepatologico",
                        principalColumn: "IdAntecedentePatologico",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_HistorialClinico_examenextraoral1",
                        column: x => x.IdExamenExtraOral,
                        principalTable: "examenextraoral",
                        principalColumn: "IdExamenExtraOral",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_HistorialClinico_examenintraoral1",
                        column: x => x.IdExamenIntraoral,
                        principalTable: "examenintraoral",
                        principalColumn: "IdExamenIntraoral",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_HistorialClinico_paciente1",
                        column: x => x.IdPaciente,
                        principalTable: "paciente",
                        principalColumn: "IdPaciente");
                    table.ForeignKey(
                        name: "fk_HistorialClinico_usuario1",
                        column: x => x.IdUsuario,
                        principalTable: "usuario",
                        principalColumn: "IdUsuario");
                    table.ForeignKey(
                        name: "fk_historialclinico_antecedentegeneral1",
                        column: x => x.IdAntecedenteGeneral,
                        principalTable: "antecedentegeneral",
                        principalColumn: "IdAntecedenteGeneral",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "odontograma",
                columns: table => new
                {
                    IdOdontograma = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NroPiezaDental = table.Column<int>(type: "int", nullable: true),
                    CaraPiezaDental = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    IdAfeccion = table.Column<int>(type: "int", nullable: false),
                    IdHistorialClinico = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.IdOdontograma);
                    table.ForeignKey(
                        name: "fk_odontograma_afeccion1",
                        column: x => x.IdAfeccion,
                        principalTable: "afeccion",
                        principalColumn: "IdAfeccion");
                    table.ForeignKey(
                        name: "fk_odontograma_historialclinico1",
                        column: x => x.IdHistorialClinico,
                        principalTable: "historialclinico",
                        principalColumn: "IdHistorialClinico");
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "tratamiento",
                columns: table => new
                {
                    IdTratamiento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Subjetivo = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    Objetivo = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    Analisis = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    PlanAccion = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    FechaTratamiento = table.Column<DateOnly>(type: "date", nullable: true),
                    TratamientoConcluido = table.Column<sbyte>(type: "tinyint", nullable: true),
                    IdHistorialClinico = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.IdTratamiento);
                    table.ForeignKey(
                        name: "fk_tratamiento_historialclinico1",
                        column: x => x.IdHistorialClinico,
                        principalTable: "historialclinico",
                        principalColumn: "IdHistorialClinico");
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "estadoperiodontal",
                columns: table => new
                {
                    IdEstadoPeriodontal = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CPO_C = table.Column<int>(type: "int", nullable: true),
                    CPO_P = table.Column<int>(type: "int", nullable: true),
                    CPO_EI = table.Column<int>(type: "int", nullable: true),
                    CPO_O = table.Column<int>(type: "int", nullable: true),
                    TotalCpo = table.Column<int>(type: "int", nullable: true),
                    CEO_C = table.Column<int>(type: "int", nullable: true),
                    CEO_E = table.Column<int>(type: "int", nullable: true),
                    CEO_O = table.Column<int>(type: "int", nullable: true),
                    TotalCeo = table.Column<int>(type: "int", nullable: true),
                    TotalPiezasSanas = table.Column<int>(type: "int", nullable: true),
                    TotalPiezasDentarias = table.Column<int>(type: "int", nullable: true),
                    IdOdontograma = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.IdEstadoPeriodontal);
                    table.ForeignKey(
                        name: "fk_estadoperiodontal_odontograma1",
                        column: x => x.IdOdontograma,
                        principalTable: "odontograma",
                        principalColumn: "IdOdontograma");
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "avancetratamiento",
                columns: table => new
                {
                    IdAvanceTratamiento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FechaInicio = table.Column<DateOnly>(type: "date", nullable: true),
                    FechaConclusion = table.Column<DateOnly>(type: "date", nullable: true),
                    PiezaDental = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    IdTratamiento = table.Column<int>(type: "int", nullable: false),
                    Avance = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.IdAvanceTratamiento);
                    table.ForeignKey(
                        name: "fk_avancetratamiento_tratamiento1",
                        column: x => x.IdTratamiento,
                        principalTable: "tratamiento",
                        principalColumn: "IdTratamiento");
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "presupuestotratamiento",
                columns: table => new
                {
                    IdPresupuestoTratamiento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PresupuestoTotal = table.Column<decimal>(type: "decimal(10)", precision: 10, nullable: false),
                    ACuenta = table.Column<decimal>(type: "decimal(10)", precision: 10, nullable: false),
                    Saldo = table.Column<decimal>(type: "decimal(10)", precision: 10, nullable: false),
                    IdTratamiento = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "pagostratamiento",
                columns: table => new
                {
                    IdPagoTratamiento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Monto = table.Column<decimal>(type: "decimal(10)", precision: 10, nullable: false),
                    FechaPago = table.Column<DateOnly>(type: "date", nullable: false),
                    IdpresupuestoTratamiento = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.IdPagoTratamiento);
                    table.ForeignKey(
                        name: "fk_pagostratamiento_presupuestotratamiento1",
                        column: x => x.IdpresupuestoTratamiento,
                        principalTable: "presupuestotratamiento",
                        principalColumn: "IdPresupuestoTratamiento");
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateIndex(
                name: "fk_antecedenteenfermedad_antecedentepatologico1_idx",
                table: "antecedenteenfermedad",
                column: "IdAntecedentePatologico");

            migrationBuilder.CreateIndex(
                name: "fk_antecedenteenfermedad_enfermerdad1_idx",
                table: "antecedenteenfermedad",
                column: "IdEnfermerdad");

            migrationBuilder.CreateIndex(
                name: "idantecedentegeneral_UNIQUE",
                table: "antecedentegeneral",
                column: "IdAntecedenteGeneral",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_avancetratamiento_tratamiento1_idx",
                table: "avancetratamiento",
                column: "IdTratamiento");

            migrationBuilder.CreateIndex(
                name: "fk_cita_usuario1_idx",
                table: "cita",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "fk_clinica_usuario1_idx",
                table: "clinica",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "fk_cuenta_usuario1_idx",
                table: "cuenta",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "fk_estadoperiodontal_odontograma1_idx",
                table: "estadoperiodontal",
                column: "IdOdontograma");

            migrationBuilder.CreateIndex(
                name: "fk_examenextraoral_respiracion1_idx",
                table: "examenextraoral",
                column: "IdRespiracion");

            migrationBuilder.CreateIndex(
                name: "fk_HistorialClinico_antecedentebucodental1_idx",
                table: "historialclinico",
                column: "IdAntecedenteBucoDental");

            migrationBuilder.CreateIndex(
                name: "fk_historialclinico_antecedentegeneral1_idx",
                table: "historialclinico",
                column: "IdAntecedenteGeneral");

            migrationBuilder.CreateIndex(
                name: "fk_HistorialClinico_antecedentehigieneoral1_idx",
                table: "historialclinico",
                column: "IdAntecedenteHigieneOral");

            migrationBuilder.CreateIndex(
                name: "fk_HistorialClinico_antecedentepatologico1_idx",
                table: "historialclinico",
                column: "IdAntecedentePatologico");

            migrationBuilder.CreateIndex(
                name: "fk_HistorialClinico_examenextraoral1_idx",
                table: "historialclinico",
                column: "IdExamenExtraOral");

            migrationBuilder.CreateIndex(
                name: "fk_HistorialClinico_examenintraoral1_idx",
                table: "historialclinico",
                column: "IdExamenIntraoral");

            migrationBuilder.CreateIndex(
                name: "fk_HistorialClinico_paciente1_idx",
                table: "historialclinico",
                column: "IdPaciente");

            migrationBuilder.CreateIndex(
                name: "fk_HistorialClinico_usuario1_idx",
                table: "historialclinico",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "fk_odontograma_afeccion1_idx",
                table: "odontograma",
                column: "IdAfeccion");

            migrationBuilder.CreateIndex(
                name: "fk_odontograma_historialclinico1_idx",
                table: "odontograma",
                column: "IdHistorialClinico");

            migrationBuilder.CreateIndex(
                name: "fk_paciente_gradoinstruccion1_idx",
                table: "paciente",
                column: "IdGradoInstruccion");

            migrationBuilder.CreateIndex(
                name: "fk_paciente_lugarnacimiento1_idx",
                table: "paciente",
                column: "IdLugarNacimiento");

            migrationBuilder.CreateIndex(
                name: "fk_pagostratamiento_presupuestotratamiento1_idx",
                table: "pagostratamiento",
                column: "IdpresupuestoTratamiento");

            migrationBuilder.CreateIndex(
                name: "fk_presupuestotratamiento_tratamiento1_idx",
                table: "presupuestotratamiento",
                column: "IdTratamiento");

            migrationBuilder.CreateIndex(
                name: "fk_tratamiento_historialclinico1_idx",
                table: "tratamiento",
                column: "IdHistorialClinico");

            migrationBuilder.CreateIndex(
                name: "fk_usuario_rol1_idx",
                table: "usuario",
                column: "IdRol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "antecedenteenfermedad");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "avancetratamiento");

            migrationBuilder.DropTable(
                name: "cita");

            migrationBuilder.DropTable(
                name: "clinica");

            migrationBuilder.DropTable(
                name: "cuenta");

            migrationBuilder.DropTable(
                name: "estadoperiodontal");

            migrationBuilder.DropTable(
                name: "pagostratamiento");

            migrationBuilder.DropTable(
                name: "enfermerdad");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "odontograma");

            migrationBuilder.DropTable(
                name: "presupuestotratamiento");

            migrationBuilder.DropTable(
                name: "afeccion");

            migrationBuilder.DropTable(
                name: "tratamiento");

            migrationBuilder.DropTable(
                name: "historialclinico");

            migrationBuilder.DropTable(
                name: "antecedentebucodental");

            migrationBuilder.DropTable(
                name: "antecedentehigieneoral");

            migrationBuilder.DropTable(
                name: "antecedentepatologico");

            migrationBuilder.DropTable(
                name: "examenextraoral");

            migrationBuilder.DropTable(
                name: "examenintraoral");

            migrationBuilder.DropTable(
                name: "paciente");

            migrationBuilder.DropTable(
                name: "usuario");

            migrationBuilder.DropTable(
                name: "antecedentegeneral");

            migrationBuilder.DropTable(
                name: "respiracion");

            migrationBuilder.DropTable(
                name: "estadocivil");

            migrationBuilder.DropTable(
                name: "gradoinstruccion");

            migrationBuilder.DropTable(
                name: "lugarnacimiento");

            migrationBuilder.DropTable(
                name: "rol");
        }
    }
}
