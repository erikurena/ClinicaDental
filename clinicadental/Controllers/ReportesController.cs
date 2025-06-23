using clinicadental.dbcontext;
using clinicadental.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace clinicadental.Controllers
{
    public class ReportesController : Controller
    {
        private readonly ClinicadentalContext _context;

        public ReportesController(ClinicadentalContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ReportePacientes()
        {
            return View();
        }
       
        [HttpPost]
        public async Task<IActionResult> ReportePacientes(DateTime FechaInicio, DateTime FechaFin,int pagina = 1, int tamanoPagina = 10)
        {
            if (FechaInicio > FechaFin)
            {
                ModelState.AddModelError("", "La fecha de inicio no puede ser mayor que la fecha de fin.");
                return View();
            }

            //añadir idusuario
            var IdUsuario = Convert.ToInt32(User.FindFirst("IdNumUsuario")?.Value);
            FechaFin = FechaFin.Date.AddDays(1).AddTicks(-1);

            var totalPacientes = await _context.Pacientes.AsNoTracking().CountAsync(p => p.IdUsuario == IdUsuario && 
                                                                                                p.FechaCreacionPaciente >= FechaInicio && 
                                                                                                p.FechaCreacionPaciente <= FechaFin);

            var listaPacientes = await _context.Pacientes.AsNoTracking().Where(p => p.IdUsuario == IdUsuario && 
                                                                                                p.FechaCreacionPaciente >= FechaInicio && 
                                                                                                p.FechaCreacionPaciente <= FechaFin)
                                                                                                .Join( _context.Historialclinicos,
                                                                                                            p => p.IdPaciente,
                                                                                                            hc => hc.IdPaciente,
                                                                                                            (p, hc) => new { p, hc }
                                                                                                )
                                                                                                .GroupJoin( _context.Tratamientos,
                                                                                                phc => phc.hc.IdHistorialClinico,
                                                                                                t => t.IdHistorialClinico,
                                                                                                (phc, tratamientos) =>new {phc.p, phc.hc, tratamientos})
                                                                                                .SelectMany(
                                                                                                    x => x.tratamientos.DefaultIfEmpty(),
                                                                                                    (x,t) => new
                                                                                                {
                                                                                                    x.p.Nombres,
                                                                                                    x.p.ApellidoPaterno,
                                                                                                    x.p.ApellidoMaterno,
                                                                                                    x.p.Celular,
                                                                                                    x.p.IdLugarNacimiento,
                                                                                                    x.p.FechaCreacionPaciente,
                                                                                                    x.hc.MotivoCita,
                                                                                                    PlanAccion = t != null ? t.PlanAccion : "No hay tratamiento asignado",
                                                                                                    }).OrderBy(p => p.FechaCreacionPaciente)
                                                                                                .Skip((pagina - 1) * tamanoPagina)
                                                                                                .Take(tamanoPagina)
                                                                                                .ToListAsync();

            return Json(new
            {
                datos = listaPacientes,
                totalPacientes,
                paginaActual = pagina,
                totalPaginas = (int)Math.Ceiling((double)totalPacientes / tamanoPagina)
            });
        }
    }
}
