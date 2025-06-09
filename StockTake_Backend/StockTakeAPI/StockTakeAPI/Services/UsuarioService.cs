using Microsoft.EntityFrameworkCore;
using StockTakeAPI.Data;
using StockTakeAPI.Interfaces;
using StockTakeAPI.Models;

namespace StockTakeAPI.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly AppDbContext _context;

        public UsuarioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario?> ValidarCredenciales(string correo, string clave)
        {
            return await _context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u =>
                    u.Email == correo &&
                    u.ClaveHash == clave &&
                    u.EsActivo);
        }
    }
}
