using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using clinicadental.Models;
using Microsoft.AspNetCore.Authorization;
using static System.Runtime.InteropServices.JavaScript.JSType;
using clinicadental.Interfaces;

namespace clinicadental.Controllers
{
    [Authorize]
    public class ClinicasController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IClinica _clinicaService;

        public ClinicasController(IWebHostEnvironment webHostEnvironment, IClinica clinicaContext)
        {
            _webHostEnvironment = webHostEnvironment;
            _clinicaService = clinicaContext;
        }
    
        // GET: Clinicas
        public async Task<IActionResult> Index()
        {
            var userId = Convert.ToInt32(User.FindFirst("IdNumUsuario")?.Value);
         
            var clinicas = await _clinicaService.GetClinica(userId);
            return View(clinicas);
        }

        // GET: Clinicas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var clinica = await _clinicaService.DetailsClinica(id);

            if (clinica == null)
                return NotFound();

            return View(clinica);
        }

        // GET: Clinicas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clinicas/Create        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdClinica,Nombre,Nit,Ujsedes,Pmc,Direccion,Celular,FotoFile,Ciudad,Pais")] Clinica clinica)
        {
            if (!ModelState.IsValid) 
                return PartialView("Create", clinica);

           var userId = Convert.ToInt32(User.FindFirst("IdNumUsuario")?.Value);
           
            if (clinica.FotoFile != null)            
                await SubirFoto(clinica);            

            var result = await _clinicaService.CreateClinica(clinica, userId);

            return result ? Json(new { success = true }) : Json(new {success = false}) ;
        }

        private async Task SubirFoto(Clinica clinica)
        {
            //formar nombre del archivo
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string nombreFoto = clinica.FotoFile.FileName;

            clinica.FotoClinica = nombreFoto;
            //copiar la foto en el servidor
            string path = Path.Combine(wwwRootPath,"Photos","FotoClinicaPortada", nombreFoto);

            using (var fileStream = new FileStream(path,FileMode.Create))
            {
                await clinica.FotoFile.CopyToAsync(fileStream);
            }
        }

        // GET: Clinicas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var clinica = await _clinicaService.DetailsClinica(id);

            if (clinica == null)            
                return NotFound();
            
            return View(clinica);
        }

        // POST: Clinicas/Edit/5      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("IdClinica,Nombre,Nit,Ujsedes,Pmc,Direccion,Celular,FotoFile,Ciudad,Pais")] Clinica clinica)
        {          
            if (ModelState.IsValid)
            {
                var userId = clinica.IdUsuario = Convert.ToInt32(User.FindFirst("IdNumUsuario")?.Value);
                   
                var resultClinica = await _clinicaService.EditClinica(clinica, userId);

                return resultClinica ?  Json(new { success = true }) : Json( new {success = false});
              }
               return PartialView("Edit", clinica);
        }

        // GET: Clinicas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var clinica = await _clinicaService.DetailsClinica(id);

            if (clinica == null)
                return NotFound();

            return View(clinica);
        }

        // POST: Clinicas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
           var result = await _clinicaService.DeleteClinica(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
