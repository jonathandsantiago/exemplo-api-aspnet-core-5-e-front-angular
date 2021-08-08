using FavoDeMel.Domain.Entities.Comandas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FavoDeMel.Repository.Common.Mapping
{
    public class ComandaPedidoMapping : IEntityTypeConfiguration<ComandaPedido>
    {
        public void Configure(EntityTypeBuilder<ComandaPedido> builder)
        {
            builder.ToTable("ComandaPedido");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).IsRequired()
                .HasColumnName("IdComandaPedido");

            builder.HasOne(c => c.Comanda).WithMany().IsRequired();
            builder.HasOne(c => c.Produto).WithMany().IsRequired();
            builder.Property(c => c.Quantidade);
            builder.Property(c => c.Situacao);
        }
    }
}