using System.Reflection;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistencia;
public class ApiContext : DbContext
{
   public ApiContext(DbContextOptions options) : base(options)
   { }
   public DbSet<Rol> Roles { get; set; }
   public DbSet<RolUsuario> RolUsuarios { get; set; }
   public DbSet<Usuario> Usuarios { get; set; }
   public DbSet<Cliente> Clientes { get; set; }
   public DbSet<Sucursal> Sucursales { get; set; }
   public DbSet<Reparto> Repartos { get; set; }
   public DbSet<Compra> Compras { get; set; }
   public DbSet<Inventario> Inventarios { get; set; }
   public DbSet<Producto> Productos { get; set; }
   public DbSet<Categoria> Categorias { get; set; }
   public DbSet<Envio> Envios { get; set; }
   public DbSet<ProductoCompra> ProductoCompras { get; set; }
   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
      base.OnModelCreating(modelBuilder);

      modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
   }

}
