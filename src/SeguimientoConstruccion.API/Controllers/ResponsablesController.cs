using Microsoft.AspNetCore.Mvc;
using SeguimientoConstruccion.Application.Common;
using SeguimientoConstruccion.Application.DTOs;
using SeguimientoConstruccion.Application.Services;

namespace SeguimientoConstruccion.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResponsablesController : ControllerBase
    {
        private readonly ResponsableService _responsableService;

        public ResponsablesController(ResponsableService responsableService)
        {
            _responsableService = responsableService;
        }

        [HttpGet]
        public ActionResult<APIResponse<IEnumerable<ResponsableDTO>>> GetResponsables()
        {
            var response = _responsableService.GetAllResponsables();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public ActionResult<APIResponse<ResponsableDTO>> GetResponsable(int id)
        {
            var response = _responsableService.GetResponsableById(id);
            if (!response.Success) return NotFound(response);
            return Ok(response);
        }

        [HttpPost]
        public ActionResult<APIResponse<int>> PostResponsable(ResponsableDTO dto)
        {
            var response = _responsableService.CreateResponsable(dto);
            if (!response.Success) return BadRequest(response);
            return StatusCode(201, response);
        }

        [HttpPut("{id}")]
        public ActionResult<APIResponse<bool>> PutResponsable(int id, ResponsableDTO dto)
        {
            var response = _responsableService.UpdateResponsable(id, dto);
            if (!response.Success) return BadRequest(response);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public ActionResult<APIResponse<bool>> DeleteResponsable(int id)
        {
            var response = _responsableService.DeleteResponsable(id);
            if (!response.Success) return BadRequest(response);
            return Ok(response);
        }
    }
}
