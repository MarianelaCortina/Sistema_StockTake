using StockTakeAPI.DTOs;

namespace StockTakeAPI.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(SesionDTO sesion);
    }
}
