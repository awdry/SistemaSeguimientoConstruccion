namespace SeguimientoConstruccion.API.Models
{
    public class Responsable
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
        public string Contacto { get; set; } = string.Empty;

        public List<Tarea> Tareas { get; set; } = new List<Tarea>();
    }
}
