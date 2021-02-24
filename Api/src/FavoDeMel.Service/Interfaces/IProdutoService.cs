using FavoDeMel.Domain.Dtos;
using FavoDeMel.Domain.Models;
using FavoDeMel.Domain.Produtos;
using System.Threading.Tasks;

namespace FavoDeMel.Service.Interfaces
{
    public interface IProdutoService : IServiceBase<int, Produto>
    {
        Task<Produto> InserirOuEditar(Produto produto);
        Task<PaginacaoDto<ProdutoDto>> ObterTodosPaginado(FiltroProduto filtro);
    }
}
