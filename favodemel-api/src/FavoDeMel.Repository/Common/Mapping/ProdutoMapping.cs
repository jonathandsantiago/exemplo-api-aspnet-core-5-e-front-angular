using FavoDeMel.Domain.Entities.Produtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FavoDeMel.Repository.Common.Mapping
{
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produto");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).IsRequired()
                .HasColumnName("IdProduto");

            builder.Property(c => c.Nome);
            builder.Property(c => c.Preco);
            builder.Property(c => c.Ativo);
        }
    }
}