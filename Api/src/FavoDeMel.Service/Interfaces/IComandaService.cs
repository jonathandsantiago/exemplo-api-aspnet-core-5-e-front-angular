using FavoDeMel.Domain.Comandas;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FavoDeMel.Service.Interfaces
{
    public interface IComandaService : IServiceBase<int, Comanda>
    {
        Task<Comanda> Inserir(Comanda comanda);
        Task<Comanda> Editar(Comanda comanda);
        Task<IEnumerable<Comanda>> ObterTodosPorSituacao(ComandaSituacao situacao);
        Task<Comanda> Confirmar(int comandaId);
        Task<Comanda> Fechar(int comandaId);
    }
}
