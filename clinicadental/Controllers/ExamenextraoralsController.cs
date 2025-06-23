using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using clinicadental.Models;
using clinicadental.dbcontext;
using static clinicadental.Enums.EnumClinicaDental;
using Microsoft.AspNetCore.Authorization;

namespace clinicadental.Controllers
{
    [Authorize]
    public class ExamenextraoralsController : Controller
    {
        private readonly ClinicadentalContext _context;

        public ExamenextraoralsController(ClinicadentalContext context)
        {
            _context = context;
        }

        // GET: Examenextraorals
        public async Task<IActionResult> Index()
        {
            var clinicadentalContext = _context.Examenextraorals.AsNoTracking().Include(e => e.IdRespiracionNavigation);
            return View(await clinicadentalContext.ToListAsync());
        }

        // GET: Examenextraorals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)            
                return NotFound();            

            var examenextraoral = await _context.Examenextraorals.AsNoTracking()
                .Include(e => e.IdRespiracionNavigation)
                .FirstOrDefaultAsync(m => m.IdExamenExtraOral == id);

            if (examenextraoral == null)            
                return NotFound();            

            return View(examenextraoral);
        }

        // GET: Examenextraorals/Create
        public IActionResult Create()
        {
            ViewData["IdRespiracion"] = new SelectList(_context.Respiracions, "IdRespiracion", "TipoRespiracion");
            return View();
        }

        // POST: Examenextraorals/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdExamenExtraOral,Atm,GanglioLinfatico,Otro,IdRespiracion")] Examenextraoral examenextraoral)
        {
            if (ModelState.IsValid)
            {
                _context.Add(examenextraoral);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdRespiracion"] = new SelectList(_context.Respiracions, "IdRespiracion", "TipoRespiracion");
            return View(examenextraoral);
        }

        // GET: Examenextraorals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var examenextraoral = await _context.Examenextraorals.FindAsync(id);

            if (examenextraoral == null)
                return NotFound();
            
            ViewData["IdRespiracion"] = new SelectList(_context.Respiracions, "IdRespiracion", "IdRespiracion", examenextraoral.IdRespiracion);
            return View(examenextraoral);
        }

        // POST: Examenextraorals/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdExamenExtraOral,Atm,GanglioLinfatico,Otro,IdRespiracion")] Examenextraoral examenextraoral)
        {
            if (id != examenextraoral.IdExamenExtraOral)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(examenextraoral);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExamenextraoralExists(examenextraoral.IdExamenExtraOral))
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
            ViewData["IdRespiracion"] = new SelectList(_context.Respiracions, "IdRespiracion", "IdRespiracion", examenextraoral.IdRespiracion);
            return View(examenextraoral);
        }

        // GET: Examenextraorals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)            
                return NotFound();            

            var examenextraoral = await _context.Examenextraorals
                .Include(e => e.IdRespiracionNavigation)
                .FirstOrDefaultAsync(m => m.IdExamenExtraOral == id);

            if (examenextraoral == null)
                return NotFound();

            return View(examenextraoral);
        }

        // POST: Examenextraorals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var examenextraoral = await _context.Examenextraorals.FindAsync(id);

            if (examenextraoral != null)
                _context.Examenextraorals.Remove(examenextraoral);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExamenextraoralExists(int id)
        {
            return _context.Examenextraorals.Any(e => e.IdExamenExtraOral == id);
        }
    }
}
