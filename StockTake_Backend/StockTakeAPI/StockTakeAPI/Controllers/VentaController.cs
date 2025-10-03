using Microsoft.AspNetCore.Mvc;
using StockTakeAPI.DTOs;
using StockTakeAPI.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace StockTakeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VentaController : ControllerBase
    {
        private readonly IVentaService _ventaService;

        public VentaController(IVentaService ventaService)
        {
            _ventaService = ventaService;
        }

        /// <summary>
        /// Registrar una nueva venta
        /// </summary>
        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] VentaDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _ventaService.Registrar(model);

            if (!response.Status)
                return BadRequest(response);

            return Ok(response);
        }

        /// <summary>
        /// Obtener historial de ventas
        /// buscarPor = "fecha" o "numero"
        /// </summary>
        [HttpGet("historial")]
        public async Task<IActionResult> Historial(
            [FromQuery] string buscarPor,
            [FromQuery] string? numeroVenta,
            [FromQuery] string? fechaInicio,
            [FromQuery] string? fechaFin)
        {
            var response = await _ventaService.Historial(buscarPor, numeroVenta, fechaInicio, fechaFin);

            if (!response.Status)
                return BadRequest(response);

            return Ok(response);
        }

    }
}

