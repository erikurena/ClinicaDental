using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using clinicadental.Models;
using static clinicadental.Enums.EnumClinicaDental;
using Microsoft.AspNetCore.Authorization;
using clinicadental.Interfaces;

namespace clinicadental.Controllers
{
    [Authorize]
    public class PacientesController : Controller
    {
        private readonly IPaciente _pacienteService;

        public PacientesController(IPaciente pacieteService)
        {
            _pacienteService = pacieteService;
        }

        // GET: Pacientes
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            var userId = Convert.ToInt32(User.FindFirst("IdNumusuario")?.Value);

            var listaPacientes = await _pacienteService.GetPacientes(page, pageSize, userId);

            ViewBag.CurrentPage = listaPacientes.PaginaActual;
            ViewBag.TotalPages = listaPacientes.TotalPaginas;
            ViewBag.PageSize = listaPacientes.TamañoPagina;

            ViewsData(); 
            return View(listaPacientes);
        }

        // GET: Pacientes/Details/5
        public async Task<IActionResult> Details(string? codigoPaciente)
        {
            if (codigoPaciente == null)   return NotFound();

           var PacienteHistorialdto = await _pacienteService.Details(codigoPaciente);

            return View(PacienteHistorialdto);
        }

        public async Task<IActionResult> ListaHistorial(int? idpaciente)
        {
            if (idpaciente == null) 
                return NotFound();

            var listaHistorialPaciente = await _pacienteService.ListaHistorial(idpaciente);

            return View(listaHistorialPaciente);
        }

        // GET: Pacientes/Create
        public IActionResult Create()
        {
            ViewsData();
            return View();
        }

        // POST: Pacientes/Create        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPaciente,Sexo,IdLugarNacimiento,IdEstadoCivil,IdGradoInstruccion,ApellidoMaterno,ApellidoPaterno,Celular,Direccion,FechaNacimientoPaciente,Nombres,Ocupacion")] Paciente paciente)
        {
            if (ModelState.IsValid)
            {
               int UserId = Convert.ToInt32(User.FindFirst("IdNumUsuario")?.Value);

                var returnPaciente = await _pacienteService.CreatePaciente(paciente, UserId);

                if (Request.Headers.XRequestedWith == "XMLHttpRequest")
                {
                    return Json(new
                    {
                        idPaciente = returnPaciente.IdPaciente,
                        apellidoPaterno = returnPaciente.ApellidoPaterno,
                        apellidoMaterno = returnPaciente.ApellidoMaterno,
                        nombres = returnPaciente.Nombres,
                        celular = returnPaciente.Celular,
                        codigoPaciente = returnPaciente.CodigoPaciente
                    });
                }
                return RedirectToAction(nameof(Index));
            }           
            return View(paciente);
        }

        // GET: Pacientes/Edit/5
        [HttpGet]
        [Route("Pacientes/Edit/{codigoPaciente}")]
        public async Task<IActionResult> Edit(string? codigoPaciente)
        {
            if (codigoPaciente == null)            
                return NotFound();            

            var paciente = await _pacienteService.DetailsPaciente(codigoPaciente);

            if (paciente == null)            
                return NotFound();

            ViewData["IdEstadoCivil"] = new SelectList(Enum.GetValues(typeof(EstadoCivil)),paciente.IdEstadoCivil);
            ViewData["IdGradoInstruccion"] = new SelectList(Enum.GetValues(typeof(GradoInstruccion)), paciente.IdGradoInstruccion);
            ViewData["Sexo"] = new SelectList(Enum.GetValues(typeof(Sexo)), paciente.Sexo);
            return PartialView("~/Views/Pacientes/Edit.cshtml", paciente);           
        }

        // POST: Pacientes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
      
        public async Task<IActionResult> Edit(string codigoPaciente, [Bind("IdPaciente,Sexo,IdLugarNacimiento,IdEstadoCivil,IdGradoInstruccion,ApellidoMaterno,ApellidoPaterno,Celular,Direccion,FechaNacimientoPaciente,Nombres,Ocupacion,CodigoPaciente,FechaCreacionPaciente")] Paciente paciente)
        {
            if (codigoPaciente != paciente.CodigoPaciente)
                return NotFound();

            if (ModelState.IsValid)
            {
                var userId = Convert.ToInt32(User.FindFirst("IdNumUsuario")?.Value);
                    
                var result = await _pacienteService.EditPaciente(codigoPaciente, paciente, userId);
                return result ? Json(new { success= true, paciente }) : NotFound();
            }
            ViewData["IdEstadoCivil"] = new SelectList(Enum.GetValues(typeof(EstadoCivil)), paciente.IdEstadoCivil);
            ViewData["IdGradoInstruccion"] = new SelectList(Enum.GetValues(typeof(GradoInstruccion)), paciente.IdGradoInstruccion);
           
            return BadRequest(ModelState);
        }

        // GET: Pacientes/Delete/5
        public async Task<IActionResult> Delete(string? codigoPaciente)
        {
            if (codigoPaciente == null)            
                return NotFound();            

            var paciente = await _pacienteService.DetailsPaciente(codigoPaciente);

            if (paciente == null)
                return NotFound();

            return View(paciente);
        }

        // POST: Pacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string codigoPaciente)
        {
            var userId = Convert.ToInt32(User.FindFirst("IdNumUsuario")?.Value);
            var paciente = await _pacienteService.DeletePaciente(codigoPaciente,userId);

            if (paciente)
            {
                TempData["PacienteEliminado"] = true;
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["PacienteEliminado"] = false;
                return RedirectToAction(nameof(Index));
            }
        }

        public void ViewsData()
        {
            ViewData["IdEstadoCivil"] = new SelectList(Enum.GetValues(typeof(EstadoCivil)));           
            ViewData["IdGradoInstruccion"] = new SelectList(Enum.GetValues(typeof(GradoInstruccion)));          
            ViewData["Sexo"] = new SelectList(Enum.GetValues(typeof(Sexo)));
        }
    }
}
