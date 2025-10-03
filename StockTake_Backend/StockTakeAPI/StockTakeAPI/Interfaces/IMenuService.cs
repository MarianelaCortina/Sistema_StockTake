using StockTakeAPI.DTOs;
using StockTakeAPI.Helpers;

namespace StockTakeAPI.Interfaces
{
    public interface IMenuService
    {
        // <summary>
        /// Obtiene la lista de menús habilitados para un usuario según su rol.
        /// </summary>
        /// <param name="idUsuario">Id del usuario</param>
        /// <returns>Lista de menús en un objeto Response</returns>
        Task<Response<List<MenuDTO>>> Lista(int idUsuario);
    }
}

