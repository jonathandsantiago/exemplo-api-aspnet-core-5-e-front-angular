using FavoDeMel.Domain.Produtos;
using Microsoft.EntityFrameworkCore;

namespace FavoDeMel.EF.Repository.Common.Mapping
{
    public static class ProdutoMap
    {
        public static ModelBuilder MapearProduto(this ModelBuilder builder)
        {
            builder.Entity<Produto>().ToTable("Produto");

            builder.Entity<Produto>()
                .HasIndex(c => c.Nome);

            return builder;
        }
    }
}