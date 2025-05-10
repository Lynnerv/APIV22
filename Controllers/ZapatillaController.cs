using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using APIV22.Models;
using APIV22.Data; // Asegúrate de tener esto
using System.IO;
using System.Threading.Tasks;

namespace APIV22.Controllers
{
    public class ZapatillaController : Controller
    {
        private readonly ILogger<ZapatillaController> _logger;
        private readonly IWebHostEnvironment _env;
        private readonly ApplicationDbContext _context;

        public ZapatillaController(ILogger<ZapatillaController> logger, IWebHostEnvironment env, ApplicationDbContext context)
        {
            _logger = logger;
            _env = env;
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(ZapatillaPersonalizada modelo, IFormFile Imagen)
        {
            if (!ModelState.IsValid)
                return View(modelo);

            if (Imagen != null && Imagen.Length > 0)
            {
                var uploads = Path.Combine(_env.WebRootPath, "uploads");
                if (!Directory.Exists(uploads))
                    Directory.CreateDirectory(uploads);

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(Imagen.FileName);
                var path = Path.Combine(uploads, fileName);

                using var stream = new FileStream(path, FileMode.Create);
                await Imagen.CopyToAsync(stream);

                modelo.NombreImagen = fileName;
            }

            try
            {
                _context.ZapatillasPersonalizadas.Add(modelo);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar en la base de datos");
                return Content($"ERROR al guardar: {ex.Message}");
            }

            return RedirectToAction("Index");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
