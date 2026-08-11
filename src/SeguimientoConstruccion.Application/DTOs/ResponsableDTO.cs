using System;
using System.Collections.Generic;
using System.Text;

namespace SeguimientoConstruccion.Application.DTOs
{
    public class ResponsableDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
        public string Contacto { get; set; } = string.Empty;
    }
}