namespace clinicadental.Dtos
{
    public class OdontogramaDto
    {
        public int Id { get; set; }
        public int? NroPiezaDental { get; set; }
        public string? CaraPiezaDental { get; set; }
        public int IdAfeccion { get; set; }
        public int IdHistorialClinico { get; set; }
      
    }
}
