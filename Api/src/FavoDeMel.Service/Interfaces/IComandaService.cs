using FavoDeMel.Domain.Comandas;
using FavoDeMel.Domain.Dtos;
using FavoDeMel.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FavoDeMel.Service.Interfaces
{
    public interface IComandaService : IServiceBase<int, Comanda>
    {
        Task<IEnumerable<Comanda>> GetComandasAbertas();
        Task<PaginacaoDto<ComandaDto>> ObterTodosPaginado(FiltroComanda filtro);
        Task<Comanda> InserirOuEditar(Comanda comanda);
    }
}
