namespace SeguimientoConstruccion.API.DTOs
{
    public class TareaDto
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public double PorcentajeAvance { get; set; }
        public int ObraId { get; set; }
        public int ResponsableId { get; set; }
    }
}
