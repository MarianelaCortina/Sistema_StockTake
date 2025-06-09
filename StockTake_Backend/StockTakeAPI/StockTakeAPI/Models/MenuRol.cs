namespace StockTakeAPI.Models
{
    public class MenuRol
    {
        public int Id { get; set; }

        public int MenuId { get; set; }
        public required Menu Menu { get; set; }

        public int RolId { get; set; }
        public required Rol Rol { get; set; }
    }
}
