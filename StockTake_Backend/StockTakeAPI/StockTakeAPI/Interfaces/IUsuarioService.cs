using StockTakeAPI.DTOs;
using StockTakeAPI.Models;

namespace StockTakeAPI.Interfaces
{
    public interface IUsuarioService
    {
        Task<Usuario?> ValidarCredenciales(string correo, string clave);
    }
}
