using Application.Dto;
using Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace SGTNApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaestroPeriodoController : ControllerBase
    {
        private readonly IMaestroPeriodoService _service;

        public MaestroPeriodoController(IMaestroPeriodoService service)
        {
            _service = service;
        }

        [HttpGet("byclient/{clientId}")]
        public async Task<ActionResult<IEnumerable<MaestroPeriodoDto>>> GetByClient(Guid clientId)
        {
            try
            {
                var items = await _service.GetByClientIdAsync(clientId);
                return Ok(items);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
            }
        }

        [HttpPut("byclient/{clientId}")]
        public async Task<IActionResult> UpdateRangeForClient(Guid clientId, [FromBody] IEnumerable<MaestroPeriodoDto> updateDtos)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                await _service.UpdateRangeForClientAsync(clientId, updateDtos);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
            }
        }
    }
}
