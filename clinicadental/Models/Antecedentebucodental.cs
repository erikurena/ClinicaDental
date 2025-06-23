using System;
using System.Collections.Generic;
using static clinicadental.Enums.EnumClinicaDental;

namespace clinicadental.Models;

public partial class Antecedentebucodental
{
    public int IdAntecedenteBucoDental { get; set; }

    public DateTime? UltimaVisitaDental { get; set; }

    public string? Otro { get; set; }

    public Fuma Fuma { get; set; }

    public Bebe Bebe { get; set; }

    public virtual ICollection<Historialclinico>? Historialclinicos { get; set; } = new List<Historialclinico>();
}
