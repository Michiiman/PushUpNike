using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
public class ProductoConfiguration : IEntityTypeConfiguration<Producto> 
{
    public void Configure(EntityTypeBuilder<Producto> builder)
    {

        builder.ToTable("Producto");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
        .IsRequired();

        builder.Property(p => p.Titulo)
        .HasMaxLength(100)
        .IsRequired();

        builder.Property(p => p.Imagen)
        .IsRequired()
        .HasMaxLength(255);

        builder.Property(p => p.Precio)
        .IsRequired()
        .HasColumnType("int")
        .HasMaxLength(255);

        builder.HasOne(p => p.Inventario)
        .WithMany(p => p.Productos)
        .HasForeignKey(p => p.IdInventarioFk);

        builder.HasOne(p => p.Categoria)
        .WithMany(p => p.Productos)
        .HasForeignKey(p => p.IdCategoriaFk);

        
    }
}
