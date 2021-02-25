using FavoDeMel.Domain.Comandas;
using FavoDeMel.Service.Common;
using FavoDeMel.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FavoDeMel.Service.Services
{
    public class ComandaService : ServiceBase<int, Comanda, IComandaRepository>, IComandaService
    {
        public ComandaService(IComandaRepository repository) : base(repository)
        { }

        public async Task<Comanda> InserirOuEditar(Comanda comanda)
        {
            using (var dbTransaction = _repository.BeginTransaction(_validador))
            {
                if (_validador != null && !await ObterValidador<ComandaValidator>().Validar(comanda))
                {
                    return null;
                }

                comanda.Prepare();

                if (comanda.Id > 0)
                {
                    await _repository.Editar(comanda);
                }
                else
                {
                    await _repository.Inserir(comanda);
                }

                return await _repository.ObterPorId(comanda.Id);
            }
        }

        public async Task<IEnumerable<Comanda>> GetComandasAbertas()
        {
            return await _repository.ObterComandasAbertas();
        }

        public async Task<IList<Comanda>> ObterTodosPorSituacao(ComandaSituacao situacao)
        {
            var comandas = ObterAsQueryable()
               .Where(c => c.Situacao == situacao)
               .OrderByDescending(c => c.Id);
            return await comandas.ToListAsync();
        }
    }
}