using System.ComponentModel.DataAnnotations;

namespace clinicadental.Models
{
    public partial class PagosTratamiento
    {
        public int IdPagoTratamiento { get; set; }
        [Required(ErrorMessage = "El monto es obligatorio")]
        public decimal? Monto { get; set; }
        public DateOnly? FechaPago { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public int? IdTratamiento { get; set; }       
        public virtual Tratamiento? IdTratamientoNavigation { get; set; }

    }
}
