using System;
using System.Collections.Generic;
using System.Text;

namespace SeguimientoConstruccion.Application.DTOs
{
    public class MaterialDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string UnidadMedida { get; set; } = string.Empty;
        public decimal CostoUnitario { get; set; }
        public decimal CantidadUsada { get; set; }
        public decimal CostoTotal { get; set; }
        public int TareaId { get; set; }
    }
}
