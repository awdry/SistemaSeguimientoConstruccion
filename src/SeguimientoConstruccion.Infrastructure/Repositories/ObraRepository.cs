using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SeguimientoConstruccion.Domain.Entities;
using SeguimientoConstruccion.Infrastructure.Context;

namespace SeguimientoConstruccion.Infrastructure.Repositories
{
    public class ObraRepository: GenericRepository<Obra>
    {
        public ObraRepository(AppDbContext context) : base(context) { }

        public IEnumerable<Obra> GetAllWithTareas()
        {
            return _context.Obras.Include(o => o.Tareas).ToList();
        }
    }
}
