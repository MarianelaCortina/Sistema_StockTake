using StockTakeAPI.DTOs;
using StockTakeAPI.Helpers;

namespace StockTakeAPI.Interfaces
{
    public interface IStockService
    {
        Task<Response<List<StockDto>>> ObtenerStock();
        Task<Response<List<MovimientoStockDto>>> ObtenerMovimientos();
        Task<Response<List<object>>> ObtenerAlertas();

    }
}
