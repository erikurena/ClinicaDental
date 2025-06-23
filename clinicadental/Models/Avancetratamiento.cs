using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace clinicadental.Models;

public partial class Avancetratamiento
{
    public int IdAvanceTratamiento { get; set; }

    public DateOnly? FechaInicio { get; set; } = DateOnly.FromDateTime(DateTime.Now); // Establece la fecha de inicio al día actual por defecto

    public DateOnly? FechaConclusion { get; set; } = DateOnly.FromDateTime(DateTime.Now); // Establece la fecha de conclusión al día actual por defecto

    public string? PiezaDental { get; set; }
    
    public int IdTratamiento { get; set; }
    
    public string? Avance { get; set; }

    public virtual Tratamiento? IdTratamientoNavigation { get; set; } 
}
