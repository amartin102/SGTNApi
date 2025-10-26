using Application.Dto;
using Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace SGTNApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MasterParametersController : ControllerBase
    {
        private readonly IMasterParameterService _masterParameterService;

        public MasterParametersController(IMasterParameterService masterParameterService)
        {
            _masterParameterService = masterParameterService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MasterParameterDto>>> GetAll()
        {
            var parameters = await _masterParameterService.GetAllAsync();
            return Ok(parameters);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MasterParameterDto>> GetById(Guid id)
        {
            var parameter = await _masterParameterService.GetByIdAsync(id);
            return Ok(parameter);
        }

        [HttpPost]
        public async Task<ActionResult<MasterParameterDto>> Create(CreateMasterParameterDto createDto)
        {
            var created = await _masterParameterService.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateMasterParameterDto updateDto)
        {
            await _masterParameterService.UpdateAsync(id, updateDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _masterParameterService.DeleteAsync(id);
            return NoContent();
        }
    }
}
