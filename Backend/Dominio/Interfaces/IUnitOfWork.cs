namespace Dominio.Interfaces;
public interface IUnitOfWork
{
   IRol Roles { get; }
   IUsuario Usuarios { get; }
   ICliente Clientes { get; }
   ISucursal Sucursales { get; }
   IReparto Repartos { get; }
   ICompra Compras { get; }
   IInventario Inventarios { get; }
   IProducto Productos { get; }
   ICategoria Categorias { get; }
   IEnvio Envios { get; }
Task<int> SaveAsync();
}
