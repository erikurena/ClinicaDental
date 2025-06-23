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
    public class EnfermerdadController : Controller
    {
        private readonly ClinicadentalContext _context;

        public EnfermerdadController(ClinicadentalContext context)
        {
            _context = context;
        }

        // GET: Enfermerdad
        public async Task<IActionResult> Index()
        {
            return View(await _context.Enfermerdads.AsNoTracking().ToListAsync());
        }

        // GET: Enfermerdad/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)            
                return NotFound();            

            var enfermerdad = await _context.Enfermerdads.AsNoTracking()
                .FirstOrDefaultAsync(m => m.IdEnfermerdad == id);

            if (enfermerdad == null)
                return NotFound();

            return View(enfermerdad);
        }

        // GET: Enfermerdad/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Enfermerdad/Create      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEnfermerdad,Enfermedad")] Enfermerdad enfermerdad)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enfermerdad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(enfermerdad);
        }

        // GET: Enfermerdad/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var enfermerdad = await _context.Enfermerdads.FindAsync(id);

            if (enfermerdad == null)
                return NotFound();
            
            return View(enfermerdad);
        }

        // POST: Enfermerdad/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEnfermerdad,Enfermedad")] Enfermerdad enfermerdad)
        {
            if (id != enfermerdad.IdEnfermerdad)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enfermerdad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnfermerdadExists(enfermerdad.IdEnfermerdad))
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
            return View(enfermerdad);
        }

        // GET: Enfermerdad/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var enfermerdad = await _context.Enfermerdads
                .FirstOrDefaultAsync(m => m.IdEnfermerdad == id);

            if (enfermerdad == null)
                return NotFound();

            return View(enfermerdad);
        }

        // POST: Enfermerdad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enfermerdad = await _context.Enfermerdads.FindAsync(id);

            if (enfermerdad != null)
                _context.Enfermerdads.Remove(enfermerdad);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnfermerdadExists(int id)
        {
            return _context.Enfermerdads.Any(e => e.IdEnfermerdad == id);
        }
    }
}
