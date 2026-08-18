using SeguimientoConstruccion.Domain.Core;
namespace SeguimientoConstruccion.Domain.Entities
{
    public class Obra : BaseEntity
    {
        public string Nombre { get; set; } = string.Empty;
        public string Provincia { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinEstimada { get; set; }

        public List<Tarea> Tareas { get; set; } = new List<Tarea>();
    }

}
