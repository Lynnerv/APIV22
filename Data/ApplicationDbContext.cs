using Microsoft.EntityFrameworkCore;
using APIV22.Models;

namespace APIV22.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ZapatillaPersonalizada> ZapatillasPersonalizadas { get; set; }
    }
}
