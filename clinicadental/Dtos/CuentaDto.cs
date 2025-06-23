using Microsoft.EntityFrameworkCore;

namespace clinicadental.Dtos
{
    public class CuentaDto
    {
       
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
