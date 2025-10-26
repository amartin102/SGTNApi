using Application.Dto;
using Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace SGTNApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParameterValuesController : ControllerBase
    {
        private readonly IParameterValueService _parameterValueService;

        public ParameterValuesController(IParameterValueService parameterValueService)
        {
            _parameterValueService = parameterValueService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParameterValueDto>>> GetAll()
        {
            try
            {
                var values = await _parameterValueService.GetAllAsync();
                return Ok(values);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ParameterValueDto>> GetById(Guid id)
        {
            try
            {
                var value = await _parameterValueService.GetByIdAsync(id);
                return Ok(value);
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

        [HttpGet("parameter/{parameterId}")]
        public async Task<ActionResult<IEnumerable<ParameterValueDto>>> GetByParameterId(Guid parameterId)
        {
            try
            {
                var values = await _parameterValueService.GetByParameterIdAsync(parameterId);
                return Ok(values);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
            }
        }

        [HttpGet("client/{clientId}")]
        public async Task<ActionResult<IEnumerable<ParameterValueDto>>> GetByClientId(Guid clientId)
        {
            try
            {
                var values = await _parameterValueService.GetByClientIdAsync(clientId);
                return Ok(values);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
            }
        }

        [HttpGet("employee/{employeeId}")]
        public async Task<ActionResult<IEnumerable<ParameterValueDto>>> GetByEmployeeId(Guid employeeId)
        {
            try
            {
                var values = await _parameterValueService.GetByEmployeeIdAsync(employeeId);
                return Ok(values);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
            }
        }

        [HttpGet("client/{clientId}/parameter/{parameterId}")]
        public async Task<ActionResult<IEnumerable<ParameterValueDto>>> GetByClientAndParameter(Guid clientId, Guid parameterId)
        {
            try
            {
                var values = await _parameterValueService.GetByClientAndParameterAsync(clientId, parameterId);
                return Ok(values);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<ParameterValueDto>> Create([FromBody] CreateParameterValueDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var created = await _parameterValueService.CreateAsync(createDto);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
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
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateParameterValueDto updateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await _parameterValueService.UpdateAsync(id, updateDto);
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _parameterValueService.DeleteAsync(id);
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
