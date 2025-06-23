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
    public class TratamientoesController : Controller
    {
        private readonly ClinicadentalContext _context;

        public TratamientoesController(ClinicadentalContext context)
        {
            _context = context;
        }

        // GET: Tratamientoes
        public async Task<IActionResult> Index()
        {
            var clinicadentalContext = _context.Tratamientos.AsNoTracking().Include(t => t.IdHistorialClinicoNavigation).Include(x => x.PagoTratamiento);           

            return View(await clinicadentalContext.ToListAsync());
        }

        // GET: Tratamientoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewData["IdHistorial"] = id;

            var tratamiento = await _context.Tratamientos
                .Include(t => t.Avancetratamientos)
                 .Include(t => t.IdHistorialClinicoNavigation)
                 .Include(t => t.PagoTratamiento)
                .FirstOrDefaultAsync(m => m.IdHistorialClinico == id);

            var pagos = tratamiento?.PagoTratamiento?.ToList() ?? new List<PagosTratamiento>();

            ViewBag.Pagos = pagos;

            if (tratamiento == null)
            {
                tratamiento = new Tratamiento
                {
                    IdTratamiento = 0,
                    Analisis = "No hay análisis",
                    Objetivo = "No hay objetivo",
                    PlanAccion = "No hay plan de acción",
                    Subjetivo = "No hay subjetivo",
                    PresupuestoTotal = 0,
                       ACuenta = 0,
                       Saldo = 0,
                    FechaTratamiento = DateOnly.FromDateTime(DateTime.UtcNow),
                    TratamientoConcluido = 0,
                    IdHistorialClinico = 0
                };
                return View(tratamiento);
            }
            return View(tratamiento);
        }

        // GET: Tratamientoes/Create
        public IActionResult Create( int id)
        {
            ViewData["IdHistorial"] = id;
            return PartialView("Create", new Tratamiento());
        }

        // POST: Tratamientoes/Create       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTratamiento,Subjetivo,Objetivo,Analisis,PlanAccion,FechaTratamiento,TratamientoConcluido,IdHistorialClinico,PresupuestoTotal,ACuenta,Saldo")] Tratamiento tratamiento)
        {
            if (ModelState.IsValid)
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    var listaTratamientos = await _context.Tratamientos.Where(t => t.IdHistorialClinico == tratamiento.IdHistorialClinico).ToListAsync();

                    if (listaTratamientos.Count > 0)
                        return Json(new { success = false, message = "Ya existe un tratamiento para este historial clínico." });

                    tratamiento.FechaCreacionTratamiento = DateTime.UtcNow;

                    if (tratamiento.ACuenta > 0)
                        tratamiento.Saldo = tratamiento.PresupuestoTotal - tratamiento.ACuenta;
                    else
                    {
                        tratamiento.Saldo = tratamiento.PresupuestoTotal;
                        tratamiento.ACuenta = 0;
                    }
                       

                    tratamiento.FechaTratamiento = DateOnly.FromDateTime(DateTime.UtcNow);
                    tratamiento.TratamientoConcluido = 0;

                    _context.Tratamientos.Add(tratamiento);
                    await _context.SaveChangesAsync();
                  
                    if(tratamiento.ACuenta > 0)
                    {
                        var pagoTratamiento = new PagosTratamiento
                        {
                            IdTratamiento = tratamiento.IdTratamiento,
                            FechaPago = tratamiento.FechaTratamiento,
                            Monto = tratamiento.ACuenta
                        };
                        _context.PagosTratamientos.Add(pagoTratamiento);
                        await _context.SaveChangesAsync();
                    }                 

                    await transaction.CommitAsync();

                    return Json(new
                    {
                        success = true,
                        idTratamiento = tratamiento.IdTratamiento,
                        tratamiento = new
                        {
                            idTratamiento = tratamiento.IdTratamiento,
                            subjetivo = tratamiento.Subjetivo,
                            objetivo = tratamiento.Objetivo,
                            analisis = tratamiento.Analisis,
                            planAccion = tratamiento.PlanAccion,
                            fechaTratamiento = tratamiento.FechaTratamiento,
                            presupuestoTotal = tratamiento.PresupuestoTotal,
                            aCuenta = tratamiento.ACuenta,
                            saldo = tratamiento.Saldo,
                            tratamientoConcluido = tratamiento.TratamientoConcluido,
                            idHistorialClinico = tratamiento.IdHistorialClinico
                        }
                    });
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    ViewData["IdHistorial"] = tratamiento.IdHistorialClinico;
                    return PartialView("Create", tratamiento);
                }              
            }
            ViewData["IdHistorial"] = tratamiento.IdHistorialClinico;
            return PartialView("Create", tratamiento);
        }

        // GET: Tratamientoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["IdHistorial"] = id;

            var tratamiento = await  _context.Tratamientos.FindAsync(id);
            if (tratamiento == null)
            {
                return NotFound();
            }
            return PartialView("~/Views/Tratamientoes/Edit.cshtml", tratamiento);
        }

        // POST: Tratamientoes/Edit/5        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("IdTratamiento,Subjetivo,Objetivo,Analisis,PlanAccion,FechaTratamiento,TratamientoConcluido,IdHistorialClinico,PresupuestoTotal,ACuenta,Saldo, FechaCreacionTratamiento")] Tratamiento tratamiento)
        {
            if (ModelState.IsValid)
            {
                tratamiento.FechaModificacionTratamiento = DateTime.UtcNow;
                tratamiento.Saldo = tratamiento.PresupuestoTotal - tratamiento.ACuenta;
                _context.Update(tratamiento);
                await _context.SaveChangesAsync();
                return Json(new { success = true, tratamiento });
            }
            return PartialView("~/Views/Tratamientoes/Edit.cshtml", tratamiento);
        }

        // GET: Tratamientoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)            
                return NotFound();            

            var tratamiento = await _context.Tratamientos
                .Include(t => t.IdHistorialClinicoNavigation)
                .FirstOrDefaultAsync(m => m.IdTratamiento == id);

            if (tratamiento == null)            
                return NotFound();            

            return View(tratamiento);
        }

        // POST: Tratamientoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tratamiento = await _context.Tratamientos.FindAsync(id);

            if (tratamiento != null)            
                _context.Tratamientos.Remove(tratamiento);            

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TratamientoExists(int id)
        {
            return _context.Tratamientos.Any(e => e.IdTratamiento == id);
        }
    }
}
