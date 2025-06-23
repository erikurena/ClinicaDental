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
    public class AntecedentebucodentalsController : Controller
    {
        private readonly ClinicadentalContext _context;

        public AntecedentebucodentalsController(ClinicadentalContext context)
        {
            _context = context;
        }

        // GET: Antecedentebucodentals
        public async Task<IActionResult> Index()
        {
            return View(await _context.Antecedentebucodentals.AsNoTracking().ToListAsync());
        }

        // GET: Antecedentebucodentals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)            
                return NotFound();            

            var antecedentebucodental = await _context.Antecedentebucodentals.AsNoTracking()
                .FirstOrDefaultAsync(m => m.IdAntecedenteBucoDental == id);

            if (antecedentebucodental == null)            
                return NotFound();            

            return View(antecedentebucodental);
        }

        // GET: Antecedentebucodentals/Create
        public IActionResult Create()
        {
            ViewData["Fuma"] = new SelectList(Enum.GetValues(typeof(Fuma)));
            ViewData["Bebe"] = new SelectList(Enum.GetValues(typeof(Bebe)));
            return View();
        }

        // POST: Antecedentebucodentals/Create       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAntecedenteBucoDental,UltimaVisitaDental,Otro,Fuma,Bebe")] Antecedentebucodental antecedentebucodental)
        {
            if (ModelState.IsValid)
            {
                _context.Add(antecedentebucodental);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Fuma"] = new SelectList(Enum.GetValues(typeof(Fuma)));
            ViewData["Bebe"] = new SelectList(Enum.GetValues(typeof(Bebe)));
            return View(antecedentebucodental);
        }

        // GET: Antecedentebucodentals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)            
                return NotFound();            

            var antecedentebucodental = await _context.Antecedentebucodentals.FindAsync(id);

            if (antecedentebucodental == null)            
                return NotFound();
            
            return View(antecedentebucodental);
        }

        // POST: Antecedentebucodentals/Edit/5     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAntecedenteBucoDental,UltimaVisitaDental,Otro,Fuma,Bebe")] Antecedentebucodental antecedentebucodental)
        {
            if (id != antecedentebucodental.IdAntecedenteBucoDental)            
                return NotFound();            

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(antecedentebucodental);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AntecedentebucodentalExists(antecedentebucodental.IdAntecedenteBucoDental))
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
            return View(antecedentebucodental);
        }

        // GET: Antecedentebucodentals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var antecedentebucodental = await _context.Antecedentebucodentals
                .FirstOrDefaultAsync(m => m.IdAntecedenteBucoDental == id);
            if (antecedentebucodental == null)
            {
                return NotFound();
            }

            return View(antecedentebucodental);
        }

        // POST: Antecedentebucodentals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var antecedentebucodental = await _context.Antecedentebucodentals.FindAsync(id);
            if (antecedentebucodental != null)
            {
                _context.Antecedentebucodentals.Remove(antecedentebucodental);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AntecedentebucodentalExists(int id)
        {
            return _context.Antecedentebucodentals.Any(e => e.IdAntecedenteBucoDental == id);
        }
    }
}
