using FavoDeMel.Domain.Entities.Comandas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FavoDeMel.Repository.Common.Mapping
{
    public class ComandaMapping : IEntityTypeConfiguration<Comanda>
    {
        public void Configure(EntityTypeBuilder<Comanda> builder)
        {
            builder.ToTable("Comanda");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).IsRequired()
                .HasColumnName("ComandaId");

            builder.HasOne(c => c.Garcom).WithMany();
            builder.Property(c => c.TotalAPagar);
            builder.Property(c => c.GorjetaGarcom);
            builder.Property(c => c.Codigo);
            builder.Property(c => c.DataCadastro);
        }
    }
}