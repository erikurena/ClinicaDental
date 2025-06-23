using System;
using System.Collections.Generic;
using static clinicadental.Enums.EnumClinicaDental;

namespace clinicadental.Models;

public partial class Antecedentegeneral
{
    public int IdAntecedenteGeneral { get; set; }

    public Alergia Alergia { get; set; }

    public string? TipoAlergia { get; set; }

    public HemorragiaDen HemorragiaDental { get; set; }

    public EspecificacionHemorragia EspecificacionHemorragia { get; set; }

    public Embarazo Embarazo { get; set; }

    public string? SemanaEmbarazo { get; set; }

    public virtual ICollection<Historialclinico>? Historialclinicos { get; set; } = new List<Historialclinico>();
}
