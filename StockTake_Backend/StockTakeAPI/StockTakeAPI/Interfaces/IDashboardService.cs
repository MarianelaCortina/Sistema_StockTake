using StockTakeAPI.DTOs;

namespace StockTakeAPI.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardDto> Resumen();
    }
}
