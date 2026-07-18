using SeguimientoConstruccion.Domain.Core;
namespace SeguimientoConstruccion.Domain.Entities
{
    public class Material : BaseEntity
    {
        public string ?Nombre { get; set; }
        public string UnidadMedida { get; set; } = string.Empty;
        public decimal CostoUnitario { get; set; }
        public decimal CantidadUsada { get; set; }
        public decimal CostoTotal => CostoUnitario * CantidadUsada;

        public int TareaId { get; set; }
        public Tarea? Tarea { get; set; }

    }
}
