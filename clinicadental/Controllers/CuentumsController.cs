using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using clinicadental.Models;
using clinicadental.dbcontext;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using clinicadental.Dtos;
using Microsoft.AspNetCore.Authentication;
using clinicadental.Services;
using System.Net.Mail;
using clinicadental.Interfaces;

namespace clinicadental.Controllers
{
    public class CuentumsController : Controller
    {
        private readonly ClinicadentalContext _context;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailSender _emailSender;


        public CuentumsController(ClinicadentalContext context, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IEmailSender emailSender)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _emailSender = emailSender;
        }


        // GET: Cuentums
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: Cuentums/IniciarSesion
        [HttpGet]
        public IActionResult IniciarSesion()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IniciarSesion(CuentaDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Correo o contraseña incorrectos.");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var idUsuario = await _context.Usuarios.Where(u => u.CodigoUsuario == user.Id)
                                                                                        .Select(x => new
                                                                                        {
                                                                                            idUsuario = x.IdUsuario,
                                                                                            nombre = x.PrimerNombre,
                                                                                            apellidoPaterno = x.ApellidoPaterno,
                                                                                            apellidoMaterno = x.ApellidoMaterno,
                                                                                            codigoUsuario = x.CodigoUsuario
                                                                                        }).FirstOrDefaultAsync();

                if (idUsuario == null)
                {
                    ModelState.AddModelError(string.Empty, "No se encontraron datos del usuario.");
                    return View(model);
                }

                var roles = await _userManager.GetRolesAsync(user);

                var claims = new List<Claim>
                {
                    new Claim("IdUsuario", user.Id),
                    new Claim("IdNumUsuario", idUsuario.idUsuario.ToString()),
                    new Claim("Nombre", idUsuario.nombre ?? string.Empty),
                    new Claim("ApellidoPaterno", idUsuario.apellidoPaterno ?? string.Empty),
                    new Claim("ApellidoMaterno", idUsuario.apellidoMaterno ?? string.Empty),
                    new Claim("CodigoUsuario", idUsuario.codigoUsuario ?? string.Empty)
                };

                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                // Crear una identidad personalizada con los claims
                var claimsIdentity = new ClaimsIdentity(claims, IdentityConstants.ApplicationScheme);

                // Agregar la identidad al principal
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                // Iniciar sesión con los claims personalizados
                await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, claimsPrincipal);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Correo o contraseña incorrectos.");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("IniciarSesion", "Cuentums");
        }

        // GET: Cuentums/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cuentums/Create        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Usuario model)
        {
            if (!ModelState.IsValid)
                return View(model);

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Crear el usuario en ASP.NET Identity
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
                    return View(model);
                }

                await _userManager.AddToRoleAsync(user, "Medico"); // Asignar rol al usuario

                // Crear la entrada en la tabla Usuarios
                var usuario = new Usuario
                {
                    IdUsuario = model.IdUsuario, // Relación con Cuenta
                    Email = model.Email, // Ejemplo: Campo personalizado
                    Password = model.Password,
                    Sexo = (Enums.EnumClinicaDental.Sexo)1,
                    PrimerNombre = model.PrimerNombre,
                    SegundoNombre = model.SegundoNombre,
                    ApellidoPaterno = model.ApellidoPaterno,
                    ApellidoMaterno = model.ApellidoMaterno,
                    Celular = model.Celular,
                    CodigoUsuario = user.Id
                };
                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                // Agregar el IdUsuario como claim
                var claim = new Claim("IdUsuario", user.Id);
                await _userManager.AddClaimAsync(user, claim);

                // Iniciar sesión automáticamente tras el registro (opcional)
                await _signInManager.SignInAsync(user, isPersistent: false);

                await transaction.CommitAsync();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                // Revertir la transacción en caso de error
                await transaction.RollbackAsync();
                ModelState.AddModelError(string.Empty, "Ocurrió un error al registrar el usuario.");
                return View(model);
            }
        }
        public IActionResult RestorePassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> RestorePassword(Usuario usuario)
        {
            var usuarioEncontrado = await _userManager.FindByEmailAsync(usuario.Email);

            if (usuarioEncontrado == null)            
                return RedirectToAction("ForgotPasswordConfirmation");
            
            // Generar token de restablecimiento
            var token = await _userManager.GeneratePasswordResetTokenAsync(usuarioEncontrado);
            var callbackUrl = Url.Action("ResetPassword", "Cuentums", new { userId = usuarioEncontrado.Id, token = token }, protocol: Request.Scheme);

            // Use the _emailSender instance to send the email
            await _emailSender.SendEmailAsync(usuario.Email, "Restablecer contraseña", $"Haz clic <a href='{callbackUrl}'>aquí</a> para restablecer tu contraseña.");

            return RedirectToAction("ForgotPasswordConfirmation");
        }
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ResetPassword(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Index", "Home");
            }
            var model = new ResetPasswordDto { UserId = userId, Token = token };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user == null)
                return RedirectToAction("ResetPasswordConfirmation");

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

            if (result.Succeeded)
                return RedirectToAction("ResetPasswordConfirmation");

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
    }
}
