using System;
using System.Collections.Generic;

namespace clinicadental.Models;

public partial class Estadoperiodontale
{
    public int IdEstadoPeriodontal { get; set; }

    public int? CpodC { get; set; }

    public int? CpodP { get; set; }

    public int? CpodEi { get; set; }

    public int? CpodO { get; set; }

    public int? CpodTotal { get; set; }

    public int? CeoC { get; set; }

    public int? CeoE { get; set; }

    public int? CeoO { get; set; }

    public int? CeoTotal { get; set; }

    public int? TotalPiezasSanas { get; set; }

    public int? TotalPiezasDentarias { get; set; }

    public int IdOdontograma { get; set; }

    public virtual Odontograma? IdOdontogramaNavigation { get; set; } 
  
}
