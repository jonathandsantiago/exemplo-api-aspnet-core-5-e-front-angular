using FavoDeMel.Domain.Comandas;
using FavoDeMel.EF.Repository.Common;
using System.Linq;
using System.Threading.Tasks;

namespace FavoDeMel.EF.Repository
{
    public class ComandaRepository : RepositoryBase<int, Comanda>, IComandaRepository
    {
        public ComandaRepository(BaseDbContext dbContext) : base(dbContext)
        { }

        public IQueryable<Comanda> GetComandasAbertas()
        {
            return _dbSet.Where(c => c.Situacao == ComandaSituacao.Aberta).AsQueryable();
        }

        public async Task<Comanda> FecharConta(Comanda comanda)
        {
            comanda = await GetById(comanda.Id);

            comanda.FecharConta();
            _dbContext.Entry(comanda)
               .CurrentValues.SetValues(comanda);

            return comanda;
        }
    }
}
