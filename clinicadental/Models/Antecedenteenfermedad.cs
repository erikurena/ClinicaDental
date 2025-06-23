using System;
using System.Collections.Generic;

namespace clinicadental.Models;

public partial class Antecedenteenfermedad
{
    public int IdAntecedenteEnfermedad { get; set; }

    public int IdEnfermerdad { get; set; }

    public int IdAntecedentePatologico { get; set; }

    public virtual Antecedentepatologico? IdAntecedentePatologicoNavigation { get; set; } 

    public virtual Enfermerdad? IdEnfermerdadNavigation { get; set; } 
}
