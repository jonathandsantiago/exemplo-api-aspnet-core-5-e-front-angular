using FavoDeMel.Domain.Common;
using FavoDeMel.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FavoDeMel.Service.Interfaces
{
    public interface IServiceBase<TId, TEntity> : IServiceBase<TEntity>
       where TEntity : Entity<TId>
    {
        IList<string> MensagensValidacao { get; }
        Task<TEntity> ObterPorId(TId id);
        Task<bool> Excluir(TId id);
        bool Exists(TId id);
    }

    public interface IServiceBase<TEntity> : IDisposable
        where TEntity : Entity
    {
        void SetUnitOfWork(IUnitOfWork unitOfWork);
        IQueryable<TEntity> ObterAsQueryable();
        Task<TEntity> ObterPorId<TId>(TId id);
        Task<IEnumerable<TEntity>> ObterTodos();
    }
}