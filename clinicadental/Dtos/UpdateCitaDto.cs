namespace clinicadental.Dtos
{
    public  class UpdateCitaDto
    {
        public int IdCita { get; set; }
        public DateOnly FechaHoraCita { get; set; }
        public DateTime HorainicioCita { get; set; }
        public DateTime HorafinCita { get; set; }
    }
}
