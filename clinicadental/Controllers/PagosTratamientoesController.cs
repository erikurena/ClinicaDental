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
    public class PagosTratamientoesController : Controller
    {
        private readonly ClinicadentalContext _context;

        public PagosTratamientoesController(ClinicadentalContext context)
        {
            _context = context;
        }

        // GET: PagosTratamientoes
        public  IActionResult Index()
        {
            var pagosTratamientos = _context.PagosTratamientos.AsNoTracking()
                .Include(p => p.IdTratamientoNavigation)
                .OrderByDescending(p => p.FechaPago)
                .ToList();

            return View(pagosTratamientos);
        }

        // GET: PagosTratamientoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)            
                return NotFound();            

            var pagosTratamiento = await _context.PagosTratamientos.AsNoTracking()
                .FirstOrDefaultAsync(m => m.IdPagoTratamiento == id);

            if (pagosTratamiento == null)            
                return NotFound();
            
            return View(pagosTratamiento);
        }

        // GET: PagosTratamientoes/Create
        public IActionResult Create(int? id)
        {
            var model = new PagosTratamiento { IdTratamiento = id };
            return PartialView("Create",model);
        }

        // POST: PagosTratamientoes/Create       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPagoTratamiento,Monto,FechaPago,IdTratamiento")] PagosTratamiento pagosTratamiento)
        {
            if (ModelState.IsValid)
            {
                var tratamiento = await _context.Tratamientos.FindAsync(pagosTratamiento.IdTratamiento);

                if(pagosTratamiento.Monto > tratamiento.Saldo || pagosTratamiento.Monto == 0)
                    return PartialView("Create", pagosTratamiento);
               
                tratamiento.ACuenta += pagosTratamiento.Monto;
                tratamiento.Saldo -= pagosTratamiento.Monto;
                _context.Update(tratamiento);

                _context.Add(pagosTratamiento);
                await _context.SaveChangesAsync();
                return Json(new 
                { 
                    success = true
                   
                });
            }          
            return PartialView("Create", pagosTratamiento);
        }

        // GET: PagosTratamientoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();            

            var pagosTratamiento = await _context.PagosTratamientos.FindAsync(id);

            if (pagosTratamiento == null)
                return NotFound();                       
          
            return PartialView("~/Views/PagosTratamientoes/Edit.cshtml", pagosTratamiento);
        }

        // POST: PagosTratamientoes/Edit/5       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPagoTratamiento,Monto,FechaPago,IdTratamiento")] PagosTratamiento pagosTratamiento)
        {             
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pagosTratamiento);

                    var tratamiento = await _context.Tratamientos.FindAsync(pagosTratamiento.IdTratamiento);

                    if (pagosTratamiento.Monto > tratamiento.Saldo || pagosTratamiento.Monto <= 0)                    
                        return PartialView("~/Views/PagosTratamientoes/Edit.cshtml", pagosTratamiento);

                    var montoPagoBd = await _context.PagosTratamientos.Where(p => p.IdPagoTratamiento == pagosTratamiento.IdPagoTratamiento)
                                                                                                                    .Select(p => p.Monto)
                                                                                                                    .FirstOrDefaultAsync();
                    var diferencia =  pagosTratamiento.Monto - montoPagoBd;
                    tratamiento.ACuenta += diferencia;
                    tratamiento.Saldo -= diferencia;
                    _context.Update(tratamiento);

                    await _context.SaveChangesAsync();
                    return Json(new { success = true });
                }
                catch (DbUpdateConcurrencyException)
                {
                    return PartialView("~/Views/PagosTratamientoes/Edit.cshtml", pagosTratamiento);
                }              
            }
            return PartialView("~/Views/PagosTratamientoes/Edit.cshtml", pagosTratamiento);
        }

        // POST: PagosTratamientoes/Delete/5
        [HttpPost]     
        public async Task<IActionResult> Delete(int id)
        {
            var pagoTratamiento = await _context.PagosTratamientos.FindAsync(id);

            if (pagoTratamiento == null)            
                return Json(new { success = false, message = "Pago no encontrado." });            

            try
            {
                var tratamiento = await _context.Tratamientos.FindAsync(pagoTratamiento.IdTratamiento);

                _context.PagosTratamientos.Remove(pagoTratamiento);

                tratamiento.ACuenta -= pagoTratamiento.Monto;
                tratamiento.Saldo += pagoTratamiento.Monto;
                _context.Update(tratamiento);

                await _context.SaveChangesAsync();
                return Json(new { success = true });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Error al eliminar el pago." });
            }
        }

        private bool PagosTratamientoExists(int id)
        {
            return _context.PagosTratamientos.Any(e => e.IdPagoTratamiento == id);
        }
    }
}
