using clinicadental.Models;

namespace clinicadental.Dtos
{
    public class PaginacionPacienteDto
    {
        public List<Paciente> Pacientes { get; set; }
        public int PaginaActual { get; set; }
        public int TotalPaginas { get; set; }
        public int TamañoPagina { get; set; }
    }
}
