using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class ProductoCompraConfiguration : IEntityTypeConfiguration<ProductoCompra>
    {
        public void Configure(EntityTypeBuilder<ProductoCompra> builder)
        {

            builder.ToTable("productoCompra");

            builder.HasKey(e => new { e.IdProductoFK, e.IdCompraFK });

            builder.HasOne(p => p.Producto)
            .WithMany(p => p.ProductoCompras)
            .HasForeignKey(p => p.IdProductoFK);

            builder.HasOne(p => p.Compra)
            .WithMany(p => p.ProductoCompras)
            .HasForeignKey(p => p.IdCompraFK);
        }
    }
}