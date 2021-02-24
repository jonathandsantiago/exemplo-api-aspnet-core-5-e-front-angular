using FavoDeMel.Domain.Comandas;
using FavoDeMel.Redis.Repository.Abstractions;
using FavoDeMel.Redis.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
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

        public async Task Editar(Comanda usuario)
        {
            await _comandaRepository.Editar(usuario);
            await base.Salvar($"{usuario.Id}", usuario, 7200);
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

        public async Task<Comanda> FecharConta(int comandaId)
        {
            return await _comandaRepository.FecharConta(comandaId);
        }

        public async Task Inserir(Comanda usuario)
        {
            await _comandaRepository.Inserir(usuario);
            await base.Salvar($"{usuario.Id}", usuario, 7200);
        }

        public async Task<IEnumerable<Comanda>> ObterComandasAbertas()
        {
            return await _comandaRepository.ObterComandasAbertas();
        }

        public async Task<Comanda> ObterPorId(int id)
        {
            var usuario = await base.Obter($"{id}");

            if (usuario == null)
            {
                usuario = await _comandaRepository.ObterPorId(id);

                if (usuario != null)
                {
                    await base.Salvar($"{id}", usuario, 7200);
                }
            }

            return usuario;
        }
    }
}