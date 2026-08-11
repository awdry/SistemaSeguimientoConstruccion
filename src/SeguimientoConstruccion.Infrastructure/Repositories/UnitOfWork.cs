using System;
using System.Collections.Generic;
using System.Text;
using SeguimientoConstruccion.Infrastructure.Context;

namespace SeguimientoConstruccion.Infrastructure.Repositories
{
    public class UnitOfWork
    {
        private readonly AppDbContext _context;

        public ObraRepository Obra { get;  set; }
        public TareaRepository Tarea { get; set; }
        public ResponsableRepository Responsable { get; set; }

        public UnitOfWork(AppDbContext context, ObraRepository obra, TareaRepository tarea, ResponsableRepository responsable)
        {
            _context = context;
            Obra = obra;
            Tarea = tarea;
            Responsable = responsable;
        }

       public void Complete()
        {
            _context.SaveChanges();
        }
    }
}
