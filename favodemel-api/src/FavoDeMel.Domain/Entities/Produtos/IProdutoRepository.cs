using FavoDeMel.Domain.Common;
using FavoDeMel.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FavoDeMel.Domain.Entities.Produtos
{
    public interface IProdutoRepository : IRepositoryBase<Guid, Produto>
    {
        /// <summary>
        /// Validar se já existe o nome do produto de forma assíncrona
        /// </summary>
        /// <param name="id">Id do produto para que seja desconsiderado o mesmo na consulta</param>
        /// <param name="nome">Nome do produto</param>
        /// <returns>Retorna a validação se existe o produto pelo nome</returns>
        Task<bool> NomeJaCadastradoAsync(Guid id, string nome);
        /// <summary>
        /// Obter os produtos paginados de forma assíncrona
        /// </summary>
        /// <param name="page">Pagina atual</param>
        /// <param name="pageSize">Quantidade por pagina</param>
        /// <returns>Retorna os produtos paginado</returns>
        Task<PagedList<Produto>> ObterTodosPaginadoAsync(int page = 1, int pageSize = 20);
        /// <summary>
        /// Obter todos os produtos de forma assíncrona
        /// </summary>
        /// <returns>Retorna todos os produtos</returns>
        Task<IEnumerable<Produto>> ObterTodosAsync();
    }
}
