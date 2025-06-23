using System;
using System.ComponentModel;
using static clinicadental.Enums.EnumClinicaDental;

namespace clinicadental.Dtos
{
    public class MostrarReportes
    {
        public DateTime? FechaInicio { get; set; } = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        public DateTime? FechaFin { get; set; } =DateTime.Today;      
        public Sexo Sexo { get; set; }      
        public string? IdLugarNacimiento { get; set; }     
        public EstadoCivil IdEstadoCivil { get; set; }     
        public GradoInstruccion IdGradoInstruccion { get; set; }        
        public string? ApellidoMaterno { get; set; }       
        public string? ApellidoPaterno { get; set; }     
        public string? Celular { get; set; }      
        public string? Direccion { get; set; }      
        public string? Nombres { get; set; }     
        public string? Ocupacion { get; set; }
    }
}
