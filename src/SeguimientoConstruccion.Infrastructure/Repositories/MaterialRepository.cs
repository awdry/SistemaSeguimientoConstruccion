using SeguimientoConstruccion.Domain.Entities;
using SeguimientoConstruccion.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeguimientoConstruccion.Infrastructure.Repositories
{
    public class MaterialRepository : GenericRepository<Material>
    {
        public MaterialRepository(AppDbContext context) : base(context) { }

    }
}
