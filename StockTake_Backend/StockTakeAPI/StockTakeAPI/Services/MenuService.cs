using Microsoft.EntityFrameworkCore;
using StockTakeAPI.Data;
using StockTakeAPI.DTOs;
using StockTakeAPI.Helpers;
using StockTakeAPI.Interfaces;
using StockTakeAPI.Models;

namespace StockTakeAPI.Services
{
    public class MenuService : IMenuService
    {
        private readonly AppDbContext _context;

        public MenuService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Response<List<MenuDTO>>> Lista(int idUsuario)
        {
            try
            {
                // Traemos el usuario con su rol
                var usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.Id == idUsuario);

                if (usuario == null)
                {
                    return new Response<List<MenuDTO>>(
                        false, null, "Usuario no encontrado"
                    );
                }

                // Armamos la query con joins
                var menus = await (from mr in _context.MenuRols
                                   join m in _context.Menus on mr.MenuId equals m.Id
                where mr.RolId == usuario.RolId
                                   select new MenuDTO
                                   {
                                       Id = m.Id,
                                       NombreMenu = m.NombreMenu,
                                       Icono = m.Icono,
                                       Url = m.Url
                                   }).ToListAsync();

                return new Response<List<MenuDTO>>(true, menus, $"{menus.Count} menús encontrados");
            }
            catch (Exception ex)
            {
                return new Response<List<MenuDTO>>(
                    false, null, $"Error al obtener menús: {ex.Message}"
                );
            }
        }
    }
}

