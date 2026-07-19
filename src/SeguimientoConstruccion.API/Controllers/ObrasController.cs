using Microsoft.AspNetCore.Mvc;
using SeguimientoConstruccion.Application.Common;
using SeguimientoConstruccion.Application.DTOs;
using SeguimientoConstruccion.Application.Services;

namespace SeguimientoConstruccion.API.Controllers
{
[ApiController]
    [Route("api/[controller]")]
    public class ObrasController : ControllerBase
    {
        private readonly ObraService _obraService;

        public ObrasController(ObraService obraService)
        {
            _obraService = obraService;
        }

        [HttpGet]
        public ActionResult<APIResponse<IEnumerable<ObraDTO>>> GetObras()
        {
            var response = _obraService.GetAllObras();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public ActionResult<APIResponse<ObraDTO>> GetObra(int id)
        {
            var response = _obraService.GetObraById(id);
            if (!response.Success) return NotFound(response);
            return Ok(response);
        }

        [HttpPost]
        public ActionResult<APIResponse<int>> PostObra(ObraDTO dto)
        {
            var response = _obraService.CreateObra(dto);
            if (!response.Success) return BadRequest(response);
            return StatusCode(201, response);
        }

        [HttpPut("{id}")]
        public ActionResult<APIResponse<bool>> PutObra(int id, ObraDTO dto)
        {
            var response = _obraService.UpdateObra(id, dto);
            if (!response.Success) return BadRequest(response);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public ActionResult<APIResponse<bool>> DeleteObra(int id)
        {
            var response = _obraService.DeleteObra(id);
            if (!response.Success) return BadRequest(response);
            return Ok(response);
        }
    }
}