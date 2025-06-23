using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace clinicadental.Models;

public partial class Lugarnacimiento
{
   
    public int IdLugarNacimiento { get; set; }
    [DisplayName("Lugar de Nacimiento: ")]
    public string? LugarNacimiento1 { get; set; }

    public virtual ICollection<Paciente>? Pacientes { get; set; } = new List<Paciente>();
}
