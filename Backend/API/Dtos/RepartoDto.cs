using Dominio.Entities;
namespace API.Dtos;
public class RepartoDto : BaseEntity
{
    public int IdSucursalFk { get; set; }
    public Sucursal Sucursal { get; set; }
    public DateOnly FechaSalida { get; set; }

    public ICollection<Envio> Envios { get; set; }
    public ICollection<Compra> Compras { get; set; }
}
