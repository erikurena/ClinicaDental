using System;
using System.Collections.Generic;
using static clinicadental.Enums.EnumClinicaDental;

namespace clinicadental.Models;

public partial class Antecedentehigieneoral
{
    public int IdAntecedenteHigieneOral { get; set; }

    public HigieneBucal IdHigieneBucal { get; set; }

    public string? FrecuenciaCepilladoDental { get; set; }

    public CepilloDental CepilloDental { get; set; }

    public EnjuagueBucal EnjuagueBucal { get; set; }

    public HiloDental IdHiloDental { get; set; }

    public SangradoEncias SangradoEncias { get; set; }

    public virtual ICollection<Historialclinico>? Historialclinicos { get; set; } = new List<Historialclinico>();
}
