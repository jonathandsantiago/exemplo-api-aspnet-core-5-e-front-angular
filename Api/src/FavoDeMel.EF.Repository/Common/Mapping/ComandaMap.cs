using FavoDeMel.Domain.Comandas;
using Microsoft.EntityFrameworkCore;

namespace FavoDeMel.EF.Repository.Common.Mapping
{
    public static class ComandaMap
    {
        public static ModelBuilder MapearComanda(this ModelBuilder builder)
        {
            builder.Entity<Comanda>().ToTable("Comanda");

            builder.Entity<Comanda>()
                .HasIndex(c => c.Situacao);

            builder.Entity<Comanda>().HasMany(c => c.Pedidos);

            builder.Entity<ComandaPedido>().ToTable("ComandaPedido");
            builder.Entity<ComandaPedido>();

            return builder;
        }
    }
}