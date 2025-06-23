using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace clinicadental.Models;

public partial class Afeccion
{
    public int IdAfeccion { get; set; }

    public string? Afeccion1 { get; set; }
    [JsonIgnore]
    public virtual ICollection<Odontograma>? Odontogramas { get; set; } = new List<Odontograma>();
}
