using FavoDeMel.Domain.Comandas;
using System.Linq;

namespace FavoDeMel.Service.Interfaces
{
    public interface IComandaService : IServiceBase<int, Comanda>
    {
        IQueryable<Comanda> GetComandasAbertas();
    }
}
