using System;
using System.Collections.Generic;
using System.Text;
using SeguimientoConstruccion.Domain.Entities;
using SeguimientoConstruccion.Infrastructure.Context;

namespace SeguimientoConstruccion.Infrastructure.Repositories
{
    public class ResponsableRepository : GenericRepository<Responsable>
    {
        public ResponsableRepository(AppDbContext context) : base(context) { }
    }
}