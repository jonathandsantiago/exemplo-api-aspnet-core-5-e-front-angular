using FavoDeMel.Domain.Common;
using FavoDeMel.Domain.Dtos;
using FavoDeMel.Domain.Dtos.Filtros;
using FavoDeMel.Domain.Dtos.Mappers;
using FavoDeMel.Domain.Interfaces;
using FavoDeMel.Domain.Entities.Produtos;
using FavoDeMel.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static FavoDeMel.Domain.Dtos.Mappers.ProdutoMappers;

namespace FavoDeMel.Service.Services
{
    public class ProdutoService : ServiceCacheBase, IProdutoService
    {
        private readonly IProdutoRepository _repository;
        private readonly IGeradorGuidService _geradorGuidService;
        private readonly ProdutoValidator _validator;
        public IList<string> MensagensValidacao => _validator?.Mensagens ?? new List<string>();

        public ProdutoService(IServiceCache serviceCache, 
            IProdutoRepository repository,
            IGeradorGuidService geradorGuidService,
            ProdutoValidator produtoValidator) : base(serviceCache)
        {
            _repository = repository;
            _validator = produtoValidator;
            _geradorGuidService = geradorGuidService;
        }

        public async Task<ProdutoDto> ObterPorIdAsync(Guid id)
        {
            var produtoCache = await ObterPorIdInCache<ProdutoDto, Guid>(id);

            if (produtoCache != null)
            {
                return produtoCache;
            }

            var produto = await _repository.ObterPorIdAsync(id);
            return produto?.ToDto();
        }

        public async Task<ProdutoDto> CadastrarAsync(ProdutoDto produtoDto)
        {
            using (var dbTransaction = _repository.BeginTransaction(_validator))
            {
                if (!await _validator.ValidarAsync(produtoDto))
                {
                    return null;
                }

                Produto produto = produtoDto.ToEntity();
                produto.Id = _geradorGuidService.GetNexGuid();
                produto.Prepare();
                Produto produtoDb = await _repository.CadastrarAsync(produto);
                ProdutoDto dto = produtoDb.ToDto();
                await SalvarCache(dto.Id, dto);
                return dto;
            }
        }

        public async Task<ProdutoDto> EditarAsync(ProdutoDto produtoDto)
        {
            using (var dbTransaction = _repository.BeginTransaction(_validator))
            {
                if (!await _validator.ValidarAsync(produtoDto))
                {
                    return null;
                }

                Produto produto = produtoDto.ToEntity();
                produto.Prepare();
                await _repository.EditarAsync(produto);
                ProdutoDto dto = produto.ToDto();
                await SalvarCache(dto.Id, dto);
                return dto;
            }
        }

        public virtual async Task<IEnumerable<ProdutoDto>> ObterTodosAsync()
        {
            var produtos = await _repository.ObterTodos();
            return produtos?.ToListDto();
        }

        public async Task<PaginacaoDto<ProdutoDto>> ObterTodosPaginadoAsync(FiltroProduto filtro)
        {
            var produtos = await _repository.ObterTodosPaginado(filtro.Pagina, filtro.Limite);
            return produtos?.ToPaginacaoDto<PaginacaoDto<ProdutoDto>>();
        }
    }
}
