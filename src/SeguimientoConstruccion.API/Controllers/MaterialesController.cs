using Microsoft.AspNetCore.Mvc;
using SeguimientoConstruccion.Application.Common;
using SeguimientoConstruccion.Application.DTOs;
using SeguimientoConstruccion.Application.Services;

namespace SeguimientoConstruccion.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialesController : ControllerBase
    {
        private readonly MaterialService _materialService;

        public MaterialesController(MaterialService materialService)
        {
            _materialService = materialService;
        }

        [HttpGet]
        public ActionResult<APIResponse<IEnumerable<MaterialDTO>>> GetMateriales()
            => Ok(_materialService.GetAllMateriales());

        [HttpGet("{id}")]
        public ActionResult<APIResponse<MaterialDTO>> GetMaterial(int id)
        {
            var response = _materialService.GetMaterialById(id);
            if (!response.Success) return NotFound(response);
            return Ok(response);
        }

        [HttpPost]
        public ActionResult<APIResponse<int>> PostMaterial(MaterialDTO dto)
        {
            var response = _materialService.CreateMaterial(dto);
            if (!response.Success) return BadRequest(response);
            return StatusCode(201, response);
        }

        [HttpPut("{id}")]
        public ActionResult<APIResponse<bool>> PutMaterial(int id, MaterialDTO dto)
        {
            var response = _materialService.UpdateMaterial(id, dto);
            if (!response.Success) return BadRequest(response);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public ActionResult<APIResponse<bool>> DeleteMaterial(int id)
        {
            var response = _materialService.DeleteMaterial(id);
            if (!response.Success) return BadRequest(response);
            return Ok(response);
        }
    }
}
