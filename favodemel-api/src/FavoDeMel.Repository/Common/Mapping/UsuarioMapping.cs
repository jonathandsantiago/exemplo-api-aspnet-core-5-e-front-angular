using FavoDeMel.Domain.Entities.Usuarios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FavoDeMel.Repository.Common.Mapping
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).IsRequired()
                .HasColumnName("UsuarioId");

            builder.Property(c => c.Nome);
            builder.Property(c => c.Login);
            builder.Property(c => c.Password);
            builder.Property(c => c.Perfil);
            builder.Property(c => c.Comissao);
            builder.Property(c => c.Ativo);
        }
    }
}