using Microsoft.AspNetCore.Mvc;
using StockTakeAPI.DTOs;
using StockTakeAPI.Interfaces;
using StockTakeAPI.Helpers;
using StockTakeAPI.Models;

namespace StockTakeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet("Lista")]
        public async Task<IActionResult> Lista()
        {
            var usuarios = await _usuarioService.ListarUsuarios();

            var dto = usuarios.Select(u => new UsuarioDTO
            {
                Id = u.Id,
                NombreCompleto = u.NombreCompleto,
                Email = u.Email,
                EsActivo = u.EsActivo,
                EmailVerificado = u.EmailVerificado,
                RolId = u.RolId,
                RolNombre = u.Rol!.Nombre
            });

            return Ok(new Response<object>(true, dto));
        }

        [HttpPost("Crear")]
        public async Task<IActionResult> Crear([FromBody] CreateUsuarioDTO dto)
        {
            var nuevoUsuario = new Usuario
            {
                NombreCompleto = dto.NombreCompleto,
                Email = dto.Email,
                ClaveHash = dto.Clave,
                RolId = dto.RolId,
                EsActivo = true
            };

            var creado = await _usuarioService.CrearUsuario(nuevoUsuario);

            return Ok(new { status = true, value = creado.Id });
        }

        [HttpPut("Actualizar")]
        public async Task<IActionResult> Actualizar([FromBody] UsuarioDTO dto)
        {
            var usuario = await _usuarioService.ObtenerPorId(dto.Id);

            if (usuario == null)
                return NotFound(new Response<string>(false, null, "Usuario no encontrado"));

            usuario.NombreCompleto = dto.NombreCompleto;
            usuario.Email = dto.Email;
            usuario.RolId = dto.RolId;

            await _usuarioService.ActualizarUsuario(usuario);

            return Ok(new Response<UsuarioDTO>(true, dto, "Usuario actualizado"));
        }

        [HttpDelete("Eliminar/{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var result = await _usuarioService.EliminarUsuario(id);

            if (!result)
                return NotFound(new Response<string>(false, null, "Usuario no encontrado"));

            return Ok(new Response<string>(true, null, "Usuario eliminado"));
        }
    }
}
