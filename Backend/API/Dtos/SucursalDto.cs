using Dominio.Entities;
namespace API.Dtos;
public class SucursalDto : BaseEntity
{
    public string Nombre { get; set; }
    public string Ciudad { get; set; }
    public string Direccion { get; set; }

    public ICollection<Inventario> Inventarios { get; set; }
    public ICollection<Reparto> Repartos { get; set; }
}
