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
    public class AntecedentepatologicoController : Controller
    {
        private readonly ClinicadentalContext _context;

        public AntecedentepatologicoController(ClinicadentalContext context)
        {
            _context = context;
        }

        // GET: Antecedentepatologico
        public async Task<IActionResult> Index()
        {
            var clinicadentalContext = _context.Antecedentepatologicos.AsNoTracking();
                                                                                                                      
            return View(await clinicadentalContext.ToListAsync());
        }

        // GET: Antecedentepatologico/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)            
                return NotFound();            

            var antecedentepatologico = await _context.Antecedentepatologicos                
                .FirstOrDefaultAsync(m => m.IdAntecedentePatologico == id);

            if (antecedentepatologico == null)            
                return NotFound();            

            return View(antecedentepatologico);
        }

        // GET: Antecedentepatologico/Create
        public IActionResult Create()
        {          
            ViewData["IdEnfermedad"] = new SelectList(_context.Enfermerdads, "IdEnfermerdad", "Enfermedad");
            ViewData["IdAlergia"] = new SelectList(Enum.GetValues(typeof(Alergia)));
            ViewData["IdEmbarazo"] = new SelectList(Enum.GetValues(typeof(Embarazo)));
            ViewData["IdEspecificacionHemorragia"] = new SelectList(Enum.GetValues(typeof(EspecificacionHemorragia)));
            ViewData["IdHemorragia"] = new SelectList(Enum.GetValues(typeof(HemorragiaDen)));
            return View();
        }

        // POST: Antecedentepatologico/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Otro,IdEmbarazo,IdAlergia,AntecedentePatologicoFamiliar,TratamientoMedico,RecibeMedicacion,IdHemorragiaDental")] Antecedentepatologico antecedentepatologico,
            List<int> enfermedadSeleccionada )
        {
            if (!ModelState.IsValid)
            {              
                ViewData["IdEnfermedad"] = new SelectList(_context.Enfermerdads, "IdEnfermerdad", "Enfermedad");
                ViewData["IdAlergia"] = new SelectList(Enum.GetValues(typeof(Alergia)));
                ViewData["IdEmbarazo"] = new SelectList(Enum.GetValues(typeof(Embarazo)));
                ViewData["IdEspecificacionHemorragia"] = new SelectList(Enum.GetValues(typeof(EspecificacionHemorragia)));
                ViewData["IdHemorragia"] = new SelectList(Enum.GetValues(typeof(HemorragiaDen)));
                return View(antecedentepatologico);
            }

            using (var transaccion = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.Add(antecedentepatologico);
                    await _context.SaveChangesAsync();

                    var idAntePatologico = antecedentepatologico.IdAntecedentePatologico;
                    foreach (var enfermedad in enfermedadSeleccionada)
                    {
                        var addEnfermedad = new Antecedenteenfermedad
                        {
                            IdEnfermerdad = enfermedad,
                            IdAntecedentePatologico = idAntePatologico
                        };
                        _context.Antecedenteenfermedads.Add(addEnfermedad);
                    }
                    await _context.SaveChangesAsync();

                    await transaccion.CommitAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    await transaccion.RollbackAsync();
                    throw;
                }
            }
        }

        // GET: Antecedentepatologico/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)            
                return NotFound();
            
            var antecedentepatologico = await _context.Antecedentepatologicos.FindAsync(id);

            if (antecedentepatologico == null)            
                return NotFound();            
           
            return View(antecedentepatologico);
        }

        // POST: Antecedentepatologico/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAntecedentePatologico,Otro,IdEmbarazo,IdAlergia,AntecedentePatologicoFamiliar,TratamientoMedico,RecibeMedicacion,IdHemorragiaDental")] Antecedentepatologico antecedentepatologico)
        {
            if (id != antecedentepatologico.IdAntecedentePatologico)            
                return NotFound();            

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(antecedentepatologico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AntecedentepatologicoExists(antecedentepatologico.IdAntecedentePatologico))
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
            return View(antecedentepatologico);
        }

        // GET: Antecedentepatologico/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)            
                return NotFound();            

            var antecedentepatologico = await _context.Antecedentepatologicos
                .FirstOrDefaultAsync(m => m.IdAntecedentePatologico == id);

            if (antecedentepatologico == null)            
                return NotFound();            

            return View(antecedentepatologico);
        }

        // POST: Antecedentepatologico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var antecedentepatologico = await _context.Antecedentepatologicos.FindAsync(id);

            if (antecedentepatologico != null)            
                _context.Antecedentepatologicos.Remove(antecedentepatologico);
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AntecedentepatologicoExists(int id)
        {
            return _context.Antecedentepatologicos.Any(e => e.IdAntecedentePatologico == id);
        }
    }
}
