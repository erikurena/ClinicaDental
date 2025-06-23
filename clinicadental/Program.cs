using clinicadental;
using clinicadental.dbcontext;
using clinicadental.Dtos;
using clinicadental.Interfaces;
using clinicadental.Models;
using clinicadental.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Session;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);



string Conexion = builder.Configuration.GetConnectionString("DbConexion") ?? throw new Exception("No Existe Conexion a la Base de Datos");

builder.Services.AddDbContext<ClinicadentalContext>(conexion => conexion.UseLazyLoadingProxies().UseMySql(Conexion, ServerVersion.AutoDetect(Conexion)));

builder.Services.AddScoped<IPaciente, PacienteService>();
builder.Services.AddScoped<IClinica, ClinicaService>();

// Configurar sesiones
builder.Services.AddDistributedMemoryCache(); 
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tiempo de vida de la sesión
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SameSite = SameSiteMode.Strict;
    options.Cookie.Name = ".AspNetCore.Session"; // Nombre de la cookie de sesión
});

// Configurar serialización JSON
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
        options.JsonSerializerOptions.Converters.Add(new TimeOnlyJsonConverter());
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

// Configurar Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // Desactiva confirmación de cuenta
})
.AddEntityFrameworkStores<ClinicadentalContext>()
.AddDefaultTokenProviders();

// Configurar cookies de autenticación para usar sesiones
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Cuentums/IniciarSesion";
    options.LogoutPath = "/Cuentums/IniciarSesion";
    options.AccessDeniedPath = "/Cuentums/IniciarSesion";
    options.ExpireTimeSpan = TimeSpan.FromHours(1);
    options.SlidingExpiration = true; 
    options.SessionStore = builder.Services.BuildServiceProvider().GetService<MemoryCacheTicketStore>();
    options.Cookie.HttpOnly = true;
    options.Cookie.SameSite = SameSiteMode.Strict;
});

// Configurar servicios de correo electrónico
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

builder.Services.AddTransient<IEmailSender, EmailSender>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Inicializar roles
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await InicializarRol.InicializarRolAsync(roleManager);
}

// Configurar el pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseSession(); // Habilitar sesiones
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Cuentums}/{action=IniciarSesion}/{id?}");

app.Run();