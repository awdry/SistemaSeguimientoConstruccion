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
        public MaterialRepository Material { get; set; }

        public UnitOfWork(AppDbContext context, ObraRepository obra, TareaRepository tarea, ResponsableRepository responsable, MaterialRepository material)
        {
            _context = context;
            Obra = obra;
            Tarea = tarea;
            Responsable = responsable;
            Material = material;
        }

       public void Complete()
        {
            _context.SaveChanges();
        }
    }
}
