using FavoDeMel.Domain.Comandas;
using FavoDeMel.Redis.Repository.Abstractions;
using FavoDeMel.Redis.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FavoDeMel.Redis.Repository
{
    public class ComandaCachedRepository : RedisRepositoryBase<Comanda>, IComandaRepository
    {
        private readonly IComandaRepository _comandaRepository;

        public ComandaCachedRepository(IServiceCache<Comanda> serviceCache,
          IComandaRepository comandaRepository,
          ILogger<ComandaCachedRepository> logger)
          : base(serviceCache, logger, comandaRepository)
        {
            _comandaRepository = comandaRepository;
        }

        public async Task Editar(Comanda comanda)
        {
            await _comandaRepository.Editar(comanda);
            await base.Salvar($"{comanda.Id}", comanda, 7200);
        }

        public async Task Excluir(int id)
        {
            await _comandaRepository.Excluir(id);
            await base.Remover($"{id}");
        }

        public bool Exists(int id)
        {
            return _comandaRepository.Exists(id);
        }

        public async Task<Comanda> Fechar(int comandaId)
        {
            var comanda = await _comandaRepository.Fechar(comandaId);
            await base.Salvar($"{comanda.Id}", comanda, 7200);
            return comanda;
        }

        public async Task Inserir(Comanda comanda)
        {
            await _comandaRepository.Inserir(comanda);
            await base.Salvar($"{comanda.Id}", comanda, 7200);
        }

        public async Task<Comanda> ObterPorId(int id)
        {
            var comanda = await base.Obter($"{id}");

            if (comanda == null)
            {
                comanda = await _comandaRepository.ObterPorId(id);

                if (comanda != null)
                {
                    await base.Salvar($"{id}", comanda, 7200);
                }
            }

            return comanda;
        }

        public async Task<IEnumerable<Comanda>> ObterTodosPorSituacao(ComandaSituacao situacao)
        {
            return await _comandaRepository.ObterTodosPorSituacao(situacao);
        }

        public async Task<Comanda> Confirmar(int comandaId)
        {
            Comanda comanda = await _comandaRepository.Confirmar(comandaId);
            comanda.Situacao = ComandaSituacao.EmAndamento;
            await base.Salvar($"{comanda.Id}", comanda, 7200);
            return comanda;
        }
    }
}