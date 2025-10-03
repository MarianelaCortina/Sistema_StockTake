namespace StockTakeAPI.Models
{
    public class MovimientoStock
    {
        public int Id { get; set; }
        public int IdProducto { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string Tipo { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public int StockFinal { get; set; }

        public virtual Producto Producto { get; set; }
    }

}
