using FavoDeMel.Domain.Comandas;
using FavoDeMel.Messaging.Commands;
using FavoDeMel.Messaging.Interfaces;
using FavoDeMel.Service.Common;
using FavoDeMel.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FavoDeMel.Service.Services
{
    public class ComandaService : ServiceBase<int, Comanda, IComandaRepository>, IComandaService
    {
        private readonly IEventBus _eventBus;

        public ComandaService(IComandaRepository repository,
            IEventBus eventBus) : base(repository)
        {
            _validador = new ComandaValidator(repository);
            _eventBus = eventBus;
        }

        public async Task<Comanda> Inserir(Comanda comanda)
        {
            using (var dbTransaction = _repository.BeginTransaction(_validador))
            {
                if (!await ObterValidador<ComandaValidator>().Validar(comanda))
                {
                    return null;
                }

                comanda.Prepare();
                await _repository.Inserir(comanda);
                await _eventBus.Send(new ComandaCommand(comanda));
                return comanda;
            }
        }

        public async Task<Comanda> Editar(Comanda comanda)
        {
            using (var dbTransaction = _repository.BeginTransaction(_validador))
            {
                if (!await ObterValidador<ComandaValidator>().Validar(comanda))
                {
                    return null;
                }

                comanda.Prepare();
                await _repository.Editar(comanda);
                await _eventBus.Send(new ComandaCommand(comanda));
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
                if (!ObterValidador<ComandaValidator>().PermiteAlterarSituacao(comandaId))
                {
                    return null;
                }

                Comanda comanda = await _repository.Confirmar(comandaId);
                await _eventBus.Send(new ComandaCommand(comanda));
                return comanda;
            }
        }

        public async Task<Comanda> Fechar(int comandaId)
        {
            using (var dbTransaction = _repository.BeginTransaction(_validador))
            {
                if (!ObterValidador<ComandaValidator>().PermiteAlterarSituacao(comandaId))
                {
                    return null;
                }

                Comanda comanda = await _repository.Fechar(comandaId);
                await _eventBus.Send(new ComandaCommand(comanda));
                return comanda;
            }
        }
    }
}