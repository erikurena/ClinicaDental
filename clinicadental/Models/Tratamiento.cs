using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace clinicadental.Models;

public partial class Tratamiento
{
    public int IdTratamiento { get; set; }
    [Required(ErrorMessage = "El campo Subjetivo es obligatorio.")]
    public string? Subjetivo { get; set; }
    [Required(ErrorMessage = "El campo Objetivo es obligatorio.")]
    public string? Objetivo { get; set; }
    [Required(ErrorMessage ="El campo Analisis es obligatorio.")]
    public string? Analisis { get; set; }
    [Required(ErrorMessage = "El campo Plan de Accion es obligatorio.")]
    public string? PlanAccion { get; set; }
    public DateOnly? FechaTratamiento { get; set; }
    public sbyte? TratamientoConcluido { get; set; }
    public int IdHistorialClinico { get; set; }
    [Display(Name = "Presupuesto Total")]
    public decimal? PresupuestoTotal { get; set; } 
    [Display(Name = "A Cuenta")]
    public decimal? ACuenta { get; set; }
    public decimal? Saldo { get; set; }
    public DateTime? FechaCreacionTratamiento { get; set; } 
    public DateTime? FechaModificacionTratamiento { get; set; }

    public virtual ICollection<Avancetratamiento>? Avancetratamientos { get; set; } = new List<Avancetratamiento>();
    public virtual ICollection<PagosTratamiento>? PagoTratamiento { get; set; } = new List<PagosTratamiento>();
    public virtual Historialclinico? IdHistorialClinicoNavigation { get; set; } 
}
