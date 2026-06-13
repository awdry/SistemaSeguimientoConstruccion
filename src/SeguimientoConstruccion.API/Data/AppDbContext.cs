using Microsoft.EntityFrameworkCore;
using SeguimientoConstruccion.API.Models;

namespace SeguimientoConstruccion.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Obra> Obras { get; set; }

    }
}
