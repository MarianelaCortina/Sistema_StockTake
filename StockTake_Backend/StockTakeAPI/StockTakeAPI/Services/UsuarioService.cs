using Microsoft.EntityFrameworkCore;
using StockTakeAPI.Data;
using StockTakeAPI.Interfaces;
using StockTakeAPI.Models;
using StockTakeAPI.Helpers;
using Microsoft.AspNetCore.Identity;

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
            var usuario = await _context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.Email == correo && u.EsActivo);

            if (usuario == null || string.IsNullOrWhiteSpace(usuario.ClaveHash))
                return null;

            var hasher = new PasswordHasher<Usuario>();
            var resultado = hasher.VerifyHashedPassword(usuario, usuario.ClaveHash, clave);

            return resultado == PasswordVerificationResult.Success ? usuario : null;
        }


        public async Task<Usuario> CrearUsuario(Usuario nuevoUsuario)
        {
            if (string.IsNullOrWhiteSpace(nuevoUsuario.ClaveHash))
                throw new ArgumentException("La clave no puede estar vacía.");

            nuevoUsuario.ClaveHash = HashHelper.HashearClave(nuevoUsuario.ClaveHash);

            _context.Usuarios.Add(nuevoUsuario);
            await _context.SaveChangesAsync();
            return nuevoUsuario;
        }

        public async Task<List<Usuario>> ListarUsuarios()
        {
            return await _context.Usuarios
                .Include(u => u.Rol)
                .ToListAsync();
        }

        public async Task<Usuario?> ObtenerPorId(int id)
        {
            return await _context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Usuario?> ActualizarUsuario(Usuario usuarioActualizado)
        {
            var usuario = await _context.Usuarios.FindAsync(usuarioActualizado.Id);

            if (usuario == null) return null;

            usuario.NombreCompleto = usuarioActualizado.NombreCompleto;
            usuario.Email = usuarioActualizado.Email;
            usuario.RolId = usuarioActualizado.RolId;

            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<bool> EliminarUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null) return false;

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return true;
        }

    }


}
