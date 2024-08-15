using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEBAPI.Models;

namespace WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReporteController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReporteController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{modo}/{fechaDesde}/{fechaHasta}")]
        public async Task<IActionResult> GetData(string modo, DateTime fechaDesde, DateTime fechaHasta)
        {
            fechaDesde = DateTime.SpecifyKind(fechaDesde, DateTimeKind.Utc);
            fechaHasta = DateTime.SpecifyKind(fechaHasta, DateTimeKind.Utc);

            if (modo != "day" && modo != "month")
            {
                return BadRequest("Modo debe ser 'day' o 'month'.");
            }

            // Obtener los códigos de parámetros de la tabla parametros_sensores
            var parametros = await _context.ParametrosSensores.ToListAsync();

            var query = _context.DatosSensores
                .Where(data => data.FechaDato >= fechaDesde && data.FechaDato <= fechaHasta);

            if (modo == "day")
            {
                var result = await query
                    .GroupBy(data => new { data.FechaDato.Date, data.CodigoParametro })
                    .Select(g => new
                    {
                        Fecha = g.Key.Date,
                        CodigoParametro = g.Key.CodigoParametro,
                        AvgData = g.Average(d => d.ValorNumero),
                        MinData = g.Min(d => d.ValorNumero),
                        MaxData = g.Max(d => d.ValorNumero)
                    })
                    .OrderBy(r => r.Fecha) // Asegúrate de ordenar por fecha
                    .ToListAsync();

                // Agrupar los resultados por código de parámetro
                var deviceData = parametros.Select(param => new
                {
                    CodigoParametro = param.CodigoParametro,
                    NombreParametro = param.DescripcionLarga,
                    UnidadParametro = param.Unidad,
                    AbreviacionParametro = param.Abreviacion,
                    Values = new
                    {
                        AvgData = result.Where(r => r.CodigoParametro == param.CodigoParametro).Select(r => r.AvgData).ToList(),
                        MinData = result.Where(r => r.CodigoParametro == param.CodigoParametro).Select(r => r.MinData).ToList(),
                        MaxData = result.Where(r => r.CodigoParametro == param.CodigoParametro).Select(r => r.MaxData).ToList()
                    }
                }).ToList();

                // Obtener las fechas en orden ascendente
                var deviceDates = result.Select(r => r.Fecha.ToString("yyyy-MM-dd"))
                    .Distinct()
                    .OrderBy(date => date) // Ordenar las fechas
                    .ToList();

                return Ok(new { device_dates = deviceDates, device_data = deviceData });
            }
            else {
                var result = await query
               .GroupBy(data => new { data.FechaDato.Year, data.FechaDato.Month, data.CodigoParametro })
               .Select(g => new
               {
                   Fecha = new DateTime(g.Key.Year, g.Key.Month, 1), // Primer día del mes
                   CodigoParametro = g.Key.CodigoParametro,
                   AvgData = g.Average(d => d.ValorNumero),
                   MinData = g.Min(d => d.ValorNumero),
                   MaxData = g.Max(d => d.ValorNumero)
               })
               .OrderBy(r => r.Fecha) // Asegúrate de ordenar por fecha
               .ToListAsync();

                // Agrupar los resultados por código de parámetro
                var deviceData = parametros.Select(param => new
                {
                    CodigoParametro = param.CodigoParametro,
                    NombreParametro = param.DescripcionLarga,
                    UnidadParametro = param.Unidad,
                    AbreviacionParametro = param.Abreviacion,
                    Values = new
                    {
                        AvgData = result.Where(r => r.CodigoParametro == param.CodigoParametro).Select(r => r.AvgData).ToList(),
                        MinData = result.Where(r => r.CodigoParametro == param.CodigoParametro).Select(r => r.MinData).ToList(),
                        MaxData = result.Where(r => r.CodigoParametro == param.CodigoParametro).Select(r => r.MaxData).ToList()
                    }
                }).ToList();

                var deviceDates = result.Select(r => $"{r.Fecha:yyyy-MM-dd} – {new DateTime(r.Fecha.Year, r.Fecha.Month, DateTime.DaysInMonth(r.Fecha.Year, r.Fecha.Month)).ToString("yyyy-MM-dd")}")
                    .Distinct()
                    .OrderBy(date => date)
                    .ToList();

                return Ok(new { device_dates = deviceDates, device_data = deviceData });
            }
        }
    }
}
