using FavoDeMel.Domain.Comandas;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FavoDeMel.Service.Interfaces
{
    public interface IComandaService : IServiceBase<int, Comanda>
    {
        Task<IEnumerable<Comanda>> GetComandasAbertas();
        Task<Comanda> InserirOuEditar(Comanda comanda);
        Task<IList<Comanda>> ObterTodosPorSituacao(ComandaSituacao situacao);
    }
}
