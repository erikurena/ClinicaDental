using System;
using System.Collections.Generic;

namespace clinicadental.Models;

public partial class Respiracion
{
    public int IdRespiracion { get; set; }

    public string? TipoRespiracion { get; set; }

    public virtual ICollection<Examenextraoral>? Examenextraorals { get; set; } = new List<Examenextraoral>();
}
