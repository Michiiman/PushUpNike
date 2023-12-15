using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
public class ClienteConfiguration : IEntityTypeConfiguration<Cliente> 
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {

        builder.ToTable("Cliente");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
        .IsRequired();

        builder.Property(p => p.Nombre)
        .HasColumnType("varchar")
        .HasMaxLength(50)
        .IsRequired();

        builder.Property(p => p.Apellido)
        .IsRequired()
        .HasMaxLength(50);

        builder.Property(p => p.Telefono)
        .IsRequired()
        .HasMaxLength(20);

        builder.HasOne(p => p.Usuario)
        .WithOne(p => p.Cliente)
        .HasForeignKey<Cliente>(p => p.IdUsuarioFk);

    }
}
