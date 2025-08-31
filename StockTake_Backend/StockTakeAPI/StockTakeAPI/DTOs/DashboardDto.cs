namespace StockTakeAPI.DTOs
{
    public class DashboardDto
    {
        public int TotalVentas { get; set; }

        public string? TotalIngresos { get; set; }

        public int TotalProductos { get; set; }

        public List<VentasSemanaDto> VentasUltimaSemana { get; set; }
    }
}
