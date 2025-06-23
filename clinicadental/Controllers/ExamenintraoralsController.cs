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
    public class ExamenintraoralsController : Controller
    {
        private readonly ClinicadentalContext _context;

        public ExamenintraoralsController(ClinicadentalContext context)
        {
            _context = context;
        }

        // GET: Examenintraorals
        public async Task<IActionResult> Index()
        {
            return View(await _context.Examenintraorals.AsNoTracking().ToListAsync());
        }

        // GET: Examenintraorals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();
            
            var examenintraoral = await _context.Examenintraorals.AsNoTracking()
                .FirstOrDefaultAsync(m => m.IdExamenIntraoral == id);

            if (examenintraoral == null)
                return NotFound();

            return View(examenintraoral);
        }

        // GET: Examenintraorals/Create
        public IActionResult Create()
        {
            ViewData["Protesis"] = new SelectList(Enum.GetValues(typeof(ProtesisDental)));
            return View();
        }

        // POST: Examenintraorals/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdExamenIntraoral,Encia,Labio,Lengua,MucosaYugal,Paladar,PisoDeBoca,ProtesisDental")] Examenintraoral examenintraoral)
        {
            if (ModelState.IsValid)
            {
                _context.Add(examenintraoral);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Protesis"] = new SelectList(Enum.GetValues(typeof(ProtesisDental)));
            return View(examenintraoral);
        }

        // GET: Examenintraorals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var examenintraoral = await _context.Examenintraorals.FindAsync(id);

            if (examenintraoral == null)
                return NotFound();
            
            return View(examenintraoral);
        }

        // POST: Examenintraorals/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdExamenIntraoral,Encia,Labio,Lengua,MucosaYugal,Paladar,PisoDeBoca,ProtesisDental")] Examenintraoral examenintraoral)
        {
            if (id != examenintraoral.IdExamenIntraoral)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(examenintraoral);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExamenintraoralExists(examenintraoral.IdExamenIntraoral))
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
            return View(examenintraoral);
        }

        // GET: Examenintraorals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var examenintraoral = await _context.Examenintraorals
                .FirstOrDefaultAsync(m => m.IdExamenIntraoral == id);

            if (examenintraoral == null)
                return NotFound();

            return View(examenintraoral);
        }

        // POST: Examenintraorals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var examenintraoral = await _context.Examenintraorals.FindAsync(id);

            if (examenintraoral != null)
                _context.Examenintraorals.Remove(examenintraoral);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExamenintraoralExists(int id)
        {
            return _context.Examenintraorals.Any(e => e.IdExamenIntraoral == id);
        }
    }
}
