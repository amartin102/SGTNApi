using Application.Interface;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Abstractions;

namespace SGTNApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParameterMasterController : Controller
    {
        private readonly IParameterMasterService _parameterMasterService;
        public ParameterMasterController(IParameterMasterService parameterMasterService)
        {
            _parameterMasterService = parameterMasterService;
        }

        [HttpGet]
        [Route("GetParameterById")]
        public async Task<ActionResult<ParameterMasterDto>> GetOrderById(Guid id)
        {
            try
            {              
                var result = await _parameterMasterService.GetParameterById(id);

                if (result == null)
                    return NotFound($"Not Found");
            
                return Ok(result);
            }
            catch (Exception ex)
            {              
                return StatusCode(500, $"Internal Error: {ex.Message}");
            }

        }

        [HttpPost]
        [Route("CreateParameter")]
        public async Task<IActionResult> CreateOrder(ParameterMasterDto dto)
        {           
            try
            {
                if (dto.strIdParametro == null)
                {
                    return BadRequest(400);
                }

                var result = await _parameterMasterService.CreateParameter(dto);

                return Ok(result);
            }
            catch (Exception ex)
            {              
                return StatusCode(500, $"Internal Error: {ex.Message}");
            }

        }

        [HttpPut]
        [Route("UpdateParameter")]
        public async Task<IActionResult> UpdateOrder(ParameterMasterDto dto)
        {
            
            try
            {
                if (dto.strIdParametro == null)
                {
                    return BadRequest(400);
                }

                var result = await _parameterMasterService.UpdateParameter(dto);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Error: {ex.Message}");
            }

        }
    }
}
