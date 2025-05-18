using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using APIV22.Integration.galletafortuna;
using APIV22.Integration.galletafortuna.dto;


namespace APIV22.Controllers
{

    public class GalletaController : Controller
    {
        private readonly ILogger<GalletaController> _logger;
        private readonly GalletaApiIntegration _apiIntegration;

        public GalletaController(ILogger<GalletaController> logger, GalletaApiIntegration apiIntegration)
        {
            _logger = logger;
            _apiIntegration = apiIntegration ?? throw new ArgumentNullException(nameof(apiIntegration));
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var mensajeRaw = await _apiIntegration.ObtenerMensajeAsync();

                // Extrae solo el texto de la propiedad "text" del JSON de RapidAPI
                using var jsonDoc = System.Text.Json.JsonDocument.Parse(mensajeRaw);
                var texto = jsonDoc.RootElement.GetProperty("text").GetString();

                var galleta = new Galleta { Mensaje = texto };
                return View(galleta);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la galleta de la fortuna.");
                return View("Error");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
