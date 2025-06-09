namespace StockTakeAPI.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public string? NombreMenu { get; set; }
        public string? Icono { get; set; }
        public string? Url { get; set; }

        public required ICollection<MenuRol> MenuRoles { get; set; }
    }
}
