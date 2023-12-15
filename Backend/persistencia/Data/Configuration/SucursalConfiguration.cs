using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
public class SucursalConfiguration : IEntityTypeConfiguration<Sucursal> 
{
    public void Configure(EntityTypeBuilder<Sucursal> builder)
    {

        builder.ToTable("Sucursal");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
        .IsRequired();

        builder.Property(p => p.Nombre)
        .HasMaxLength(255)
        .IsRequired();

        builder.Property(p => p.Ciudad)
        .IsRequired()
        .HasMaxLength(50);

        builder.Property(p => p.Direccion)
        .IsRequired()
        .HasMaxLength(50);

    }
}
