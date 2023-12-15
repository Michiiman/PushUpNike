namespace Dominio.Entities;
public class Inventario : BaseEntity
{
    public string Descripcion { get; set; }
    public int IdSucursalFk { get; set; }
    public Sucursal Sucursal { get; set; }
    public DateOnly FechaEntrada { get; set; }
    public DateOnly FechaSalida { get; set; }

    public ICollection<Producto> Productos { get; set; }

}
