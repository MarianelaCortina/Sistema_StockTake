namespace StockTakeAPI.DTOs
{
    public class DetalleVentaDto
    {
        public int? IdProducto { get; set; }
        public string? DescripcionProducto { get; set; }
        public int? Cantidad { get; set; }

        public string? PrecioTexto { get; set; }

        public string? TotalTexto
        {
            get; set;
        }
    }

}
