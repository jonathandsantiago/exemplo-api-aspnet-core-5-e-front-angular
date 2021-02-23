using FavoDeMel.Domain.Common;
using FavoDeMel.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FavoDeMel.EF.Repository.Common
{
    public class RepositoryBase<TId, TEntity> : IRepositoryBase<TId, TEntity>
      where TEntity : Entity<TId>
    {
        protected readonly BaseDbContext _dbContext;

        protected DbSet<TEntity> _dbSet;

        public RepositoryBase(BaseDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }

        public virtual void Add(TEntity entity)
        {
            entity.Id = _dbSet.Add(entity).Entity.Id;
        }

        public virtual async Task Delete(TEntity entity)
        {
            TEntity originalEntity = await GetById(entity.Id);
            _dbContext.Remove(originalEntity);
        }

        public virtual async Task Edit(TEntity entity)
        {
            _dbContext.Entry(await GetById(entity.Id))
                .CurrentValues
                .SetValues(entity);
        }

        public bool Exists(TId id)
        {
            return _dbSet.Any(e => e.Id.Equals(id));
        }

        public virtual async Task<TEntity> GetById(TId id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }

        public virtual IQueryable<TEntity> GetQueryable()
        {
            return _dbSet.AsQueryable();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;

            if (_dbContext == null) return;

            _dbContext.Dispose();
        }
    }
}