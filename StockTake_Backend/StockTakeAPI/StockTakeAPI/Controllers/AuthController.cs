using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockTakeAPI.DTOs;
using StockTakeAPI.Interfaces;
using StockTakeAPI.Services;
using System.Security.Cryptography;

namespace StockTakeAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IJwtService _jwtService;

        public AuthController(IUsuarioService usuarioService, IJwtService jwtService) 
        {
            _usuarioService = usuarioService;
            _jwtService = jwtService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody]LoginDTO login)
        {
            var usuario = await _usuarioService.ValidarCredenciales(login.Correo!, login.Clave!);
            if (usuario == null)
            {
                return Unauthorized("Credenciales inválidas");
            }

            var sesion = new SesionDTO
            {
                IdUsuario = usuario.Id,
                NombreCompleto = usuario?.NombreCompleto,
                Correo = usuario?.Email,
                RolNombre = usuario?.Rol.Nombre,
            };

            var token = _jwtService.GenerateToken(sesion);

            return Ok(new
            {
                token,
                sesion
            });

        }

        






    }
}
