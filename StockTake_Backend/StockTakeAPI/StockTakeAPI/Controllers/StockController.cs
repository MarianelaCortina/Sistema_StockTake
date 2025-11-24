using Microsoft.AspNetCore.Mvc;
using StockTakeAPI.Interfaces;

namespace StockTakeAPI.Controllers
{
        [ApiController]
        [Route("api/[controller]")]
        public class StockController : ControllerBase
        {
            private readonly IStockService _stockService;

            public StockController(IStockService stockService)
            {
                _stockService = stockService;
            }

            /// <summary>
            /// Obtener stock actual de productos
            /// </summary>
            [HttpGet("productos")]
            public async Task<IActionResult> ObtenerStock()
            {
                var response = await _stockService.ObtenerStock();
                if (!response.Status) return BadRequest(response);
                return Ok(response);
            }

            /// <summary>
            /// Obtener últimos movimientos de stock
            /// </summary>
            [HttpGet("movimientos")]
            public async Task<IActionResult> ObtenerMovimientos()
            {
                var response = await _stockService.ObtenerMovimientos();
                if (!response.Status) return BadRequest(response);
                return Ok(response);
            }

            /// <summary>
            /// Obtener alertas de bajo stock basadas en IA predictiva
            /// </summary>
            [HttpGet("alertas")]
            public async Task<IActionResult> ObtenerAlertas()
            {
                var response = await _stockService.ObtenerAlertas();
                if (!response.Status) return BadRequest(response);
                return Ok(response);
            }

    }

}
