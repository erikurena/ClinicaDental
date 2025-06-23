using Microsoft.AspNetCore.Identity;

namespace clinicadental.Models
{
    public class InicializarRol
    {
        public static async Task InicializarRolAsync(RoleManager<IdentityRole> roleManager)
        {
            string[] roles = { "Administrador", "Medico", "Secretario" };
            foreach (var i in roles)
            {
                if (!await roleManager.RoleExistsAsync(i))
                {
                    await roleManager.CreateAsync(new IdentityRole(i));
                }
            }
        }
    }
}
