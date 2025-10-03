using Microsoft.EntityFrameworkCore;
using StockTakeAPI.Data;
using StockTakeAPI.DTOs;
using StockTakeAPI.Helpers;
using StockTakeAPI.Interfaces;
using StockTakeAPI.Models;
using System.Globalization;

namespace StockTakeAPI.Services
{
    public class VentaService : IVentaService
    {
        private readonly AppDbContext _context;

        public VentaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Response<VentaDto>> Registrar(VentaDto model)
        {
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    decimal totalDecimal = 0;
                    decimal.TryParse(model.TotalTexto, NumberStyles.Any, CultureInfo.InvariantCulture, out totalDecimal);

                    var venta = new Venta
                    {
                        NumeroDocumento = model.NumeroDocumento,
                        TipoPago = model.TipoPago,
                        Total = totalDecimal,
                        FechaRegistro = DateTime.Now,
                        DetalleVenta = new List<DetalleVenta>()
                    };

                    foreach (var d in model.DetalleVenta)
                    {
                        // 🔎 Buscar producto
                        var producto = await _context.Productos.FindAsync(d.IdProducto);
                        if (producto == null)
                        {
                            await transaction.RollbackAsync();
                            return new Response<VentaDto>(false, null, $"El producto con Id {d.IdProducto} no existe");
                        }

                        // 🔎 Validar stock
                        if (producto.Stock < d.Cantidad)
                        {
                            await transaction.RollbackAsync();
                            return new Response<VentaDto>(false, null,
                                $"Stock insuficiente de {producto.Nombre}. Stock actual: {producto.Stock}, solicitado: {d.Cantidad}");
                        }

                        // 🔹 Descontar stock
                        producto.Stock = (int)(producto.Stock - d.Cantidad);

                        _context.MovimientosStock.Add(new MovimientoStock
                        {
                            IdProducto = producto.Id,
                            Tipo = "Venta",
                            Cantidad = (int)d.Cantidad,
                            StockFinal = producto.Stock,
                            Fecha = DateTime.Now
                        });

                    // 🔹 Crear detalle
                    decimal precio = 0, total = 0;
                        decimal.TryParse(d.PrecioTexto, NumberStyles.Any, CultureInfo.InvariantCulture, out precio);
                        decimal.TryParse(d.TotalTexto, NumberStyles.Any, CultureInfo.InvariantCulture, out total);

                        venta.DetalleVenta.Add(new DetalleVenta
                        {
                            IdProducto = d.IdProducto,
                            Cantidad = d.Cantidad,
                            Precio = precio,
                            Total = total
                        });
                    }

                    _context.Venta.Add(venta);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    // 🔹 DTO de respuesta
                    var ventaDto = new VentaDto
                    {
                        IdVenta = venta.IdVenta,
                        NumeroDocumento = venta.NumeroDocumento,
                        TipoPago = venta.TipoPago,
                        TotalTexto = (venta.Total ?? 0).ToString("0.00"),
                        FechaRegistro = venta.FechaRegistro?.ToString("dd/MM/yyyy"),
                        DetalleVenta = venta.DetalleVenta.Select(d => new DetalleVentaDto
                        {
                            IdProducto = d.IdProducto,
                            DescripcionProducto = d.IdProductoNavigation?.Nombre,
                            Cantidad = d.Cantidad,
                            PrecioTexto = (d.Precio ?? 0).ToString("0.00"),
                            TotalTexto = (d.Total ?? 0).ToString("0.00")
                        }).ToList()
                    };

                    return new Response<VentaDto>(true, ventaDto, "Venta registrada correctamente y stock actualizado");
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return new Response<VentaDto>(false, null, $"Error al registrar venta: {ex.Message}");
                }
         }

        

        public async Task<Response<List<VentaDto>>> Historial(string buscarPor, string numeroVenta, string fechaInicio, string fechaFin)
        {
            try
            {
                IQueryable<Venta> query = _context.Venta
                    .Include(v => v.DetalleVenta)
                    .ThenInclude(dv => dv.IdProductoNavigation);

                List<Venta> ventas;

                if (buscarPor == "fecha")
                {
                    DateTime fech_Inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-AR"));
                    DateTime fech_Fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-AR"));

                    ventas = await query
                        .Where(v => v.FechaRegistro.Value.Date >= fech_Inicio.Date &&
                                    v.FechaRegistro.Value.Date <= fech_Fin.Date)
                        .ToListAsync();
                }
                else
                {
                    ventas = await query
                        .Where(v => v.NumeroDocumento == numeroVenta)
                        .ToListAsync();
                }

                var listaDto = ventas.Select(v => new VentaDto
                {
                    IdVenta = v.IdVenta,
                    NumeroDocumento = v.NumeroDocumento,
                    TipoPago = v.TipoPago,
                    TotalTexto = (v.Total ?? 0).ToString("0.00"),
                    FechaRegistro = v.FechaRegistro?.ToString("dd/MM/yyyy"),
                    DetalleVenta = v.DetalleVenta.Select(d => new DetalleVentaDto
                    {
                        IdProducto = d.IdProducto,
                        DescripcionProducto = d.IdProductoNavigation?.Nombre,
                        Cantidad = d.Cantidad,
                        PrecioTexto = (d.Precio ?? 0).ToString("0.00"),
                        TotalTexto = (d.Total ?? 0).ToString("0.00")
                    }).ToList()
                }).ToList();

                return new Response<List<VentaDto>>(true, listaDto);
            }
            catch (Exception ex)
            {
                return new Response<List<VentaDto>>(false, null, $"Error al obtener historial: {ex.Message}");
            }
        }

        //public async Task<Response<List<ReporteDto>>> Reporte(string fechaInicio, string fechaFin)
        //{
        //    try
        //    {
        //        DateTime fech_Inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-AR"));
        //        DateTime fech_Fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-AR"));

        //        var detalleVentas = await _context.DetalleVenta
        //            .Include(dv => dv.IdProductoNavigation)
        //            .Include(dv => dv.IdVentaNavigation)
        //            .Where(dv => dv.IdVentaNavigation.FechaRegistro.Value.Date >= fech_Inicio.Date &&
        //                         dv.IdVentaNavigation.FechaRegistro.Value.Date <= fech_Fin.Date)
        //            .ToListAsync();

        //        var listaDto = detalleVentas.Select(d => new ReporteDto
        //        {
        //            FechaRegistro = d.IdVentaNavigation.FechaRegistro?.ToString("dd/MM/yyyy"),
        //            NumeroDocumento = d.IdVentaNavigation.NumeroDocumento,
        //            TipoPago = d.IdVentaNavigation.TipoPago,
        //            TotalVenta = d.IdVentaNavigation.Total.ToString("0.00"),
        //            Producto = d.IdProductoNavigation?.Nombre,
        //            Cantidad = d.Cantidad,
        //            Precio = d.Precio,
        //            Total = d.Total
        //        }).ToList();

        //        return new Response<List<ReporteDto>>(true, listaDto);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Response<List<ReporteDto>>(false, null, $"Error al generar reporte: {ex.Message}");
        //    }
        //}
    }
}
