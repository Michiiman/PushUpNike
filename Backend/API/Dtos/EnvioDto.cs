using Dominio.Entities;
namespace API.Dtos;
public class EnvioDto : BaseEntity
{
    public string Ruta { get; set; }
    public int IdRepartoFk { get; set; }
    public Reparto Reparto { get; set; }
    public DateOnly FechaEnvio { get; set; }
    public string EstadoEnvio { get; set; }
    public string Seguimiento { get; set; }
    public DateOnly FechaEntrega { get; set; }
    public int TotalPagar { get; set; }
    
}
