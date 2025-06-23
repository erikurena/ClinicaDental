namespace clinicadental.Models
{
    public partial class Cita
    {
        public int IdCita { get; set; }
        public DateOnly FechaHoraCita { get; set; }
        public DateTime HorainicioCita { get; set; }
        public DateTime HorafinCita { get; set; }
        public string? NombrePaciente { get; set; }
        public string? MotivoCita { get; set; }
        public string? EstadoCita { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public string? CorreoElectronico { get; set; }
        public string? Telefono { get; set; }
        public int IdUsuario { get; set; }
        public virtual Usuario? UsuarioNavigation { get; set; }
    }
}
