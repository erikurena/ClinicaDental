using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace clinicadental.Models;

public partial class Clinica
{
    public int IdClinica { get; set; }

    public string? Nombre { get; set; }

    public string? Nit { get; set; }

    public string? Ujsedes { get; set; }

    public string? Pmc { get; set; }

    public string? Direccion { get; set; }

    public string? Celular { get; set; }
    public int? IdUsuario { get; set; }

    public string? Ciudad { get; set; } 

    public string? Pais { get; set; }

    public string? FotoClinica { get; set; }
    [NotMapped]
    public IFormFile? FotoFile { get; set; }
    public virtual Usuario? UsuariosNavigation { get; set; }
}
