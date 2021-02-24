using FavoDeMel.Domain.Dtos;
using FavoDeMel.Domain.Models;
using FavoDeMel.Domain.Produtos;
using FavoDeMel.Service.Common;
using FavoDeMel.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FavoDeMel.Service.Services
{
    public class ProdutoService : ServiceBase<int, Produto, IProdutoRepository>, IProdutoService
    {
        public ProdutoService(IProdutoRepository repository) : base(repository)
        { }

        public async Task<Produto> InserirOuEditar(Produto produto)
        {
            using (var dbTransaction = _repository.BeginTransaction(_validador))
            {
                if (_validador != null && !await ObterValidador<ProdutoValidator>().Validar(produto))
                {
                    return null;
                }

                produto.Prepare();

                if (produto.Id > 0)
                {
                    await _repository.Editar(produto);
                }
                else
                {
                    await _repository.Inserir(produto);
                }

                return await _repository.ObterPorId(produto.Id);
            }
        }

        public async Task<PaginacaoDto<ProdutoDto>> ObterTodosPaginado(FiltroProduto filtro)
        {
            int pagina = filtro.Pagina;
            var produtos = ObterAsQueryable()
               .Where(c => !filtro.ProdutosIds.Any() || filtro.ProdutosIds.Contains(c.Id));
            int total = await produtos.CountAsync();

            if (total == 0)
            {
                return new PaginacaoDto<ProdutoDto>();
            }

            var items = filtro.Limite > 0 && total >= filtro.Limite ? await produtos.Skip(pagina * filtro.Limite).Take(filtro.Limite).ToListAsync() :
                await produtos.ToListAsync();

            return new PaginacaoDto<ProdutoDto>(total, filtro.Limite, pagina)
                .SetItens(items.OrderBy(c => c.Nome).ToList());
        }
    }
}
