using clinicadental.Models;
using System.ComponentModel.DataAnnotations.Schema;
using static clinicadental.Enums.EnumClinicaDental;

namespace clinicadental.Dtos
{
    public  class AfeccionDto
    {
        public int IdAfeccionDto { get; set; }
        public string? AfeccionDtos{ get; set; }        
    }
    public  class AntecedentebucodentalDto
    {
        public int IdAntecedenteBucoDentalDto { get; set; }
        public DateTime? UltimaVisitaDentalDto { get; set; }
        public string? OtroDto { get; set; }
        public Fuma FumaDto { get; set; }
        public Bebe BebeDto { get; set; }
    }
    public  class AntecedenteenfermedadDto
    {
        public int IdAntecedenteEnfermedadDto { get; set; }
        public int IdEnfermerdadDto { get; set; }
        public int IdAntecedentePatologicoDto { get; set; }
        public EnfermerdadDto? EnfermerdadDto { get; set; } 
    }
    public  class AntecedentegeneralDto
    {
        public int IdAntecedenteGeneralDto { get; set; }
        public Alergia AlergiaDto { get; set; }
        public string? TipoAlergiaDto { get; set; }
        public HemorragiaDen HemorragiaDentalDto { get; set; }
        public EspecificacionHemorragia EspecificacionHemorragiaDto { get; set; }
        public Embarazo EmbarazoDto { get; set; }
        public string? SemanaEmbarazoDto { get; set; }
    }
    public  class AntecedentehigieneoralDto
    {
        public int IdAntecedenteHigieneOralDto { get; set; }
        public HigieneBucal IdHigieneBucalDto { get; set; }
        public string? FrecuenciaCepilladoDentalDto { get; set; }
        public CepilloDental CepilloDentalDto { get; set; }
        public EnjuagueBucal EnjuagueBucalDto { get; set; }
        public HiloDental IdHiloDentalDto { get; set; }
        public SangradoEncias SangradoEnciasDto { get; set; }
    }
    public  class AntecedentepatologicoDto
    {
        public int IdAntecedentePatologicoDto { get; set; }
        public string? OtroDto { get; set; }
        public string? AntecedentePatologicoFamiliarDto { get; set; }
        public string? TratamientoMedicoDto { get; set; }
        public string? RecibeMedicacionDto { get; set; }        
        public int? AntecedenteEnfermedads { get; set; }
        public List<AntecedenteenfermedadDto>? AntecedenteenfermedadDto { get; set; }
    }
    public  class AvancetratamientoDto
    {
        public int IdAvanceTratamientoDto { get; set; }
        public DateOnly? FechaInicioDto { get; set; } 
        public DateOnly? FechaConclusionDto { get; set; } 
        public string? PiezaDentalDto { get; set; }
        public int IdTratamientoDto { get; set; }
        public string? AvanceDto { get; set; }       
    }

    public class ClinicaDto
    {
        public int IdClinica { get; set; }
        public string? Nombre { get; set; }
        public string? Nit { get; set; }
        public string? Ujsedes { get; set; }
        public string? Pmc { get; set; }
        public string? Direccion { get; set; }
        public string? Celular { get; set; }
        public string? FotoClinica { get; set; }    
        public string? Ciudad { get; set; }
        public string? Pais { get; set; }
        public IFormFile? FotoFile { get; set; }
        public UsuarioDto? Usuario { get; set; }
    }
    public  class EnfermerdadDto
    {
        public int IdEnfermerdadDto { get; set; }
        public string? EnfermedadDto { get; set; }
    }
    public class EstadocivilDto
    {
        public int IdEstadoCivilDto { get; set; }
        public string? EstadoCivil1Dto { get; set; }
    }
    public  class EstadoperiodontaleDto
    {
        public int IdEstadoPeriodontalDto { get; set; }
        public int? CpodCDto { get; set; }
        public int? CpodPDto { get; set; }
        public int? CpodEiDto { get; set; }
        public int? CpodODto { get; set; }
        public int? CpodTotalDto { get; set; }
        public int? CeoCDto { get; set; }
        public int? CeoEDto { get; set; }
        public int? CeoODto { get; set; }
        public int? CeoTotalDto { get; set; }
        public int? TotalPiezasSanasDto { get; set; }
        public int? TotalPiezasDentariasDto { get; set; }
        public int IdOdontogramaDto { get; set; }
    }
    public class ExamenextraoralDto
    {
        public int IdExamenExtraOralDto { get; set; }
        public string? AtmDto { get; set; }
        public string? GanglioLinfaticoDto { get; set; }
        public string? OtroDto { get; set; }
        public RespiracionDto RespiracionDto { get; set; }      
    }
    public class ExamenintraoralDto
    {
        public int IdExamenIntraoralDto { get; set; }
        public string? EnciaDto { get; set; }
        public string? LabioDto { get; set; }
        public string? LenguaDto { get; set; }
        public string? MucosaYugalDto { get; set; }
        public string? PaladarDto { get; set; }
        public string? PisoDeBocaDto { get; set; }
        public ProtesisDental ProtesisDentalDto { get; set; }       
    }
    public class GradoinstruccionDto
    {
        public int IdGradoInstruccionDto { get; set; }
        public string? GradoInstruccion1Dto { get; set; }
    }
    public class HemorragiadentalDto
    {
        public int IdHemorragiaDentalDto { get; set; }
        public HemorragiaDen? HemorragiaDental1Dto { get; set; }
        public EspecificacionHemorragia? EspecificacionHemorragiaDto { get; set; }
    }
    public  class HistorialclinicoDto
    {               
        public string? CodigoHistorialClinicoDto { get; set; } 
        public virtual AntecedentebucodentalDto? AntecedenteBucoDental { get; set; }

