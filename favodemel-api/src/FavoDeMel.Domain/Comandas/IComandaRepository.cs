using FavoDeMel.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FavoDeMel.Domain.Comandas
{
    public interface IComandaRepository : IRepositoryBase<int, Comanda>
    {
        Task<IEnumerable<Comanda>> ObterTodosPorSituacao(ComandaSituacao situacao);
        Task<Comanda> Fechar(int comandaId);
        Task<Comanda> Confirmar(int comandaId);
    }
}
