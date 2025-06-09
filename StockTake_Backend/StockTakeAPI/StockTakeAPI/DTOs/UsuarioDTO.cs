using StockTakeAPI.Models;

namespace StockTakeAPI.DTOs
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string? NombreCompleto { get; set; }
        public string? Email { get; set; }
        public bool EsActivo { get; set; } = true;
        public bool EmailVerificado { get; set; } 
        public int RolId { get; set; }
        public string? RolNombre { get; set; }

    }
}
