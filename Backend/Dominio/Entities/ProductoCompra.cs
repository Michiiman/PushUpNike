namespace Dominio.Entities;
public class ProductoCompra : BaseEntity
{
    public int IdCompraFK { get; set; }
    public Compra Compra { get; set; }
    public int IdProductoFK { get; set; }
    public Producto Producto { get; set; }
    
}
