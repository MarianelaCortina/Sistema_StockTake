using StockTakeAPI.DTOs;
using StockTakeAPI.Models;

namespace StockTakeAPI.Interfaces
{
    public interface IUsuarioService
    {
        Task<Usuario?> ValidarCredenciales(string email, string clave);
        Task<Usuario> CrearUsuario(Usuario nuevoUsuario);

        Task<List<Usuario>> ListarUsuarios();
        Task<Usuario?> ObtenerPorId(int id);
        Task<Usuario?> ActualizarUsuario(Usuario usuario);
        Task<bool> EliminarUsuario(int id);
    }
}
