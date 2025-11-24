using Microsoft.EntityFrameworkCore;
using StockTakeAPI.Data;
using StockTakeAPI.DTOs;
using StockTakeAPI.Helpers;
using StockTakeAPI.Interfaces;

namespace StockTakeAPI.Services
{
    public class StockService : IStockService
    {
        private readonly AppDbContext _context;

        public StockService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtener stock actual de todos los productos
        /// </summary>
        public async Task<Response<List<StockDto>>> ObtenerStock()
        {
            try
            {
                var productos = await _context.Productos
                    .Select(p => new StockDto
                    {
                        IdProducto = p.Id,
                        Nombre = p.Nombre,
                        StockActual = p.Stock
                    })
                    .ToListAsync();

                return new Response<List<StockDto>>(true, productos);
            }
            catch (Exception ex)
            {
                return new Response<List<StockDto>>(false, null, $"Error al obtener stock: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtener los últimos movimientos de stock
        /// </summary>
        public async Task<Response<List<MovimientoStockDto>>> ObtenerMovimientos()
        {
            try
            {
                var movimientos = await _context.MovimientosStock
                    .Include(m => m.Producto)
                    .OrderByDescending(m => m.Fecha)
                    .Take(50)
                    .ToListAsync();

                var listaDto = movimientos.Select(m => new MovimientoStockDto
                {
                    IdMovimiento = m.Id,
                    Fecha = m.Fecha,
                    Tipo = m.Tipo,
                    Cantidad = m.Cantidad,
                    StockFinal = m.StockFinal,
                    Producto = m.Producto.Nombre
                }).ToList();

                return new Response<List<MovimientoStockDto>>(true, listaDto);
            }
            catch (Exception ex)
            {
                return new Response<List<MovimientoStockDto>>(false, null, $"Error al obtener movimientos: {ex.Message}");
            }
        }

        public async Task<Response<List<object>>> ObtenerAlertas()
        {
            try
            {
                var productos = await _context.Productos
                    .Include(p => p.MovimientosStock)
                    .ToListAsync();

                var alertas = new List<object>();

                foreach (var producto in productos)
                {
                    var ventas = producto.MovimientosStock
                        .Where(m => m.Tipo == "Venta")
                        .OrderByDescending(m => m.Fecha)
                        .Take(30)
                        .ToList();

                    if (!ventas.Any()) continue;

                    var rangoDias = (ventas.First().Fecha - ventas.Last().Fecha).TotalDays;
                    rangoDias = rangoDias < 1 ? 7 : rangoDias;

                    var totalVendidas = ventas.Sum(v => v.Cantidad);
                    var promedioDiario = totalVendidas / rangoDias;

                    if (promedioDiario == 0) continue;

                    var diasRestantes = producto.Stock / promedioDiario;

                    string nivel;
                    string color;
                    string icono;
                    string estado;

                    if (diasRestantes <= 2)
                    {
                        nivel = "critico";
                        color = "#ff2d2d";
                        icono = "priority_high";
                        estado = "Agotamiento inminente";
                    }
                    else if (diasRestantes <= 5)
                    {
                        nivel = "medio";
                        color = "#ff9800";
                        icono = "warning";
                        estado = "Bajo stock";
                    }
                    else if (diasRestantes <= 10)
                    {
                        nivel = "bajo";
                        color = "#ffc107";
                        icono = "info";
                        estado = "Reponer pronto";
                    }
                    else
                    {
                        // stock saludable → No alertamos
                        continue; 
                    }

                    // porcentaje de urgencia (0–100)
                    var porcentajeUrgencia = Math.Min(100, (10 - diasRestantes) * 10);

                    alertas.Add(new
                    {
                        producto.Id,
                        producto.Nombre,
                        producto.Stock,
                        PromedioVentasDiario = Math.Round(promedioDiario, 2),
                        DiasEstimadosRestantes = Math.Round(diasRestantes, 1),
                        NivelRiesgo = nivel,
                        Color = color,
                        Icono = icono,
                        Estado = estado,
                        PorcentajeUrgencia = Math.Round(porcentajeUrgencia),
                        StockMinimoRecomendado = Math.Ceiling(promedioDiario * 14)
                    });
                }

                return new Response<List<object>>(true, alertas);
            }
            catch (Exception ex)
            {
                return new Response<List<object>>(false, null, $"Error al generar alertas: {ex.Message}");
            }
        }




    }

}
