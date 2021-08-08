using FavoDeMel.Domain.Common;
using System.Threading.Tasks;

namespace FavoDeMel.Domain.Interfaces
{
    public interface IRepositoryBase<TId, TEntity>
        where TEntity : Entity<TId>
    {
        /// <summary>
        /// Método responsável por editar
        /// </summary>
        /// <returns></returns>
        Task<TEntity> Editar(TEntity entidade);
        /// <summary>
        /// Método responsável por cadastrar
        /// </summary>
        /// <returns></returns>
        Task<TEntity> Inserir(TEntity entity);
        /// <summary>
        /// Método responsável por obter por id
        /// </summary>
        /// <returns></returns>
        Task<TEntity> ObterPorId(TId id);
        /// <summary>
        /// Método responsável por iniciar a transação
        /// </summary>
        /// <returns></returns>
        IUnitOfWork BeginTransaction(IValidator _validator = null);
    }
}