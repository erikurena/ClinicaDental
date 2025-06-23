using clinicadental.dbcontext;
using clinicadental.Interfaces;
using clinicadental.Models;
using Microsoft.EntityFrameworkCore;

namespace clinicadental.Services
{
    public class ClinicaService : IClinica
    {
        private readonly ClinicadentalContext _context;
        public ClinicaService(ClinicadentalContext context)
        {
            _context = context;
        }
        public async Task<List<Clinica>> GetClinica(int userId)
        {
            return await _context.Clinicas.Where(x => x.IdUsuario == userId).ToListAsync();
        }
        public async Task<Clinica> DetailsClinica(int? id)
        {
            var result = await _context.Clinicas.AsNoTracking().FirstOrDefaultAsync(m => m.IdClinica == id);

            if (result == null)
                return null;

            return result;
        }
        public async Task<bool> CreateClinica(Clinica clinica, int userId)
        {
            try
            {
                clinica.IdUsuario = userId;
                _context.Clinicas.Add(clinica);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> EditClinica(Clinica clinica, int? userId)
        {
            try
            {
                clinica.IdUsuario = userId;
                _context.Update(clinica);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> DeleteClinica(int id)
        {
            try
            {
                var clinica = await DetailsClinica(id);

                if (clinica == null)
                    return false;

                _context.Clinicas.Remove(clinica);
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
