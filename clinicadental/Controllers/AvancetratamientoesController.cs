using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using clinicadental.Models;
using clinicadental.dbcontext;
using Microsoft.AspNetCore.Authorization;

namespace clinicadental.Controllers
{
    [Authorize]
    public class AvancetratamientoesController : Controller
    {
        private readonly ClinicadentalContext _context;

        public AvancetratamientoesController(ClinicadentalContext context)
        {
            _context = context;
        }

        // GET: Avancetratamientoes
        public async Task<IActionResult> Index()
        {
            var clinicadentalContext = _context.Avancetratamientos.AsNoTracking().Include(a => a.IdTratamientoNavigation);
            return View(await clinicadentalContext.ToListAsync());
        }

        // GET: Avancetratamientoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)            
                return NotFound();            

            var avancetratamiento = await _context.Avancetratamientos.AsNoTracking()
                                                                                .Include(a => a.IdTratamientoNavigation)
                                                                                .FirstOrDefaultAsync(m => m.IdAvanceTratamiento == id);
            if (avancetratamiento == null)            
                return NotFound();            

            return View(avancetratamiento);
        }

        // GET: Avancetratamientoes/Create
        public async Task<IActionResult> Create(int? id)
        {
            ViewData["IdTratamiento"] = new SelectList(await _context.Tratamientos.ToListAsync(), "IdTratamiento", "IdTratamiento");

            var piezasDentales = await _context.Odontogramas.AsNoTracking()
                .Include(o => o.IdAfeccionNavigation)
                .Include(o => o.IdHistorialClinicoNavigation)
                    .ThenInclude(h => h.Tratamientos)
                .Where(o => o.IdHistorialClinicoNavigation.Tratamientos.Any(t => t.IdTratamiento == id))
                .Select(o => new
                {
                    Value = $"Pieza dental Nro: {o.NroPiezaDental} {o.CaraPiezaDental} - {o.IdAfeccionNavigation.Afeccion1}",
                    Text = $"Pieza dental Nro: {o.NroPiezaDental} {o.CaraPiezaDental} - {o.IdAfeccionNavigation.Afeccion1}"
                })
                .ToListAsync();

            // Crear una nueva lista que incluya el elemento "Otro"
            var piezasConOtro = piezasDentales.ToList();
            piezasConOtro.Add(new { Value = "Otro", Text = "Otro" });

            ViewData["PiezaDental"] = new SelectList(piezasConOtro, "Value", "Text");

            var avanceTratamiento = new Avancetratamiento
            {
                IdTratamiento = id.Value
            };
            return PartialView("Create", avanceTratamiento);
        }

        // POST: Avancetratamientoes/Create       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAvanceTratamiento,FechaInicio,FechaConclusion,Avance,IdTratamiento")] Avancetratamiento avancetratamiento, string piezaDentalId)
        {
            if (ModelState.IsValid)
            {
                avancetratamiento.PiezaDental = piezaDentalId; // Asignar la pieza dental seleccionada
                _context.Add(avancetratamiento);
                await _context.SaveChangesAsync();
                return Json(new
                {
                    success = true,
                    avance = new
                    {
                        idAvanceTratamiento = avancetratamiento.IdAvanceTratamiento,                       
                        fechaInicio = avancetratamiento.FechaInicio?.ToString("dd/MM/yyyy"),
                        fechaConclusion = avancetratamiento.FechaConclusion?.ToString("dd/MM/yyyy"),
                        piezaDental = avancetratamiento.PiezaDental,
                        avance = avancetratamiento.Avance
                    }
                });
            }
            return PartialView("Create", avancetratamiento);
        }

        // GET: Avancetratamientoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)            
                return NotFound();            

            var avancetratamiento = await _context.Avancetratamientos.FindAsync(id);

            if (avancetratamiento == null)            
                return NotFound();
            
            var piezasDentales = await _context.Odontogramas.AsNoTracking()
                                                                        .Include(o => o.IdAfeccionNavigation)
                                                                        .Include(o => o.IdHistorialClinicoNavigation)
                                                                           .ThenInclude(h => h.Tratamientos)
                                                                        .Where(o => o.IdHistorialClinicoNavigation.Tratamientos.Any(t => t.IdTratamiento == avancetratamiento.IdTratamiento))
                                                                        .Select(o => new
                                                                        {
                                                                            Value = $"Pieza dental Nro: {o.NroPiezaDental} {o.CaraPiezaDental} - {o.IdAfeccionNavigation.Afeccion1}",
                                                                            Text = $"Pieza dental Nro: {o.NroPiezaDental} {o.CaraPiezaDental} - {o.IdAfeccionNavigation.Afeccion1}"
                                                                        })
                                                                        .ToListAsync();

            //crear una nueva lista que incluya el elemento "Otro"
            var listaconOtro = piezasDentales.ToList();
            listaconOtro.Add(new { Value= "Otro", Text="Otro"});         

            ViewData["PiezaDentalEdit"] = new SelectList(listaconOtro, "Value", "Text",avancetratamiento.PiezaDental);
            ViewData["IdTratamiento"] = new SelectList(_context.Tratamientos, "IdTratamiento", "IdTratamiento", avancetratamiento.IdTratamiento);
            return View(avancetratamiento);
        }

        // POST: Avancetratamientoes/Edit/5       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("IdAvanceTratamiento,FechaInicio,FechaConclusion,Avance,IdTratamiento")] Avancetratamiento avancetratamiento, string piezaDentalIdEdit)
        {                
            if (ModelState.IsValid)
            {
                try
                {
                    avancetratamiento.PiezaDental = piezaDentalIdEdit; 
                    _context.Update(avancetratamiento);
                    await _context.SaveChangesAsync();
                    return Json(new
                    {
                        success = true,
                        avance = new
                        {
                            idAvanceTratamiento = avancetratamiento.IdAvanceTratamiento,
                            fechaInicio = avancetratamiento.FechaInicio?.ToString("dd/MM/yyyy"),
                            fechaConclusion = avancetratamiento.FechaConclusion?.ToString("dd/MM/yyyy"),
                            piezaDental = avancetratamiento.PiezaDental,
                            avance = avancetratamiento.Avance
                        }
                    });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AvancetratamientoExists(avancetratamiento.IdAvanceTratamiento))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }                
            }
            ViewData["IdTratamiento"] = new SelectList(_context.Tratamientos, "IdTratamiento", "IdTratamiento", avancetratamiento.IdTratamiento);
            return View(avancetratamiento);
        }

        // GET: Avancetratamientoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)            
                return NotFound();            

            var avancetratamiento = await _context.Avancetratamientos
                .Include(a => a.IdTratamientoNavigation)
                .FirstOrDefaultAsync(m => m.IdAvanceTratamiento == id);

            if (avancetratamiento == null)            
                return NotFound();            

            return View(avancetratamiento);
        }

        // POST: Avancetratamientoes/Delete/5
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var avancetratamiento = await _context.Avancetratamientos.FindAsync(id);

            if (avancetratamiento == null)            
                return Json(new { success = false, message = "El avance no fue encontrado." });            

            _context.Avancetratamientos.Remove(avancetratamiento);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Avance eliminado correctamente." });
        }

        private bool AvancetratamientoExists(int id)
        {
            return _context.Avancetratamientos.Any(e => e.IdAvanceTratamiento == id);
        }
    }
}
