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
        /// Método responsável por validar se já existe produto
        /// </summary>
        /// <returns></returns>
        Task<bool> NomeJaCadastrado(Guid id, string nome);
        /// <summary>
        /// Método responsável por obter todos paginado
        /// </summary>
        /// <returns></returns>
        Task<PagedList<Produto>> ObterTodosPaginado(int page = 1, int pageSize = 20);

        /// <summary>
        /// Método responsável por obter todos produtos
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Produto>> ObterTodos();
    }
}
