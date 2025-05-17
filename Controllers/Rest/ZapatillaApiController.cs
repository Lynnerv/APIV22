using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using APIV22.Models;
using APIV22.Service; // ✅ Nuevo: usamos el servicio en lugar del DbContext

namespace APIV22.Controllers.Rest
{
    [ApiController]
    [Route("api/Zapatilla")]
    public class ZapatillaApiController : ControllerBase
    {
        private readonly ZapatillaService _service; // ✅ Nuevo: ahora usamos el servicio en lugar del contexto directo

        // ✅ Nuevo: inyectamos ZapatillaService en lugar de ApplicationDbContext
        public ZapatillaApiController(ZapatillaService service)
        {
            _service = service;
        }

        // GET: api/ZapatillaApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ZapatillaPersonalizada>>> GetAll()
        {
            // ✅ Usamos el servicio para obtener todas las zapatillas
            return await _service.GetAllAsync();
        }

        // GET: api/ZapatillaApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ZapatillaPersonalizada>> GetById(int id)
        {
            // ✅ Usamos el servicio para obtener por ID
            var zapatilla = await _service.GetByIdAsync(id);
            if (zapatilla == null)
                return NotFound();

            return zapatilla;
        }

        // POST: api/ZapatillaApi
        [HttpPost]
        public async Task<ActionResult<ZapatillaPersonalizada>> Create(ZapatillaPersonalizada model)
        {
            // ✅ Usamos el servicio para crear una nueva zapatilla
            var creada = await _service.CreateAsync(model);
            return CreatedAtAction(nameof(GetById), new { id = creada.Id }, creada);
        }

        // PUT: api/ZapatillaApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ZapatillaPersonalizada model)
        {
            if (id != model.Id)
                return BadRequest();

            // ✅ Usamos el servicio para actualizar
            var updated = await _service.UpdateAsync(id, model);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/ZapatillaApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // ✅ Usamos el servicio para eliminar
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
