using Microsoft.AspNetCore.Mvc;
using SeguimientoConstruccion.Application.Common;
using SeguimientoConstruccion.Application.DTOs;
using SeguimientoConstruccion.Application.Services;

namespace SeguimientoConstruccion.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TareasController : ControllerBase
    {
        private readonly TareaService _tareaService;

        public TareasController(TareaService tareaService)
        {
            _tareaService = tareaService;
        }

        [HttpGet]
        public ActionResult<APIResponse<IEnumerable<TareaDTO>>> GetTareas()
        {
            var response = _tareaService.GetAllTareas();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public ActionResult<APIResponse<TareaDTO>> GetTarea(int id)
        {
            var response = _tareaService.GetTareaById(id);
            if (!response.Success) return NotFound(response);
            return Ok(response);
        }

        [HttpPost]
        public ActionResult<APIResponse<int>> PostTarea(TareaDTO dto)
        {
            var response = _tareaService.CreateTarea(dto);
            if (!response.Success) return BadRequest(response);
            return StatusCode(201, response);
        }

        [HttpPut("{id}")]
        public ActionResult<APIResponse<bool>> PutTarea(int id, TareaDTO dto)
        {
            var response = _tareaService.UpdateTarea(id, dto);
            if (!response.Success) return BadRequest(response);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public ActionResult<APIResponse<bool>> DeleteTarea(int id)
        {
            var response = _tareaService.DeleteTarea(id);
            if (!response.Success) return BadRequest(response);
            return Ok(response);
        }
    }
}