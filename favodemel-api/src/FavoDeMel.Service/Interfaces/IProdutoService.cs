using FavoDeMel.Domain.Dtos;
using FavoDeMel.Domain.Dtos.Filtros;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FavoDeMel.Service.Interfaces
{
    public interface IProdutoService : IServiceBase
    {
        /// <summary>
        /// Método responsável obter o produto por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ProdutoDto> ObterPorId(Guid id);
        /// <summary>
        /// Método responsável por cadastrar o produto
        /// </summary>
        /// <param name="produtoDto"></param>
        /// <returns>Retorna a produto</returns>
        Task<ProdutoDto> Inserir(ProdutoDto produtoDto);
        /// <summary>
        /// Método responsável por editar o produto
        /// </summary>
        /// <param name="produtoDto"></param>
        /// <returns>Retorna a produto</returns>
        Task<ProdutoDto> Editar(ProdutoDto produtoDto);
        /// <summary>
        /// Método responsável por obter todos paginados
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns>Retorna a produto</returns>
        Task<PaginacaoDto<ProdutoDto>> ObterTodosPaginado(FiltroProduto filtro);
        /// <summary>
        /// Método responsável por obter todos produtos
        /// </summary>
        /// <returns>Retorna a produto</returns>
        Task<IEnumerable<ProdutoDto>> ObterTodos();
    }
}
