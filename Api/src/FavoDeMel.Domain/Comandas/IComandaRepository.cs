using FavoDeMel.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FavoDeMel.Domain.Comandas
{
    public interface IComandaRepository : IRepositoryBase<int, Comanda>
    {
        Task<IEnumerable<Comanda>> ObterComandasAbertas();
        Task<Comanda> FecharConta(int comandaId);
    }
}
