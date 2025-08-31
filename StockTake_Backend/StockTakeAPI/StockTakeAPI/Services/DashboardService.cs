using Microsoft.EntityFrameworkCore;
using StockTakeAPI.Data;
using StockTakeAPI.DTOs;
using StockTakeAPI.Interfaces;
using StockTakeAPI.Models;
using System.Globalization;

namespace StockTakeAPI.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly AppDbContext _context;

        public DashboardService(AppDbContext context)
        {
            _context = context;
        }

        private IQueryable<Venta> RetornarVentas(IQueryable<Venta> tablaVenta, int restarCantidadDias)
        {
            var ultimaFecha = tablaVenta
                .OrderByDescending(v => v.FechaRegistro)
                .Select(v => v.FechaRegistro)
                .FirstOrDefault();

            if (ultimaFecha == null)
                return Enumerable.Empty<Venta>().AsQueryable();

            ultimaFecha = ultimaFecha.Value.AddDays(restarCantidadDias);

            return tablaVenta.Where(v => v.FechaRegistro.Value.Date >= ultimaFecha.Value.Date);
        }

        private async Task<int> TotalVentasUltimaSemana()
        {
            var ventas = await _context.Venta.ToListAsync();
            if (!ventas.Any()) return 0;

            var tablaVenta = RetornarVentas(ventas.AsQueryable(), -7);
            return tablaVenta.Count();
        }

        private async Task<string> TotalIngresosUltimaSemana()
        {
            var ventas = await _context.Venta.ToListAsync();
            if (!ventas.Any()) return "0";

            var tablaVenta = RetornarVentas(ventas.AsQueryable(), -7);
            var resultado = tablaVenta.Sum(v => v.Total ?? 0);

            return Convert.ToString(resultado, new CultureInfo("es-AR"));
        }

        private async Task<int> TotalProductos()
        {
            return await _context.Productos.CountAsync();
        }

        private async Task<Dictionary<string, int>> VentasUltimaSemana()
        {
            var ventas = await _context.Venta.ToListAsync();
            if (!ventas.Any()) return new Dictionary<string, int>();

            var tablaVenta = RetornarVentas(ventas.AsQueryable(), -7);

            return tablaVenta
                .GroupBy(v => v.FechaRegistro.Value.Date)
                .OrderBy(g => g.Key)
                .ToDictionary(
                    g => g.Key.ToString("dd/MM/yyyy"),
                    g => g.Count()
                );
        }

        public async Task<DashboardDto> Resumen()
        {
            var vmDashboard = new DashboardDto();

            vmDashboard.TotalVentas = await TotalVentasUltimaSemana();
            vmDashboard.TotalIngresos = await TotalIngresosUltimaSemana();
            vmDashboard.TotalProductos = await TotalProductos();

            vmDashboard.VentasUltimaSemana = (await VentasUltimaSemana())
                .Select(item => new VentasSemanaDto
                {
                    Fecha = item.Key,
                    Total = item.Value
                })
                .ToList();

            return vmDashboard;
        }
    }
}
