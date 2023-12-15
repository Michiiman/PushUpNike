namespace Dominio.Entities;
public class Compra : BaseEntity
{
    public int IdRepartoFk { get; set; }
    public Reparto Reparto { get; set; }
    public int IdClienteFk{get;set;}
    public Cliente Cliente{get;set;}
    public int Precio { get; set; }
    public int Cantidad { get; set; }
    public int Total { get; set; }

    public ICollection<Producto> Productos { get; set; } = new HashSet<Producto>();
    public ICollection<ProductoCompra> ProductoCompras { get; set; }


}
