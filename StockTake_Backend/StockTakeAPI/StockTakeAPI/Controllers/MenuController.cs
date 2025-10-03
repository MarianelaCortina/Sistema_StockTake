
using Microsoft.AspNetCore.Mvc;
using StockTakeAPI.DTOs;
using StockTakeAPI.Helpers;
using StockTakeAPI.Interfaces;

namespace StockTakeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        /// <summary>
        /// Obtiene los menús disponibles para un usuario según su rol.
        /// </summary>
        /// <param name="idUsuario">Id del usuario</param>
        /// <returns>Lista de menús en un objeto Response</returns>
        [HttpGet("{idUsuario}")]
        public async Task<ActionResult<Response<List<MenuDTO>>>> GetMenus(int idUsuario)
        {
            var result = await _menuService.Lista(idUsuario);

            if (!result.Status)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
