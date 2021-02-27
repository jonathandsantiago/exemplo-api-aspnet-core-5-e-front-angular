using FavoDeMel.Domain.Produtos;
using FavoDeMel.Redis.Repository.Abstractions;
using FavoDeMel.Redis.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FavoDeMel.Redis.Repository
{
    public class ProdutoCachedRepository : RedisRepositoryBase<Produto>, IProdutoRepository
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoCachedRepository(IServiceCache<Produto> serviceCache,
          IProdutoRepository produtoRepository,
          ILogger<ProdutoCachedRepository> logger)
          : base(serviceCache, logger, produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task Editar(Produto comanda)
        {
            await _produtoRepository.Editar(comanda);
            await base.Salvar($"{comanda.Id}", comanda, 7200);
        }

        public async Task Excluir(int id)
        {
            await _produtoRepository.Excluir(id);
            await base.Remover($"{id}");
        }

        public bool Exists(int id)
        {
            return _produtoRepository.Exists(id);
        }

        public async Task Inserir(Produto comanda)
        {
            await _produtoRepository.Inserir(comanda);
            await base.Salvar($"{comanda.Id}", comanda, 7200);
        }

        public async Task<bool> NomeJaCadastrado(int id, string nome)
        {
            return await _produtoRepository.NomeJaCadastrado(id, nome);
        }

        public async Task<Produto> ObterPorId(int id)
        {
            var comanda = await base.Obter($"{id}");

            if (comanda == null)
            {
                comanda = await _produtoRepository.ObterPorId(id);

                if (comanda != null)
                {
                    await base.Salvar($"{id}", comanda, 7200);
                }
            }

            return comanda;
        }
    }
}