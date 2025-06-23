using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using clinicadental.Models;
using clinicadental.dbcontext;
using clinicadental.Dtos;
using System.Text.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace clinicadental.Controllers
{
    [Authorize]
    public class CitasController : Controller
    {
        private readonly ClinicadentalContext _context;

        public CitasController(ClinicadentalContext context)
        {
            _context = context;
        }

        // GET: Citas
        public IActionResult Index()
        {          
            return View();
        }

        // GET: Citas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)            
                return NotFound();            

            var cita = await _context.Citas.Include(c => c.UsuarioNavigation).FirstOrDefaultAsync(m => m.IdCita == id);

            if (cita == null)            
                return NotFound();            

            return View(cita);
        }

        // GET: Citas/Create
        public IActionResult Create(string start, string end)
        {
            try
            {
                var model = new Cita();
                return PartialView("Create", model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        // POST: Citas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCita,FechaHoraCita,HorainicioCita,HorafinCita,NombrePaciente,MotivoCita,EstadoCita,FechaRegistro,CorreoElectronico,Telefono,IdUsuario")] Cita cita)
        {
            if (!ModelState.IsValid)            
                return PartialView("Create", cita);

            // Verificar si ya existe una cita en el mismo rango de fecha y hora
            var fechaInicio = cita.HorainicioCita;
            var fechaFin = cita.HorafinCita;
            var duracion = fechaFin - fechaInicio;

            if (duracion.TotalHours > 8 || fechaInicio.Day != fechaFin.Day)
                return Json(new { success = false, message = "Área seleccionada demasiado grande o no permitida." });

            var IdUsuario = Convert.ToInt32(User.FindFirst("IdNumUsuario")?.Value);

            var citaExistente = _context.Citas.AsNoTracking()
                                                            .Any(c => c.FechaHoraCita == cita.FechaHoraCita && // Misma fecha
                                                                        ((c.HorainicioCita <= cita.HorainicioCita && c.HorafinCita > cita.HorainicioCita) || // Inicio dentro de otra cita
                                                                        (c.HorainicioCita < cita.HorafinCita && c.HorafinCita >= cita.HorafinCita) ||    // Fin dentro de otra cita
                                                                        (c.HorainicioCita >= cita.HorainicioCita && c.HorafinCita <= cita.HorafinCita)) 
                                                            && c.IdUsuario == IdUsuario); // Cita completamente dentro de otra

            if (citaExistente)
                return Json(new { success = false, message = "Ya existe una cita en ese horario." });

            cita.IdUsuario = IdUsuario;
            _context.Add(cita);
            await _context.SaveChangesAsync();
            return Json(new { success = true, message = "Cita guardada correctamente" });
        }

        public IActionResult GetCitas(DateTime start, DateTime end)
        {
            try
            {
                var idusuario = Convert.ToInt32(User.FindFirst("IdNumUsuario")?.Value);
                // Obtener las citas de la base de datos dentro del rango de fechas
                var citas = _context.Citas.AsNoTracking().Where(c => c.IdUsuario == idusuario).Where(c => c.HorainicioCita >= start && c.HorafinCita <= end)
                                                            .Select(c => new
                                                            {
                                                                id = c.IdCita,
                                                                title = $"{c.NombrePaciente} - {c.MotivoCita}",
                                                                start = c.FechaHoraCita.ToString("yyyy-MM-dd") + "T" + c.HorainicioCita.ToString("HH:mm:ss"),
                                                                end = c.FechaHoraCita.ToString("yyyy-MM-dd") + "T" + c.HorafinCita.ToString("HH:mm:ss"),
                                                                extendedProps = new
                                                                {
                                                                    celular = c.Telefono 
                                                                }
                                                            }).ToList();

                return Json(citas);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetCitas: {ex.Message}");
                return StatusCode(500, "Error al obtener las citas");
            }
        }

        // GET: Citas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)            
                return NotFound();            

            var cita = await _context.Citas.FindAsync(id);

            if (cita == null)            
                return NotFound();            
            
            return View(cita);
        }

        // POST: Citas/Edit/5   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCita,NombrePaciente,MotivoCita,EstadoCita,CorreoElectronico,Telefono")] Cita cita)
        {
            if (id != cita.IdCita)            
                return NotFound();            

            if (ModelState.IsValid)
            {
                try
                {
                    // Buscar la cita existente
                    var citaExistente = await _context.Citas.FindAsync(id);
                    if (citaExistente == null)                    
                        return NotFound();                    

                    // Actualizar solo los campos especificados
                    citaExistente.NombrePaciente = cita.NombrePaciente;
                    citaExistente.MotivoCita = cita.MotivoCita;
                    citaExistente.EstadoCita = cita.EstadoCita;
                    citaExistente.CorreoElectronico = cita.CorreoElectronico;
                    citaExistente.Telefono = cita.Telefono;                   

                    _context.Update(citaExistente);
                    await _context.SaveChangesAsync();

                    return Json(new { success = true });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CitaExists(cita.IdCita))                    
                        return NotFound();
                    
                    return Json(new { success = false, message = "Error de concurrencia al actualizar la cita." });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = $"Error al actualizar la cita: {ex.Message}" });
                }
            }

            // Si el modelo no es válido, devolver la vista parcial con errores
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")            
                return PartialView("_Edit", cita);            

            // Fallback para navegadores sin AJAX
            
            return View(cita);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateDate([FromBody] UpdateCitaDto model)
        {
            if (model == null)            
                return Json(new { success = false, message = "El modelo recibido es null" });            

            var cita = await _context.Citas.FindAsync(model.IdCita);
            if (cita == null)            
                return Json(new { success = false, message = "Cita no encontrada" });            

            var IdUsuario = Convert.ToInt32(User.FindFirst("IdNumUsuario")?.Value);

            if (IdUsuario == 0)            
                return Json(new { success = false, message = "Usuario no encontrado" });

            var fechaInicio = model.HorainicioCita;
            var fechaFin = model.HorafinCita;

            var duracion = fechaFin - fechaInicio;
            if (duracion.TotalHours > 8 || fechaInicio.Day != fechaFin.Day)            
                return Json(new { success = false, message = "Área seleccionada demasiado grande o no permitida." });            

            var citaExistente = _context.Citas.AsNoTracking()
                                                                 .Any(c => c.IdCita != model.IdCita &&  c.FechaHoraCita == model.FechaHoraCita &&
                                                                          ((c.HorainicioCita <= model.HorainicioCita && c.HorafinCita > model.HorainicioCita) ||
                                                                           (c.HorainicioCita < model.HorafinCita && c.HorafinCita >= model.HorafinCita) ||
                                                                           (c.HorainicioCita >= model.HorainicioCita && c.HorafinCita <= model.HorafinCita))
                                                                           && c.IdUsuario == IdUsuario);

            if (citaExistente)            
                return Json(new { success = false, message = "El nuevo horario ya está ocupado" });            

            cita.FechaHoraCita = model.FechaHoraCita;
            cita.HorainicioCita = model.HorainicioCita;
            cita.HorafinCita = model.HorafinCita;

            _context.Update(cita);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Fecha actualizada correctamente" });
        }
        // GET: Citas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)            
                return NotFound();            

            var cita = await _context.Citas
                .Include(c => c.UsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdCita == id);

            if (cita == null)            
                return NotFound();            

            return View(cita);
        }

        // POST: Citas/Delete/5
        [HttpPost, ActionName("Delete")]      
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var cita = await _context.Citas.FindAsync(id);
                if (cita == null)                
                    return Json(new { success = false, message = "La cita no fue encontrada." });                

                _context.Citas.Remove(cita);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Cita eliminada correctamente." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error al eliminar la cita: {ex.Message}" });
            }
        }

        private bool CitaExists(int id)
        {
            return _context.Citas.Any(e => e.IdCita == id);
        }
    }
}
