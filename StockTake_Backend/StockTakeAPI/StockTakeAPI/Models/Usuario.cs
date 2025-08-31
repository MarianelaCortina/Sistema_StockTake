namespace StockTakeAPI.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string? NombreCompleto { get; set; }
        public string? Email { get; set; }
        public required string? ClaveHash { get; set; }
        public bool EsActivo { get; set; } = true;
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiry { get; set; }
        public bool EmailVerificado { get; set; } = false;

        public int RolId { get; set; }
        public  Rol? Rol { get; set; }
    }
}
