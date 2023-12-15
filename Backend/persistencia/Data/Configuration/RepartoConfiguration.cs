using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
public class RepartoConfiguration : IEntityTypeConfiguration<Reparto> 
{
    public void Configure(EntityTypeBuilder<Reparto> builder)
    {

        builder.ToTable("Reparto");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
        .IsRequired();

        builder.Property(p => p.FechaSalida)
        .HasColumnType("Date")
        .HasMaxLength(20)
        .IsRequired();

        builder.HasOne(p => p.Sucursal)
        .WithMany(p => p.Repartos)
        .HasForeignKey(p => p.IdSucursalFk);
    }
}
