using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using static clinicadental.Enums.EnumClinicaDental;

namespace clinicadental.Models;

public partial class Paciente
{
    public int IdPaciente { get; set; }
    [DisplayName("Sexo: ")]
    public Sexo Sexo { get; set; }
    [DisplayName("Lugar de Nacimiento: ")]
    public string? IdLugarNacimiento { get; set; }
    [DisplayName("Estado Civil: ")]
    public EstadoCivil IdEstadoCivil { get; set; }
    [DisplayName("Grado de Instrucción: ")]
    public GradoInstruccion IdGradoInstruccion { get; set; }
    [DisplayName("Apellido Materno: ")]
    public string? ApellidoMaterno { get; set; }
    [DisplayName("Apellido Paterno: ")]
    public string? ApellidoPaterno { get; set; }
    [DisplayName("Celular: ")]
    public string? Celular { get; set; }
    [DisplayName("Dirección: ")]
    public string? Direccion { get; set; }
   
    public DateOnly? FechaNacimientoPaciente { get; set; }
    [DisplayName("Nombres: ")]
    public string? Nombres { get; set; }
    [DisplayName("Ocupación: ")]
    public string? Ocupacion { get; set; }
    public string? CodigoPaciente { get; set; }
    public int? IdUsuario { get; set; }
    public DateTime? FechaCreacionPaciente { get; set; }
    public DateTime? FechaModificacionPaciente { get; set; }

    public virtual ICollection<Historialclinico>? Historialclinicos { get; set; } = new List<Historialclinico>();

}
