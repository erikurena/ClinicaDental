using System;
using System.Collections.Generic;

namespace clinicadental.Models;

public partial class Enfermerdad
{
    public int IdEnfermerdad { get; set; }

    public string? Enfermedad { get; set; }

    public virtual ICollection<Antecedenteenfermedad>? Antecedenteenfermedads { get; set; } = new List<Antecedenteenfermedad>();
}
