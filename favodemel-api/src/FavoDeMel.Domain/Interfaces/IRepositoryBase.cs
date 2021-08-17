using FavoDeMel.Domain.Common;
using System.Threading.Tasks;

namespace FavoDeMel.Domain.Interfaces
{
    public interface IRepositoryBase<TId, TEntity>
        where TEntity : Entity<TId>
    {
        /// <summary>
        /// Edita a entidade no banco de dados de forma assíncrona
        /// </summary>
        /// <param name="entidade">Entidade a ser salva</param>
        /// <returns>Retorna a TEntity editada</returns>
        Task<TEntity> EditarAsync(TEntity entidade);
        /// <summary>
        /// Cadastra a entidade no banco de dados de forma assíncrona
        /// </summary>
        /// <param name="entidade">Entidade a ser salva</param>
        /// <returns>Retorna a TEntity cadastrada</returns>
        Task<TEntity> CadastrarAsync(TEntity entidade);
        /// <summary>
        /// Obter entidade pelo id de forma assíncrona
        /// Método responsável obter a entidade cadastrada por id
        /// </summary>
        /// <param name="id">Id do registro</param>
        /// <returns>Retorna a TEntity consultada pelo TId</returns>
        Task<TEntity> ObterPorIdAsync(TId id);
        /// <summary>
        /// Inicia transação de forma assíncrona
        /// </summary>
        /// <param name="validator">Validador</param>
        /// <returns>Retorna o IUnitOfWork da requisição</returns>
        IUnitOfWork BeginTransaction(IValidator validator = null);
    }
}