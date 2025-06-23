namespace clinicadental.Dtos
{
    public class AnteEnfermedadDto
    {
        public int? IdAntecedenteEnfermedad { get; set; }
        public int IdEnfermerdad { get; set; }
        public int? IdAntecedentePatologico { get; set; }
        public string? NombreEnfermedad { get; set; }
        public bool Seleccionada { get; set; }
    }
}
