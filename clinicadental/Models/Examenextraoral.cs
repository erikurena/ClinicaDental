using System;
using System.Collections.Generic;

namespace clinicadental.Models;

public partial class Examenextraoral
{
    public int IdExamenExtraOral { get; set; }

    public string? Atm { get; set; }

    public string? GanglioLinfatico { get; set; }

    public string? Otro { get; set; }

    public int IdRespiracion { get; set; }

    public virtual ICollection<Historialclinico>? Historialclinicos { get; set; } = new List<Historialclinico>();

    public virtual Respiracion? IdRespiracionNavigation { get; set; } 
}
