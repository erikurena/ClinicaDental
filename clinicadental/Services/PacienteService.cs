using clinicadental.dbcontext;
using clinicadental.Dtos;
using clinicadental.Interfaces;
using clinicadental.Models;
using Microsoft.EntityFrameworkCore;

namespace clinicadental.Services
{
    public class PacienteService : IPaciente
    {
        private readonly ClinicadentalContext _context;
        public PacienteService(ClinicadentalContext context)
        {
            _context = context;
        }
        public async Task<PaginacionPacienteDto> GetPacientes(int page, int pageSize, int userId)
        {

            var totalPatients = await _context.Pacientes.AsNoTracking().CountAsync(p => p.IdUsuario == userId);

            var totalPages = (int)Math.Ceiling((double)totalPatients / pageSize);

            page = Math.Max(1, Math.Min(page, totalPages));

            var pacientes = await _context.Pacientes.AsNoTracking().Where(p => p.IdUsuario == userId)
                                                                                                                 .OrderByDescending(p => p.IdPaciente)
                                                                                                                .Skip((page - 1) * pageSize)
                                                                                                                .Take(pageSize)
                                                                                                                .ToListAsync();

            return new PaginacionPacienteDto
            {
                Pacientes = pacientes,
                PaginaActual = page,
                TotalPaginas = totalPages,
                TamañoPagina = pageSize
            };
        }
        public async Task<PacienteHistorialDto> Details(string codigoPaciente)
        {
            var paciente = await DetailsPaciente(codigoPaciente);

            if (paciente == null) return null;

            var listaHistoriales = await _context.Historialclinicos.AsNoTracking().Where(x => x.IdPaciente == paciente.IdPaciente)
                                                                                                                                      .Where(x => x.FechaHistorial.HasValue)
                                                                                                                                      .Select(x => new HistorialItemDto
                                                                                                                                      {
                                                                                                                                          IdHistorial = x.IdHistorialClinico,
                                                                                                                                          FechaHistorial = x.FechaHistorial.Value,
                                                                                                                                          MotivoCita = x.MotivoCita
                                                                                                                                      }).ToListAsync();

            var PacienteHistorialdto = new PacienteHistorialDto
            {
                Paciente = paciente,
                Historiales = listaHistoriales
            };
            return PacienteHistorialdto;
        }
        public async Task<Paciente> DetailsPaciente(string codigoPaciente)
        {
            var paciente = await _context.Pacientes.FirstOrDefaultAsync(m => m.CodigoPaciente == codigoPaciente);
            return paciente;
        }
        public async Task<DateOnly?[]> ListaHistorial(int? idpaciente)
        {
            var listaHistorialPaciente = await _context.Historialclinicos.AsNoTracking().Where(x => x.IdPaciente == idpaciente)
                                                                                                                                             .Select(x => x.FechaHistorial).ToArrayAsync();

            return listaHistorialPaciente;
        }
        public async Task<Paciente> CreatePaciente(Paciente paciente, int userId)
        {
            paciente.FechaCreacionPaciente = DateTime.UtcNow;
            paciente.FechaModificacionPaciente = DateTime.UtcNow;
            paciente.CodigoPaciente = Guid.NewGuid().ToString();
            paciente.IdUsuario = userId;

            _context.Pacientes.Add(paciente);
            await _context.SaveChangesAsync();
            return paciente;
        }
        public async Task<bool> EditPaciente(string codigoPaciente, Paciente paciente,int userId)
        {
            try
            {
                paciente.FechaModificacionPaciente = DateTime.UtcNow;
                paciente.IdUsuario = userId;
                _context.Update(paciente);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> DeletePaciente(string codigoPaciente, int userId)
        {
            try
            {
                var paciente = await _context.Pacientes.FirstOrDefaultAsync(m => m.CodigoPaciente == codigoPaciente && m.IdUsuario == userId);

                if (paciente == null) return false;
                _context.Pacientes.Remove(paciente);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
     }
}
