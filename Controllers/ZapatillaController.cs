using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Threading.Tasks;
using APIV22.Models;
using APIV22.Service; // ✅ Agregas esta línea para usar el servicio
    
namespace APIV22.Controllers
{
    public class ZapatillaController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly ZapatillaService _service; // ✅ Agregas esta línea para acceder al service

        // ✅ Inyectas el servicio por constructor
        public ZapatillaController(IWebHostEnvironment env, ZapatillaService service)
        {
            _env = env;
            _service = service;
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
                if (!Directory.Exists(uploads)) Directory.CreateDirectory(uploads);

                var fileName = Guid.NewGuid() + Path.GetExtension(Imagen.FileName);
                var path = Path.Combine(uploads, fileName);

                using var stream = new FileStream(path, FileMode.Create);
                await Imagen.CopyToAsync(stream);

                modelo.NombreImagen = fileName;
            }

            // ✅ Guardas la zapatilla usando el servicio
            await _service.CreateAsync(modelo);

            return RedirectToAction("Index");
        }
    }
}
