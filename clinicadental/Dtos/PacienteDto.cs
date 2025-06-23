namespace clinicadental.Dtos
{
    public class PacienteDto
    {
        public int? IdPaciente { get; set; }
        public  string? ApellidoPaterno {get; set;}
        public string? ApellidoMaterno { get; set; }
        public string? Nombres { get; set; }
        public string? Celular { get; set; }
        public string? Direccion { get; set; }
        public string? CodigoPaciente { get; set; }
    }
}
