using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
public class InventarioConfiguration : IEntityTypeConfiguration<Inventario> 
{
    public void Configure(EntityTypeBuilder<Inventario> builder)
    {

        builder.ToTable("Inventario");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
        .IsRequired();

        builder.Property(p => p.Descripcion)
        .HasMaxLength(255)
        .IsRequired();

        builder.Property(p => p.FechaEntrada)
        .IsRequired()
        .HasColumnType("Date")
        .HasMaxLength(20);

        builder.Property(p => p.FechaSalida)
        .IsRequired()
        .HasColumnType("Date")
        .HasMaxLength(20);

        builder.HasOne(p => p.Sucursal)
        .WithMany(p => p.Inventarios)
        .HasForeignKey(p => p.IdSucursalFk);
        
    }
}
