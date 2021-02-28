using FavoDeMel.Domain.Usuarios;
using Microsoft.EntityFrameworkCore;

namespace FavoDeMel.EF.Repository.Common.Mapping
{
    public static class UsuarioMap
    {
        public static ModelBuilder MapearUsuario(this ModelBuilder builder)
        {
            builder.Entity<Usuario>().ToTable("Usuario");

            builder.Entity<Usuario>()
                .HasIndex(c => c.Login);
            builder.Entity<Usuario>()
                .HasIndex(c => c.Perfil);
            builder.Entity<Usuario>()
                .HasIndex(c => c.Ativo);

            return builder;
        }
    }
}