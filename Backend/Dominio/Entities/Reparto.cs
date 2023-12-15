namespace Dominio.Entities;
public class Reparto : BaseEntity
{
    public int IdSucursalFk { get; set; }
    public Sucursal Sucursal { get; set; }
    public DateOnly FechaSalida { get; set; }

    public ICollection<Envio> Envios { get; set; }
    public ICollection<Compra> Compras { get; set; }

}
