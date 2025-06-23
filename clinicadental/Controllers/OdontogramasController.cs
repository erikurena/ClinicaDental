using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using clinicadental.Models;
using clinicadental.dbcontext;
using Newtonsoft.Json;
using clinicadental.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace clinicadental.Controllers
{
    [Authorize]
    public class OdontogramasController : Controller
    {
        private readonly ClinicadentalContext _context;

        public OdontogramasController(ClinicadentalContext context)
        {
            _context = context;
        }

        // GET: Odontogramas
        public async Task<IActionResult> Index()
        {
            var clinicadentalContext = _context.Odontogramas.Include(o => o.IdAfeccionNavigation);
            return View(await clinicadentalContext.ToListAsync());
        }

        // GET: Odontogramas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)            
                return NotFound();            

            var odontogramas = await _context.Odontogramas.AsNoTracking()
                                                            .Where(x => x.IdHistorialClinico == id)
                                                            .Select(x => new OdontogramaDto
                                                            {
                                                                NroPiezaDental = x.NroPiezaDental,
                                                                CaraPiezaDental = x.CaraPiezaDental,
                                                                IdAfeccion = x.IdAfeccion,
                                                                IdHistorialClinico = x.IdHistorialClinico
                                                            })
                                                            .ToListAsync();
            ViewBag.IdHistorialClinico = id;
            return View(odontogramas);
        }
        public async Task<IActionResult> DetailsReport(int? id)
        {
            if (id == null)
                return NotFound();

            var odontogramasReport = await _context.Odontogramas.AsNoTracking()
                                                            .Where(x => x.IdHistorialClinico == id)
                                                            .Select(x => new OdontogramaReportDto
                                                            {
                                                                NroPiezaDental  = x.NroPiezaDental,
                                                                CaraPiezaDental = x.CaraPiezaDental,
                                                                IdAfeccion = x.IdAfeccion,
                                                                IdHistorialClinico = x.IdHistorialClinico
                                                            })
                                                            .ToListAsync();
            ViewBag.IdHistorialClinico = id;
            return View(odontogramasReport);
        }

        // GET: Odontogramas/Create
        public async Task<IActionResult> Create(int? id)
        {
            if (id == null)
                return NotFound();

            var odontogramas = await _context.Odontogramas.AsNoTracking()
                                                          .Where(x => x.IdHistorialClinico == id)
                                                          .Select(x => new OdontogramaDto
                                                          {
                                                              NroPiezaDental = x.NroPiezaDental,
                                                              CaraPiezaDental = x.CaraPiezaDental,
                                                              IdAfeccion = x.IdAfeccion,
                                                              IdHistorialClinico = x.IdHistorialClinico
                                                          })
                                                          .ToListAsync();
            ViewBag.IdHistorialClinico = id;
            return View(odontogramas);
        }

        // POST: Odontogramas/Create       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, string Procedimientos)
        {
            if (!ModelState.IsValid)
                return await ReturnViewWithOdontogramas(id);

            if (string.IsNullOrEmpty(Procedimientos))
            {
                ModelState.AddModelError("", "No se enviaron procedimientos para guardar.");
                return await ReturnViewWithOdontogramas(id);
            }

            var listaProcedimientos = JsonConvert.DeserializeObject<List<OdontogramaDto>>(Procedimientos);
            if (listaProcedimientos == null || !listaProcedimientos.Any())
            {
                ModelState.AddModelError("", "La lista de procedimientos está vacía.");
                return await ReturnViewWithOdontogramas(id);
            }

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Determinar si los procedimientos enviados son de adultos o niños
                    bool esAdultos = listaProcedimientos.All(p => p.NroPiezaDental >= 11 && p.NroPiezaDental <= 48);
                    bool esNinos = listaProcedimientos.All(p => (p.NroPiezaDental >= 51 && p.NroPiezaDental <= 65) ||
                                                                (p.NroPiezaDental >= 71 && p.NroPiezaDental <= 85));

                    // Obtener los procedimientos existentes en la base de datos para este IdHistorialClinico
                    var existentes = await _context.Odontogramas.Where(x => x.IdHistorialClinico == id).ToListAsync();

                    // Filtrar los procedimientos a eliminar según el rango de dientes enviados
                    var aEliminar = existentes.Where(e =>
                        (esAdultos && e.NroPiezaDental >= 11 && e.NroPiezaDental <= 48) ||
                        (esNinos && ((e.NroPiezaDental >= 51 && e.NroPiezaDental <= 65) ||
                                     (e.NroPiezaDental >= 71 && e.NroPiezaDental <= 85)))
                        && !listaProcedimientos.Any(p => p.NroPiezaDental == e.NroPiezaDental &&
                                                         p.CaraPiezaDental == e.CaraPiezaDental &&
                                                         p.IdAfeccion == e.IdAfeccion)).ToList();

                    if (aEliminar.Count > 0)
                        _context.Odontogramas.RemoveRange(aEliminar);

                    // Procesar los procedimientos enviados (insertar o actualizar)
                    foreach (var proc in listaProcedimientos)
                    {
                        var existente = existentes.FirstOrDefault(x => x.NroPiezaDental == proc.NroPiezaDental &&
                                                                      x.CaraPiezaDental == proc.CaraPiezaDental &&
                                                                      x.IdAfeccion == proc.IdAfeccion);

                        if (existente == null)
                        {
                            // No existe, agregar un nuevo registro
                            var odontograma = new Odontograma
                            {
                                IdHistorialClinico = id,
                                NroPiezaDental = proc.NroPiezaDental,
                                CaraPiezaDental = proc.CaraPiezaDental,
                                IdAfeccion = proc.IdAfeccion
                            };
                            _context.Odontogramas.Add(odontograma);
                        }
                        else
                        {
                            // Existe, actualizar el registro existente
                            existente.IdAfeccion = proc.IdAfeccion; // Solo actualizamos IdAfeccion, ajusta según necesidades
                            _context.Odontogramas.Update(existente);
                        }
                    }

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    // En lugar de redirigir a Index, devolver la vista Create con los datos actualizados
                    return await ReturnViewWithOdontogramas(id);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    Console.WriteLine($"Error al guardar el odontograma: {ex.Message}");
                    ModelState.AddModelError("", "Ocurrió un error al guardar los procedimientos. Por favor, intenta de nuevo.");
                    return await ReturnViewWithOdontogramas(id);
                }
            }
        }

        // Método auxiliar para evitar duplicación al devolver la vista
        private async Task<IActionResult> ReturnViewWithOdontogramas(int id)
        {
            var odontogramas = await _context.Odontogramas.AsNoTracking()
                                                                            .Where(x => x.IdHistorialClinico == id)
                                                                            .Select(x => new OdontogramaDto
                                                                            {
                                                                                NroPiezaDental = x.NroPiezaDental,
                                                                                CaraPiezaDental = x.CaraPiezaDental,
                                                                                IdAfeccion = x.IdAfeccion,
                                                                                IdHistorialClinico = x.IdHistorialClinico
                                                                            })
                                                                            .ToListAsync();
            ViewBag.IdHistorialClinico = id;
            return View(odontogramas);
        }

        // GET: Odontogramas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var odontograma = await _context.Odontogramas.FindAsync(id);

            if (odontograma == null)
                return NotFound();
            
            ViewData["IdAfeccion"] = new SelectList(_context.Afeccions, "IdAfeccion", "IdAfeccion", odontograma.IdAfeccion);
            return View(odontograma);
        }

        // POST: Odontogramas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdOdontograma,NroPiezaDental,CaraPiezaDental,IdAfeccion")] Odontograma odontograma)
        {
            if (id != odontograma.IdOdontograma)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(odontograma);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OdontogramaExists(odontograma.IdOdontograma))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAfeccion"] = new SelectList(_context.Afeccions, "IdAfeccion", "IdAfeccion", odontograma.IdAfeccion);
            return View(odontograma);
        }

        // GET: Odontogramas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var odontograma = await _context.Odontogramas
                .Include(o => o.IdAfeccionNavigation)
                .FirstOrDefaultAsync(m => m.IdOdontograma == id);

            if (odontograma == null)
                return NotFound();

            return View(odontograma);
        }

        // POST: Odontogramas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var odontograma = await _context.Odontogramas.FindAsync(id);

            if (odontograma != null)
                _context.Odontogramas.Remove(odontograma);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OdontogramaExists(int id)
        {
            return _context.Odontogramas.Any(e => e.IdOdontograma == id);
        }
    }
}
