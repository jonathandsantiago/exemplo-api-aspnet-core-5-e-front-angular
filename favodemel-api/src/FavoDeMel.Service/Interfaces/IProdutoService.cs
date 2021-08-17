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
        /// Obter o produto de forma assíncrona
        /// </summary>
        /// <param name="idProduto">Id do produto</param>
        /// <returns>Retorna o produto por id</returns>
        Task<ProdutoDto> ObterPorIdAsync(Guid idProduto);
        /// <summary>
        /// Cadastrar o produto de forma assíncrona
        /// </summary>
        /// <param name="produtoDto">Produto Dto</param>
        /// <returns>Retorna a produto cadastrado</returns>
        Task<ProdutoDto> CadastrarAsync(ProdutoDto produtoDto);
        /// <summary>
        /// Editar o produto de forma assíncrona
        /// </summary>
        /// <param name="produtoDto">Produto Dto</param>
        /// <returns>Retorna a produto</returns>
        Task<ProdutoDto> EditarAsync(ProdutoDto produtoDto);
        /// <summary>
        /// Obter todos os produtos filtrados e paginado de forma assíncrona
        /// </summary>
        /// <param name="filtro">Filtro</param>
        /// <returns>Retorna todos os produtos filtrado e paginado</returns>
        Task<PaginacaoDto<ProdutoDto>> ObterTodosPaginadoAsync(FiltroProduto filtro);
        /// <summary>
        /// Obter todos os produtos de forma assíncrona
        /// </summary>
        /// <returns>Retorna todos os produtos</returns>
        Task<IEnumerable<ProdutoDto>> ObterTodosAsync();
    }
}
