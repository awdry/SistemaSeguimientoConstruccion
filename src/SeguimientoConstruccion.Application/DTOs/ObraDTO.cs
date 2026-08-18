namespace SeguimientoConstruccion.Application.DTOs
{
    public class ObraDTO
    {
     public int Id { get; set; }
     public string Nombre { get; set; } = string.Empty;
     public string Provincia { get; set; } = string.Empty;
     public string Direccion { get; set; } = string.Empty;
     public string Estado { get; set; } = string.Empty;
     public DateTime FechaInicio { get; set; }
     public DateTime FechaFinEstimada { get; set; }


    }
}
