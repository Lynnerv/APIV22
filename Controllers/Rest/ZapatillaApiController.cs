using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIV22.Data;
using APIV22.Models;

namespace APIV22.Controllers.Rest
{
    [ApiController]
    [Route("api/Zapatilla")]
    public class ZapatillaApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ZapatillaApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ZapatillaApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ZapatillaPersonalizada>>> GetAll()
        {
            return await _context.ZapatillasPersonalizadas.ToListAsync();
        }

        // GET: api/ZapatillaApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ZapatillaPersonalizada>> GetById(int id)
        {
            var zapatilla = await _context.ZapatillasPersonalizadas.FindAsync(id);
            if (zapatilla == null)
                return NotFound();

            return zapatilla;
        }

        // POST: api/ZapatillaApi
        [HttpPost]
        public async Task<ActionResult<ZapatillaPersonalizada>> Create(ZapatillaPersonalizada model)
        {
            _context.ZapatillasPersonalizadas.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
        }

        // PUT: api/ZapatillaApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ZapatillaPersonalizada model)
        {
            if (id != model.Id)
                return BadRequest();

            _context.Entry(model).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.ZapatillasPersonalizadas.Any(e => e.Id == id))
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        // DELETE: api/ZapatillaApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var zapatilla = await _context.ZapatillasPersonalizadas.FindAsync(id);
            if (zapatilla == null)
                return NotFound();

            _context.ZapatillasPersonalizadas.Remove(zapatilla);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
