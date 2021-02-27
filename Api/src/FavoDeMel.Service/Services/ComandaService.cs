using FavoDeMel.Domain.Comandas;
using FavoDeMel.Service.Common;
using FavoDeMel.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FavoDeMel.Service.Services
{
    public class ComandaService : ServiceBase<int, Comanda, IComandaRepository>, IComandaService
    {
        public ComandaService(IComandaRepository repository) : base(repository)
        { }

        public async Task<Comanda> Inserir(Comanda comanda)
        {
            using (var dbTransaction = _repository.BeginTransaction(_validador))
            {
                if (_validador != null && !await ObterValidador<ComandaValidator>().Validar(comanda))
                {
                    return null;
                }

                comanda.Prepare();
                await _repository.Inserir(comanda);
                return comanda;
            }
        }

        public async Task<Comanda> Editar(Comanda comanda)
        {
            using (var dbTransaction = _repository.BeginTransaction(_validador))
            {
                if (_validador != null && !await ObterValidador<ComandaValidator>().Validar(comanda))
                {
                    return null;
                }

                comanda.Prepare();
                await _repository.Editar(comanda);
                return comanda;
            }
        }

        public async Task<IEnumerable<Comanda>> ObterTodosPorSituacao(ComandaSituacao situacao)
        {
            return await _repository.ObterTodosPorSituacao(situacao);
        }

        public async Task<Comanda> Confirmar(int comandaId)
        {
            using (var dbTransaction = _repository.BeginTransaction(_validador))
            {
                return await _repository.Confirmar(comandaId);
            }
        }

        public async Task<Comanda> Fechar(int comandaId)
        {
            using (var dbTransaction = _repository.BeginTransaction(_validador))
            {
                return await _repository.Fechar(comandaId);
            }
        }
    }
}