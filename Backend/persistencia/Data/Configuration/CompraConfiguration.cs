using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
public class CompraConfiguration : IEntityTypeConfiguration<Compra> 
{
    public void Configure(EntityTypeBuilder<Compra> builder)
    {

        builder.ToTable("Compra");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
        .IsRequired();

        builder.Property(p => p.Cantidad)
        .HasColumnType("int")
        .HasMaxLength(255)
        .IsRequired();

        builder.HasOne(p => p.Cliente)
        .WithMany(p => p.Compras)
        .HasForeignKey(p => p.IdClienteFk);

        builder.HasOne(p => p.Reparto)
        .WithMany(p => p.Compras)
        .HasForeignKey(p => p.IdRepartoFk);
        

    }
}
