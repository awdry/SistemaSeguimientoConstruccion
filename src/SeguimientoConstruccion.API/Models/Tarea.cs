namespace SeguimientoConstruccion.API.Models
{
    public class Tarea
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public double PorcentajeAvance { get; set; }

        public int ObraId { get; set; }
        public Obra? Obra { get; set; }

        public List<Material> Materiales { get; set; } = new List<Material>();

        public int ResponsableId { get; set; }
        public Responsable? Responsable { get; set; }
    }
}
