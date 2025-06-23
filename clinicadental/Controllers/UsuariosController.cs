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
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace clinicadental.Controllers
{   
    public class UsuariosController : Controller
    {
        private readonly ClinicadentalContext _context;

        public UsuariosController(ClinicadentalContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Administrador")]
        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            var listaUsuarios = await _context.Usuarios.AsNoTracking().ToListAsync();
            return View(listaUsuarios);
        }
        [Authorize(Roles = "Administrador,Medico")]
        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details()
        {        
            var usuarioIdClaim = User.FindFirst("CodigoUsuario")?.Value;

            if (usuarioIdClaim == null)
                return NotFound();

            var usuario = await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(m => m.CodigoUsuario == usuarioIdClaim);

            if (usuario == null)
                return NotFound();

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            ViewData["Sexo"] = new SelectList(Enum.GetValues(typeof(Sexo)));
            return View();
        }

        // POST: Usuarios/Create        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdUsuario,PrimerNombre,SegundoNombre,ApellidoPaterno,ApellidoMaterno,Celular,FechaNacimiento,Especialidad,IdRol,Sexo,IdCuenta,CodigoUsuario")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.FechaCreacionUsuario = DateTime.UtcNow;
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(usuario);
        }

        // GET: Usuarios/Edit/5      
        [Authorize(Roles = "Administrador,Medico")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
                return NotFound();

            ViewData["Sexo"] = new SelectList(Enum.GetValues(typeof(Sexo)), usuario.Sexo);
            return PartialView("~/Views/Usuarios/Edit.cshtml", usuario);
        }

        // POST: Usuarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador,Medico")]
        public async Task<IActionResult> Edit(int idUsuario, [Bind("IdUsuario,PrimerNombre,SegundoNombre,ApellidoPaterno,ApellidoMaterno,Celular,FechaNacimiento,Especialidad,Sexo")] Usuario usuario)
        {
            if (idUsuario != usuario.IdUsuario)            
                return Json(new { success = false, message = "ID de usuario no coincide." });            

            if (!ModelState.IsValid)
                return PartialView("~/Views/Usuarios/Edit.cshtml", usuario); 
          
            try
            {
                var usuarioExistente = await _context.Usuarios.FindAsync(usuario.IdUsuario);

                if (usuarioExistente == null)
                       return Json(new { success = false, message = "Usuario no encontrado." });                

                usuarioExistente.FechaModificacionUsuario = DateTime.UtcNow;
                usuarioExistente.PrimerNombre = usuario.PrimerNombre;
                usuarioExistente.SegundoNombre = usuario.SegundoNombre;
                usuarioExistente.ApellidoPaterno = usuario.ApellidoPaterno;
                usuarioExistente.ApellidoMaterno = usuario.ApellidoMaterno;
                usuarioExistente.Celular = usuario.Celular;
                usuarioExistente.FechaNacimiento = usuario.FechaNacimiento;
                usuarioExistente.Especialidad = usuario.Especialidad;
                usuarioExistente.Sexo = usuario.Sexo;

                await _context.SaveChangesAsync();             
                return Json(new { success = true, message = "Usuario actualizado con éxito." });
            }
            catch (DbUpdateConcurrencyException)
            {              
                return Json(new { success = false, message = "Error de concurrencia al actualizar el usuario." });
            }
            catch (Exception ex)
            {              
                return Json(new { success = false, message = $"Error al actualizar el usuario: {ex.Message}" });
            }
        }

        [Authorize(Roles = "Administrador")]
        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(m => m.IdUsuario == id);

            if (usuario == null)
                return NotFound();

            return View(usuario);
        }
        [Authorize(Roles = "Administrador")]
        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.IdUsuario == id);
        }
    }
}
