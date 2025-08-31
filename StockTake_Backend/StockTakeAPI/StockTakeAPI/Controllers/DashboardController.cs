using Azure;
using Microsoft.AspNetCore.Mvc;
using StockTakeAPI.DTOs;
using StockTakeAPI.Interfaces;
using StockTakeAPI.Helpers;

namespace StockTakeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dbService;

        public DashboardController(IDashboardService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]
        [Route("resumen")]
        public async Task<IActionResult> Resumen()
        {
            var rsp = new Helpers.Response<DashboardDto>();

            try
            {
                rsp.status = true;
                rsp.value = await _dbService.Resumen();
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }
            return Ok(rsp);
        }



    }
}
