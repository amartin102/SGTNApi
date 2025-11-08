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
        private readonly IMaestroConceptoService _conceptService;

        public RegistroNovedadController(IRegistroNovedadService service, IMaestroConceptoService conceptService)
        {
            _service = service;
            _conceptService = conceptService;
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

        [HttpGet("concepts")]
        public async Task<ActionResult<IEnumerable<ConceptoNovedadDto>>> GetConcepts()
        {
            try
            {
                var items = await _conceptService.GetAllWithTipoAsync();
                return Ok(items);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<RegistroNovedadDto>> Create([FromBody] CreateRegistroNovedadDto createDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var created = await _service.CreateAsync(createDto);
                return CreatedAtAction(nameof(GetByEmployee), new { employeeId = created.EmpleadoId }, created);
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateRegistroNovedadDto updateDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                await _service.UpdateAsync(id, updateDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
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

        [HttpDelete("delete/{id}")]
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
