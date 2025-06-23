using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace clinicadental.Models;

public partial class Odontograma
{
    public int IdOdontograma { get; set; }

    public int? NroPiezaDental { get; set; }

    public string? CaraPiezaDental { get; set; }

    public int IdAfeccion { get; set; }

    public int IdHistorialClinico { get; set; }
    [NotMapped]
    public virtual ICollection<Estadoperiodontale>? Estadoperiodontales { get; set; } = new List<Estadoperiodontale>();   
    
    public virtual Afeccion? IdAfeccionNavigation { get; set; }
    [NotMapped]
    public virtual Historialclinico? IdHistorialClinicoNavigation { get; set; } 
}
