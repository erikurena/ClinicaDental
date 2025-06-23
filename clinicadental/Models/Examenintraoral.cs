using System;
using System.Collections.Generic;
using static clinicadental.Enums.EnumClinicaDental;

namespace clinicadental.Models;

public partial class Examenintraoral
{
    public int IdExamenIntraoral { get; set; }

    public string? Encia { get; set; }

    public string? Labio { get; set; }

    public string? Lengua { get; set; }

    public string? MucosaYugal { get; set; }

    public string? Paladar { get; set; }

    public string? PisoDeBoca { get; set; }

    public ProtesisDental ProtesisDental { get; set; }

    public virtual ICollection<Historialclinico>? Historialclinicos { get; set; } = new List<Historialclinico>();
}
