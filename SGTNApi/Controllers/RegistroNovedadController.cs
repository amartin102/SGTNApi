using Application.Interface;
using Application.Dto;
using Microsoft.AspNetCore.Mvc;

namespace SGTNApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistroNovedadController : ControllerBase
    {
        private readonly IRegistroNovedadService _service;

        public RegistroNovedadController(IRegistroNovedadService service)
        {
            _service = service;
        }

        [HttpGet("byemployee/{employeeId}")]
        public async Task<ActionResult<IEnumerable<RegistroNovedadDto>>> GetByEmployee(Guid employeeId)
        {
            try
            {
                var items = await _service.GetByEmpleadoIdAsync(employeeId);
                return Ok(items);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
            }
        }

        [HttpGet("byperiod/{identificadorPeriodo}")]
        public async Task<ActionResult<IEnumerable<RegistroNovedadDto>>> GetByPeriod(string identificadorPeriodo)
        {
            try
            {
                var items = await _service.GetByPeriodoIdentifierAsync(identificadorPeriodo);
                return Ok(items);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
            }
        }
    }
}
