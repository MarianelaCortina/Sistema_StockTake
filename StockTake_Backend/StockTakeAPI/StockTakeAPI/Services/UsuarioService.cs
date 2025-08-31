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

        //public async Task<Usuario?> ValidarCredenciales(string correo, string clave)
        //{
        //    return await _context.Usuarios
        //        .Include(u => u.Rol)
        //        .FirstOrDefaultAsync(u =>
        //            u.Email == correo &&
        //            u.ClaveHash == clave &&
        //            u.EsActivo);
        //}

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






    }


}
