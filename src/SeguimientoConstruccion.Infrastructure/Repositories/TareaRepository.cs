using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SeguimientoConstruccion.Domain.Entities;
using SeguimientoConstruccion.Infrastructure.Context;

namespace SeguimientoConstruccion.Infrastructure.Repositories
{
    public class TareaRepository: GenericRepository<Tarea>
    {
        public TareaRepository(AppDbContext context) : base(context) { }

        public IEnumerable<Tarea> GetAllWithObras()
        {
            return _context.Tareas.Include(t => t.Obra).Include(t => t.Responsable).ToList();
        }
    }
}
