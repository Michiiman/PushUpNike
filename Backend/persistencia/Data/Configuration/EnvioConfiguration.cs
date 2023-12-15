using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
public class EnvioConfiguration : IEntityTypeConfiguration<Envio> 
{
    public void Configure(EntityTypeBuilder<Envio> builder)
    {

        builder.ToTable("Envio");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
        .IsRequired();

        builder.Property(p => p.Ruta)
        .HasMaxLength(255)
        .IsRequired();

        builder.Property(p => p.FechaEntrega)
        .IsRequired()
        .HasColumnType("Date")
        .HasMaxLength(20);

        builder.Property(p => p.FechaEnvio)
        .IsRequired()
        .HasColumnType("Date")
        .HasMaxLength(20);

        builder.Property(p => p.EstadoEnvio)
        .IsRequired()
        .HasMaxLength(60);

        builder.Property(p => p.Seguimiento)
        .IsRequired()
        .HasMaxLength(255);

        builder.Property(p => p.TotalPagar)
        .IsRequired()
        .HasColumnType("int")
        .HasMaxLength(255);

        builder.HasOne(p => p.Reparto)
        .WithMany(p => p.Envios)
        .HasForeignKey(p => p.IdRepartoFk);

    }
}
