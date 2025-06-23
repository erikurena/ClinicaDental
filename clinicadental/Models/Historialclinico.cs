using System;
using System.Collections.Generic;

namespace clinicadental.Models;

public partial class Historialclinico
{
    public int IdHistorialClinico { get; set; }
    public int IdPaciente { get; set; }
    public int IdExamenExtraOral { get; set; }
    public int IdExamenIntraoral { get; set; }
    public int IdAntecedenteBucoDental { get; set; }
    public int IdAntecedenteHigieneOral { get; set; }
    public int IdAntecedentePatologico { get; set; }
    public string? Observacion { get; set; }
    public int IdUsuario { get; set; }
    public int IdAntecedenteGeneral { get; set; }
    public DateOnly? FechaHistorial { get; set; }
    public string? CodigoHistorial { get; set; }
    public string? MotivoCita { get; set; }
    public string? Ci { get; set; }
    public DateTime? FechaCreacionHistorial { get; set; }
    public DateTime? FechaModificacionHistorial { get; set; }
    public virtual Antecedentebucodental? IdAntecedenteBucoDentalNavigation { get; set; } 
    public virtual Antecedentegeneral? IdAntecedenteGeneralNavigation { get; set; } 
    public virtual Antecedentehigieneoral? IdAntecedenteHigieneOralNavigation { get; set; } 
    public virtual Antecedentepatologico? IdAntecedentePatologicoNavigation { get; set; }
    public virtual Examenextraoral? IdExamenExtraOralNavigation { get; set; }
    public virtual Examenintraoral? IdExamenIntraoralNavigation { get; set; } 
    public virtual Paciente? IdPacienteNavigation { get; set; } 
    public virtual Usuario? IdUsuarioNavigation { get; set; }
    public virtual ICollection<Odontograma>? Odontogramas { get; set; } = new List<Odontograma>();
    public virtual ICollection<Tratamiento>? Tratamientos { get; set; } = new List<Tratamiento>();
}
