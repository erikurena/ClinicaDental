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
    public class AntecedentehigieneoralsController : Controller
    {
        private readonly ClinicadentalContext _context;

        public AntecedentehigieneoralsController(ClinicadentalContext context)
        {
            _context = context;
        }

        // GET: Antecedentehigieneorals
        public async Task<IActionResult> Index()
        {
            return View(await _context.Antecedentehigieneorals.AsNoTracking().ToListAsync());
        }

        // GET: Antecedentehigieneorals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)            
                return NotFound();            

            var antecedentehigieneoral = await _context.Antecedentehigieneorals.AsNoTracking()
                .FirstOrDefaultAsync(m => m.IdAntecedenteHigieneOral == id);

            if (antecedentehigieneoral == null)            
                return NotFound();            

            return View(antecedentehigieneoral);
        }

        // GET: Antecedentehigieneorals/Create
        public IActionResult Create()
        {
            ViewData["CepilloDental"] = new SelectList(Enum.GetValues(typeof(CepilloDental)));
            ViewData["HiloDental"] = new SelectList(Enum.GetValues(typeof(HiloDental)));
            ViewData["EnjuagueBucal"] = new SelectList(Enum.GetValues(typeof(EnjuagueBucal)));
            ViewData["SangradoEncias"] = new SelectList(Enum.GetValues(typeof(SangradoEncias)));
            ViewData["HigieneBucal"] = new SelectList(Enum.GetValues(typeof(HigieneBucal)));
            return View();
        }

        // POST: Antecedentehigieneorals/Create        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAntecedenteHigieneOral,IdHigieneBucal,FrecuenciaCepilladoDental,CepilloDental,EnjuagueBucal,IdHiloDental,SangradoEncias")] Antecedentehigieneoral antecedentehigieneoral)
        {
            if (ModelState.IsValid)
            {
                _context.Add(antecedentehigieneoral);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(antecedentehigieneoral);
        }

        // GET: Antecedentehigieneorals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)            
                return NotFound();            

            var antecedentehigieneoral = await _context.Antecedentehigieneorals.FindAsync(id);

            if (antecedentehigieneoral == null)            
                return NotFound();
            
            return View(antecedentehigieneoral);
        }

        // POST: Antecedentehigieneorals/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAntecedenteHigieneOral,IdHigieneBucal,FrecuenciaCepilladoDental,CepilloDental,EnjuagueBucal,IdHiloDental,SangradoEncias")] Antecedentehigieneoral antecedentehigieneoral)
        {
            if (id != antecedentehigieneoral.IdAntecedenteHigieneOral)            
                return NotFound();            

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(antecedentehigieneoral);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AntecedentehigieneoralExists(antecedentehigieneoral.IdAntecedenteHigieneOral))
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
            return View(antecedentehigieneoral);
        }

        // GET: Antecedentehigieneorals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)            
                return NotFound();            

            var antecedentehigieneoral = await _context.Antecedentehigieneorals
                .FirstOrDefaultAsync(m => m.IdAntecedenteHigieneOral == id);

            if (antecedentehigieneoral == null)            
                return NotFound();            

            return View(antecedentehigieneoral);
        }

        // POST: Antecedentehigieneorals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var antecedentehigieneoral = await _context.Antecedentehigieneorals.FindAsync(id);

            if (antecedentehigieneoral != null)            
                _context.Antecedentehigieneorals.Remove(antecedentehigieneoral);            

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AntecedentehigieneoralExists(int id)
        {
            return _context.Antecedentehigieneorals.Any(e => e.IdAntecedenteHigieneOral == id);
        }
    }
}
