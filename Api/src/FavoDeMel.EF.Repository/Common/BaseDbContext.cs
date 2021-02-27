using FavoDeMel.EF.Repository.Common.Mapping;
using Microsoft.EntityFrameworkCore;

namespace FavoDeMel.EF.Repository.Common
{
    public class BaseDbContext : DbContext
    {
        public BaseDbContext()
        { }

        public BaseDbContext(DbContextOptions<BaseDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.MapearUsuario();
            builder.MapearProduto();
            builder.MapearComanda();
            builder.MapearComandaPedido();
        }
    }
}
