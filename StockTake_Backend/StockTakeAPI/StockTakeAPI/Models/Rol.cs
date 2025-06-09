namespace StockTakeAPI.Models
{
    public class Rol
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public required ICollection<Usuario> Usuarios { get; set; }
        public required ICollection<MenuRol> MenuRoles { get; set; }
    }
}
