namespace Dominio.Entities;
public class Producto : BaseEntity
{
    public string Titulo { get; set; }
    public int IdCategoriaFk { get; set; }
    public Categoria Categoria { get; set; }
    public int IdInventarioFk { get; set; }
    public Inventario Inventario { get; set; }
    public string Imagen { get; set; }
    public int Precio { get; set; }

    public ICollection<Compra> Compras { get; set; } = new HashSet<Compra>();
    public ICollection<ProductoCompra> ProductoCompras { get; set; }

}
