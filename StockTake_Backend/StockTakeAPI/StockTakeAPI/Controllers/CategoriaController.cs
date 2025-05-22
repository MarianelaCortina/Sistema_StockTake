using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockTakeAPI.Data;
using StockTakeAPI.DTOs;
using StockTakeAPI.Models;

namespace StockTakeAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriaController(AppDbContext context)
        {
            _context = context;
        }

      
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetCategories()
        {
            var categorias = await _context.Categorias.ToListAsync();

            var categoriasDTO = categorias.Select(c => new CategoriaDTO
            {
                Id = c.Id,
                Nombre = c.Nombre
            }).ToList();

            return Ok(categoriasDTO);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaDTO>> GetCategorie(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);

            if (categoria == null)
                return NotFound();

            return Ok(new CategoriaDTO
            {
                Id = categoria.Id,
                Nombre = categoria.Nombre
            });
        }

     
        [HttpPost]
        public async Task<ActionResult<CategoriaDTO>> CreateCategorie(CategoriaDTO dto)
        {
            var categoria = new Categoria
            {
                Nombre = dto.Nombre,
                EsActivo = true,
                FechaRegistro = DateTime.Now
            };

            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();

            dto.Id = categoria.Id;
            return CreatedAtAction(nameof(GetCategorie), new { id = dto.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditCategorie(int id, CategoriaDTO dto)
        {
            if (id != dto.Id)
                return BadRequest();

            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
                return NotFound();

            categoria.Nombre = dto.Nombre;

            await _context.SaveChangesAsync();
            return NoContent();
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategorie(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
                return NotFound();

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
