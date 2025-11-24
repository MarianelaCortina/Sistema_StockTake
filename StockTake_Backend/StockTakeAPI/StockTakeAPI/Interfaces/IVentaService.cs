using StockTakeAPI.DTOs;
using StockTakeAPI.Helpers;

namespace StockTakeAPI.Interfaces
{
    public interface IVentaService
    {
        Task<Response<VentaDto>> Registrar(VentaDto model);
        Task<Response<List<VentaDto>>> Historial(string buscarPor, string numeroVenta, string fechaInicio, string fechaFin);
        Task<Response<List<ReporteDTO>>> Reporte(string fechaInicio, string fechaFin);
    }
}
