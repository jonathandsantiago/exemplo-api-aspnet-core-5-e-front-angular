using FavoDeMel.Domain.Produtos;
using FavoDeMel.EF.Repository.Common;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FavoDeMel.EF.Repository
{
    public class ProdutoRepository : RepositoryBase<int, Produto>, IProdutoRepository
    {
        public ProdutoRepository(BaseDbContext dbContext) : base(dbContext)
        { }

        public async Task<bool> NomeJaCadastrado(int id, string nome)
        {
            return await _dbSet.AnyAsync(c => c.Id != id && c.Nome == nome);
        }
    }
}
