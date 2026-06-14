using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeguimientoConstruccion.API.Data;
using SeguimientoConstruccion.API.DTOs;
using SeguimientoConstruccion.API.Models;

namespace SeguimientoConstruccion.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TareasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TareasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TareaDto>>> GetTareas()
        {
            var tareas = await _context.Tareas
                .Select(t => new TareaDto
                {
                    Id = t.Id,
                    Descripcion = t.Descripcion,
                    FechaInicio = t.FechaInicio,
                    FechaFin = t.FechaFin,
                    PorcentajeAvance = t.PorcentajeAvance,
                    ObraId = t.ObraId,
                    ResponsableId = t.ResponsableId
                })
                .ToListAsync();

            return Ok(tareas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TareaDto>> GetTarea(int id)
        {
            var tarea = await _context.Tareas.FindAsync(id);
            if (tarea == null) return NotFound();

            return Ok(new TareaDto
            {
                Id = tarea.Id,
                Descripcion = tarea.Descripcion,
                FechaInicio = tarea.FechaInicio,
                FechaFin = tarea.FechaFin,
                PorcentajeAvance = tarea.PorcentajeAvance,
                ObraId = tarea.ObraId,
                ResponsableId = tarea.ResponsableId
            });
        }

        [HttpPost]
        public async Task<ActionResult<TareaDto>> PostTarea(TareaDto dto)
        {
            var tarea = new Tarea
            {
                Descripcion = dto.Descripcion,
                FechaInicio = dto.FechaInicio,
                FechaFin = dto.FechaFin,
                PorcentajeAvance = dto.PorcentajeAvance,
                ObraId = dto.ObraId,
                ResponsableId = dto.ResponsableId
            };

            _context.Tareas.Add(tarea);
            await _context.SaveChangesAsync();

            dto.Id = tarea.Id;
            return CreatedAtAction(nameof(GetTarea), new { id = tarea.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTarea(int id, TareaDto dto)
        {
            var tarea = await _context.Tareas.FindAsync(id);
            if (tarea == null) return NotFound();

            tarea.Descripcion = dto.Descripcion;
            tarea.FechaInicio = dto.FechaInicio;
            tarea.FechaFin = dto.FechaFin;
            tarea.PorcentajeAvance = dto.PorcentajeAvance;
            tarea.ObraId = dto.ObraId;
            tarea.ResponsableId = dto.ResponsableId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarea(int id)
        {
            var tarea = await _context.Tareas.FindAsync(id);
            if (tarea == null) return NotFound();

            _context.Tareas.Remove(tarea);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}