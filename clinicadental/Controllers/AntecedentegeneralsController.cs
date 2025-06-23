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
    public class AntecedentegeneralsController : Controller
    {
        private readonly ClinicadentalContext _context;

        public AntecedentegeneralsController(ClinicadentalContext context)
        {
            _context = context;
        }

        // GET: Antecedentegenerals
        public async Task<IActionResult> Index()
        {
            return View(await _context.Antecedentegenerals.AsNoTracking().ToListAsync());
        }

        // GET: Antecedentegenerals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)            
                return NotFound();            

            var antecedentegeneral = await _context.Antecedentegenerals.AsNoTracking()
                .FirstOrDefaultAsync(m => m.IdAntecedenteGeneral == id);

            if (antecedentegeneral == null)            
                return NotFound();
            

            return View(antecedentegeneral);
        }

        // GET: Antecedentegenerals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Antecedentegenerals/Create       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAntecedenteGeneral,Alergia,TipoAlergia,HemorragiaDental,EspecificacionHemorragia,Embarazo,SemanaEmbarazo")] Antecedentegeneral antecedentegeneral)
        {
            if (ModelState.IsValid)
            {
                _context.Add(antecedentegeneral);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(antecedentegeneral);
        }

        // GET: Antecedentegenerals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)            
                return NotFound();            

            var antecedentegeneral = await _context.Antecedentegenerals.FindAsync(id);

            if (antecedentegeneral == null)            
                return NotFound();
            
            return View(antecedentegeneral);
        }

        // POST: Antecedentegenerals/Edit/5        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAntecedenteGeneral,Alergia,TipoAlergia,HemorragiaDental,EspecificacionHemorragia,Embarazo,SemanaEmbarazo")] Antecedentegeneral antecedentegeneral)
        {
            if (id != antecedentegeneral.IdAntecedenteGeneral)            
                return NotFound();            

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(antecedentegeneral);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AntecedentegeneralExists(antecedentegeneral.IdAntecedenteGeneral))
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
            return View(antecedentegeneral);
        }

        // GET: Antecedentegenerals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)            
                return NotFound();
            
            var antecedentegeneral = await _context.Antecedentegenerals
                .FirstOrDefaultAsync(m => m.IdAntecedenteGeneral == id);

            if (antecedentegeneral == null)            
                return NotFound();            

            return View(antecedentegeneral);
        }

        // POST: Antecedentegenerals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var antecedentegeneral = await _context.Antecedentegenerals.FindAsync(id);

            if (antecedentegeneral != null)            
                _context.Antecedentegenerals.Remove(antecedentegeneral);            

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AntecedentegeneralExists(int id)
        {
            return _context.Antecedentegenerals.Any(e => e.IdAntecedenteGeneral == id);
        }
    }
}
