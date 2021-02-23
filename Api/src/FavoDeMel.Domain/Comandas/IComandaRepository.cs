using FavoDeMel.Domain.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace FavoDeMel.Domain.Comandas
{
    public interface IComandaRepository : IRepositoryBase<int, Comanda>
    {
        IQueryable<Comanda> GetComandasAbertas();
        Task<Comanda> FecharConta(Comanda comanda);
    }
}
