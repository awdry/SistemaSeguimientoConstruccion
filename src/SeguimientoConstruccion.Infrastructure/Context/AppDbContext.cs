using Microsoft.EntityFrameworkCore;
using SeguimientoConstruccion.Domain.Entities;

namespace SeguimientoConstruccion.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Obra> Obras { get; set; }

        public DbSet<Tarea> Tareas { get; set; }

        public DbSet<Material> Materiales { get; set; }
        public DbSet<Responsable> Responsables { get; set; }

    }
}
