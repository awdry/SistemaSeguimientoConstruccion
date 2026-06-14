using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeguimientoConstruccion.API.Data;
using SeguimientoConstruccion.API.DTOs;
using SeguimientoConstruccion.API.Models;

namespace SeguimientoConstruccion.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ObrasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ObrasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ObraDto>>> GetObras()
        {
            var obras = await _context.Obras
                .Select(o => new ObraDto
                {
                    Id = o.Id,
                    Nombre = o.Nombre,
                    Ubicacion = o.Ubicacion,
                    FechaInicio = o.FechaInicio,
                    FechaFinEstimada = o.FechaFinEstimada,
                    Estado = o.Estado
                })
                .ToListAsync();
            return Ok(obras);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ObraDto>> GetObra(int id)
        {
            var obra = await _context.Obras.FindAsync(id);
            if (obra == null) return NotFound();

            return Ok(new ObraDto
            {
                Id = obra.Id,
                Nombre = obra.Nombre,
                Ubicacion = obra.Ubicacion,
                FechaInicio = obra.FechaInicio,
                FechaFinEstimada = obra.FechaFinEstimada,
                Estado = obra.Estado
            });

        }

        [HttpPost]
        public async Task<ActionResult<ObraDto>> PostObra(ObraDto dto)
        {
            var obra = new Obra
            {
                Nombre = dto.Nombre,
                Ubicacion = dto.Ubicacion,
                FechaInicio = dto.FechaInicio,
                FechaFinEstimada = dto.FechaFinEstimada,
                Estado = dto.Estado
            };

            _context.Obras.Add(obra);
            await _context.SaveChangesAsync();

            dto.Id = obra.Id;
            return CreatedAtAction(nameof(GetObra), new { id = obra.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutObra(int id, ObraDto dto)
        {
            var obra = await _context.Obras.FindAsync(id);
            if (obra == null) return NotFound();

            obra.Nombre = dto.Nombre;
            obra.Ubicacion = dto.Ubicacion;
            obra.FechaInicio = dto.FechaInicio;
            obra.FechaFinEstimada = dto.FechaFinEstimada;
            obra.Estado = dto.Estado;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteObra(int id)
        {
            var obra = await _context.Obras.FindAsync(id);
            if (obra == null) return NotFound();

            _context.Obras.Remove(obra);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}