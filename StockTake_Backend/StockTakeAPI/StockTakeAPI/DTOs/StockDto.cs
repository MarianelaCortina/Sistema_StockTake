namespace StockTakeAPI.DTOs
{
    public class StockDto
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int StockActual { get; set; }
    }

    public class MovimientoStockDto
    {
        public int IdMovimiento { get; set; }
        public DateTime Fecha { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public int StockFinal { get; set; }
        public string Producto { get; set; } = string.Empty;
    }
}
