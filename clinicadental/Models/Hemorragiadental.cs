using System;
using System.Collections.Generic;
using static clinicadental.Enums.EnumClinicaDental;

namespace clinicadental.Models;

public partial class Hemorragiadental
{
    public int IdHemorragiaDental { get; set; }

    public HemorragiaDen? HemorragiaDental1 { get; set; }

    public EspecificacionHemorragia? EspecificacionHemorragia { get; set; } 

    public virtual ICollection<Antecedentepatologico>? Antecedentepatologicos { get; set; } = new List<Antecedentepatologico>();
}
