using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using static clinicadental.Enums.EnumClinicaDental;

namespace clinicadental.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string? PrimerNombre { get; set; }

    public string? SegundoNombre { get; set; }

    public string? ApellidoPaterno { get; set; }

    public string? ApellidoMaterno { get; set; }

    public string? Celular { get; set; }

    public DateOnly? FechaNacimiento { get; set; }

    public string? Especialidad { get; set; }

    public Sexo Sexo { get; set; } 

    public string? CodigoUsuario { get; set; }

    public string? FotoUsuario { get; set; }
    [NotMapped]
    public string? Email { get; set; }
    [NotMapped]
    public string? Password { get; set; }
    public DateTime? FechaCreacionUsuario { get; set; } 
    public DateTime? FechaModificacionUsuario { get; set; }

    public virtual ICollection<Historialclinico>? Historialclinicos { get; set; } = new List<Historialclinico>();
    public virtual ICollection<Cita>? Citas { get; set; } = new List<Cita>();
    public virtual ICollection<Clinica>? Clinicas { get; set; } = new List<Clinica>();

}
