using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Examen.Models;

namespace Examen.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public DashboardController(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Método para obtener datos de la API
        public async Task<IActionResult> GetData(string modo, DateTime fechaDesde, DateTime fechaHasta)
        {
            string baseUrl = _configuration.GetValue<string>("WEBAPI");
            string endpoint = $"{baseUrl}/reporte/{modo}/{fechaDesde:yyyy-MM-dd}/{fechaHasta:yyyy-MM-dd}";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(endpoint);

                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    var deserializedData = JsonConvert.DeserializeObject<ChartData>(jsonData);
                    return Json(deserializedData);
                }
                else
                {
                    return StatusCode((int)response.StatusCode, "Error al obtener datos de la API.");
                }
            }
        }
    }
}
