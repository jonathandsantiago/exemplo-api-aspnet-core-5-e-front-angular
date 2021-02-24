using FavoDeMel.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FavoDeMel.Domain.Interfaces
{
    public interface IRepositoryBase<TId, TEntity> : IRepositoryBase<TEntity>
        where TEntity : Entity<TId>
    {
        Task Editar(TEntity entidade);
        Task Inserir(TEntity entity);
        Task Excluir(TId id);
        bool Exists(TId id);
        Task<TEntity> ObterPorId(TId id);
    }

    public interface IRepositoryBase<TEntity> : IDisposable
        where TEntity : Entity
    {
        IUnitOfWork InstanceUnitOfWork(IValidator validator);
        void SetUnitOfWork(IUnitOfWork unitOfWork);
        IUnitOfWork BeginTransaction(IValidator _validator);
        Task Excluir<TId>(TId id);
        Task<TEntity> ObterPorId<TId>(TId id);
        Task<IEnumerable<TEntity>> ObterTodos();
        IQueryable<TEntity> ObterAsQueryable();
    }
}