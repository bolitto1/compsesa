using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEBAPI.Models;

namespace WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class prueba : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public prueba(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DatosSensor>>> GetDatosSensores()
        {
            // Obtener las primeras 10 filas de la tabla DatosSensor
            var datosSensores = await _context.DatosSensores.Take(10).ToListAsync();

            return Ok(datosSensores);
        }
    }
}
