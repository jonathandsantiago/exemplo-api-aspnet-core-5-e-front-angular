using FavoDeMel.Domain.Comandas;
using FavoDeMel.Service.Common;
using FavoDeMel.Service.Interfaces;
using System.Linq;

namespace FavoDeMel.Service.Services
{
    public class ComandaService : ServiceBase<int, Comanda, IComandaRepository>, IComandaService
    {
        public ComandaService(IComandaRepository repository) : base(repository)
        { }

        public IQueryable<Comanda> GetComandasAbertas()
        {
            return _repository.GetComandasAbertas();
        }
    }
}
