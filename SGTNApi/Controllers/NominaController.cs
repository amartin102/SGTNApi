using Application.Dto;
using Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace SGTNApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NominaController : ControllerBase
    {
        private readonly INominaService _nominaService;

        public NominaController(INominaService nominaService)
        {
            _nominaService = nominaService;

        }

        [HttpGet("CalcularNomina/{strIdPeriodo}/{strIdCliente}")]
        public async Task<ActionResult<IEnumerable<CalculoNominaDto>>> CalcularNomina(string strIdPeriodo, string strIdCliente)
        {
            try
            {
                var resultado = await _nominaService.CalcularNominaAsync(strIdPeriodo, strIdCliente);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al calcular la nómina", error = ex.Message });
            }
        }
    }
}
