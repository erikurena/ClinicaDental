using clinicadental.Models;

namespace clinicadental.Interfaces
{
    public interface IClinica
    {
        Task<List<Clinica>> GetClinica(int userId);
        Task<Clinica> DetailsClinica(int? id);
        Task<bool> CreateClinica(Clinica clinica, int userId);
        Task<bool> EditClinica(Clinica clinica, int? userId);
        Task<bool> DeleteClinica(int id);
    }
}
