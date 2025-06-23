using System;
using System.Collections.Generic;

namespace clinicadental.Models;

public partial class Antecedentepatologico
{
    public int IdAntecedentePatologico { get; set; }

    public string? Otro { get; set; }

    public string? AntecedentePatologicoFamiliar { get; set; }

    public string? TratamientoMedico { get; set; }

    public string? RecibeMedicacion { get; set; }

    public virtual ICollection<Antecedenteenfermedad>? Antecedenteenfermedads { get; set; } = new List<Antecedenteenfermedad>();

    public virtual ICollection<Historialclinico>? Historialclinicos { get; set; } = new List<Historialclinico>();
}
