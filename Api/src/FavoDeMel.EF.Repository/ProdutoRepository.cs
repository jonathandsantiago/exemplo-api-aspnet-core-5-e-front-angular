using FavoDeMel.Domain.Produtos;
using FavoDeMel.EF.Repository.Common;

namespace FavoDeMel.EF.Repository
{
    public class ProdutoRepository : RepositoryBase<int, Produto>, IProdutoRepository
    {
        public ProdutoRepository(BaseDbContext dbContext) : base(dbContext)
        { }
    }
}
