using clinicadental.Models;

namespace clinicadental.Dtos
{
    public class PacienteHistorialDto
    {
        public Paciente? Paciente { get; set; }        
        public List<HistorialItemDto>? Historiales { get; set; }
    }
    public class HistorialItemDto
    {
        public int IdHistorial { get; set; }
        public DateOnly FechaHistorial { get; set; }
        public string? MotivoCita { get; set; }
    }
}