        public virtual AntecedentegeneralDto? AntecedenteGeneral { get; set; }

        public virtual AntecedentehigieneoralDto? AntecedenteHigieneOral { get; set; }

        public virtual AntecedentepatologicoDto? AntecedentePatologico { get; set; }

        public virtual ExamenextraoralDto? ExamenExtraOral { get; set; }

        public virtual ExamenintraoralDto? ExamenIntraoral { get; set; }

        public virtual PacienteReportDto? Paciente { get; set; }

        public virtual UsuarioDto? Usuario { get; set; }

        public virtual List<OdontogramaReportDto>? Odontogramas { get; set; } 
        public virtual List<TratamientoDto>? Tratamientos { get; set; } 
    }
    public  class LugarnacimientoDto
    {
        public int IdLugarNacimientoDto { get; set; }
        public string? LugarNacimiento1Dto { get; set; }
    }
    public  class PacienteReportDto
    {
        public int IdPacienteDto { get; set; }
        public Sexo SexoDto { get; set; }
        public string? IdLugarNacimientoDto { get; set; }
        public EstadoCivil IdEstadoCivilDto { get; set; }
        public GradoInstruccion IdGradoInstruccionDto { get; set; }
        public string? ApellidoMaternoDto { get; set; }
        public string? ApellidoPaternoDto { get; set; }
        public string? CelularDto { get; set; }
        public string? DireccionDto { get; set; }
        public DateOnly? FechaNacimientoDto { get; set; }
        public string? NombresDto { get; set; }
        public string? OcupacionDto { get; set; }
        public string? CodigoPacienteDto { get; set; }
    }
    public  class RespiracionDto
    {
        public int IdRespiracionDto { get; set; }
        public string? TipoRespiracionDto { get; set; }
    }
    public  class TratamientoDto
    {
        public int IdTratamientoDto { get; set; }       
        public string? SubjetivoDto { get; set; }       
        public string? ObjetivoDto { get; set; }       
        public string? AnalisisDto { get; set; }      
        public string? PlanAccionDto { get; set; }
        public DateOnly? FechaTratamientoDto { get; set; }
        public sbyte? TratamientoConcluidoDto { get; set; }
        public int IdHistorialClinicoDto { get; set; }
        public decimal ? PresupuestoTotalDto { get; set; }
        public decimal? ACuentaDto { get; set; }
        public decimal? SaldoDto { get; set; }
        public List<PagosTratamientoDto>? PagosTratamientoDtos { get; set; } 
        public List<AvancetratamientoDto>? AvanceTratamientoDtos { get; set; } 
    }
    public class OdontogramaReportDto
    {
        public int Id { get; set; }
        public int? NroPiezaDental { get; set; }
        public string? CaraPiezaDental { get; set; }
        public int IdAfeccion { get; set; }
        public int IdHistorialClinico { get; set; }
    }
    public class PagosTratamientoDto
    {
        public int IdPagoTratamientoDto { get; set; }
        public decimal? MontoDto { get; set; }
        public DateOnly? FechaPagoDto { get; set; }
        public int? IdTratamientoDto { get; set; }
        public virtual TratamientoDto? IdTratamientoNavigationDto { get; set; }
    }
    public class UsuarioDto
    {
        public int IdUsuario { get; set; }
        public string? PrimerNombre { get; set; }
        public string? SegundoNombre { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string? Celular { get; set; }
        public string? FechaNacimiento { get; set; }
        public string? Especialidad { get; set; }      
        public List<ClinicaDto> Clinicas { get; set; }
    }
}
