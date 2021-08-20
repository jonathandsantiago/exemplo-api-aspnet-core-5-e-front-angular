using System;
using System.Linq;
using System.Linq.Expressions;

namespace FavoDeMel.Domain.Extensions
{
    public static class QueryableExtensions
    {
        /// <summary>
        /// Obter query com os itens paginado
        /// </summary>
        /// <param name="query">Query DbSet</param>
        /// <param name="orderBy">Expresão de ornenação</param>
        /// <param name="page">Pagina</param>
        /// <param name="pageSize">Quantidade por pagina</param>
        /// <param name="orderByDescending">Ordem crescente ou descrescente</param>
        /// <returns>Retorna queryable do DbSet paginado.</returns>
        public static IQueryable<T> PageBy<T, TKey>(this IQueryable<T> query, Expression<Func<T, TKey>> orderBy, int page, int pageSize, bool orderByDescending = true)
        {
            const int defaultPageNumber = 1;

            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            if (page <= 0)
            {
                page = defaultPageNumber;
            }

            query = orderByDescending ? query.OrderByDescending(orderBy) : query.OrderBy(orderBy);
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        /// <summary>
        /// Obter query com os itens pela condição
        /// </summary>
        /// <param name="query">Query DbSet</param>
        /// <param name="condition">Validação de inclusão ou não do predicado</param>
        /// <param name="predicate">Expresão da condição de busca</param>
        /// <returns>Retorna queryable do DbSet paginado.</returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
        {
            return condition
                ? query.Where(predicate)
                : query;
        }

    }
}
