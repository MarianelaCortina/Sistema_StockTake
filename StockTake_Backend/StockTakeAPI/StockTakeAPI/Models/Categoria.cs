namespace StockTakeAPI.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }

        public bool EsActivo { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public ICollection<Producto>? Productos { get; set; }
    }
}
