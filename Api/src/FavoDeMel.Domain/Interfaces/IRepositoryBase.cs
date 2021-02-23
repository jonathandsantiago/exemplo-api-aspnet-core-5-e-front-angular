using FavoDeMel.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FavoDeMel.Domain.Interfaces
{
    public interface IRepositoryBase<TId, TEntity> : IDisposable
         where TEntity : Entity<TId>
    {
        void Add(TEntity entity);
        Task Delete(TEntity entidade);
        Task Edit(TEntity entidade);
        bool Exists(TId id);
        Task<TEntity> GetById(TId id);
        Task<IEnumerable<TEntity>> GetAll();
        Task Commit();
        IQueryable<TEntity> GetQueryable();
    }
}