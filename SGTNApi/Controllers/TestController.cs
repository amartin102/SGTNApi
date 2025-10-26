using Microsoft.AspNetCore.Mvc;
using Repository.Context;

namespace SGTNApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly SqlDbContext _context;

        public TestController(SqlDbContext context)
        {
            _context = context;
        }

        [HttpGet("database")]
        public async Task<IActionResult> TestDatabase()
        {
            try
            {
                var canConnect = await _context.Database.CanConnectAsync();
                return Ok(new
                {
                    message = "Conexión exitosa a la base de datos",
                    database = "Ok",
                    server = "Ok"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = "Error conectando a la base de datos",
                    error = ex.Message
                });
            }
        }
    }

}
