using APIV22.Data; // ✅ Necesario para acceder al DbContext
using APIV22.Models; // ✅ Para usar el modelo ZapatillaPersonalizada
using Microsoft.EntityFrameworkCore;
using APIV22.Service;

namespace APIV22.Service // Asegúrate de que este namespace coincida con tu estructura
{
    public class ZapatillaService
    {
        private readonly ApplicationDbContext _context;

        // ✅ Inyectamos el contexto para acceder a la base de datos
        public ZapatillaService(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ Obtener todas las zapatillas
        public async Task<List<ZapatillaPersonalizada>> GetAllAsync()
        {
            return await _context.ZapatillasPersonalizadas.ToListAsync();
        }

        // ✅ Obtener una zapatilla por ID
        public async Task<ZapatillaPersonalizada?> GetByIdAsync(int id)
        {
            return await _context.ZapatillasPersonalizadas.FindAsync(id);
        }

        // ✅ Crear una nueva zapatilla
        public async Task<ZapatillaPersonalizada> CreateAsync(ZapatillaPersonalizada modelo)
        {
            _context.ZapatillasPersonalizadas.Add(modelo);
            await _context.SaveChangesAsync();
            return modelo;
        }

        // ✅ Actualizar una zapatilla existente
        public async Task<bool> UpdateAsync(int id, ZapatillaPersonalizada modelo)
        {
            if (id != modelo.Id)
                return false;

            _context.Entry(modelo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // ✅ Eliminar una zapatilla por ID
        public async Task<bool> DeleteAsync(int id)
        {
            var zapatilla = await _context.ZapatillasPersonalizadas.FindAsync(id);
            if (zapatilla == null)
                return false;

            _context.ZapatillasPersonalizadas.Remove(zapatilla);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
