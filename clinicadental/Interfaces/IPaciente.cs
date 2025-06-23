using clinicadental.Dtos;
using clinicadental.Models;
using Microsoft.AspNetCore.Mvc;

namespace clinicadental.Interfaces
{
    public interface IPaciente 
    {
        Task<PaginacionPacienteDto> GetPacientes(int page, int pageSize,int userId);
        Task<PacienteHistorialDto> Details(string codigoPaciente);
        Task<Paciente> DetailsPaciente(string codigoPaciente);
        Task<DateOnly?[]> ListaHistorial(int? idpaciente);
        Task<Paciente> CreatePaciente(Paciente paciente, int userId);
        Task<bool> EditPaciente(string codigoPaciente, Paciente paciente,int userId);
        Task<bool> DeletePaciente(string codigoPaciente, int userId);

    }
}
