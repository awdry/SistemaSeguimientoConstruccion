using SeguimientoConstruccion.Domain.Core;
namespace SeguimientoConstruccion.Domain.Entities
{
    public class Responsable : BaseEntity
    {
        public string Nombre { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
        public string Contacto { get; set; } = string.Empty;

        public List<Tarea> Tareas { get; set; } = new List<Tarea>();
    }
}
