using FavoDeMel.Domain.Common;
using FavoDeMel.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FavoDeMel.EF.Repository.Common
{
    public class RepositoryBase<TId, TEntity> : RepositoryBase<TEntity>
       where TEntity : Entity<TId>
    {
        public RepositoryBase(BaseDbContext dbContext)
            : base(dbContext)
        { }

        public virtual async Task Excluir(TId id)
        {
            TEntity originalEntity = await ObterPorId(id);
            DbContext.Remove(originalEntity);
        }

        public virtual async Task Inserir(TEntity entity)
        {
            TEntity entityAdd = _dbSet.Add(entity).Entity;
            await DbContext.SaveChangesAsync();
            entity.Id = entityAdd.Id;
        }

        public virtual async Task Editar(TEntity entity)
        {
            var entityDb = await ObterPorId(entity.Id);
            DbContext.Entry(entityDb).State = EntityState.Modified;
            DbContext.Entry(entityDb).CurrentValues.SetValues(entity);
            await DbContext.SaveChangesAsync();
        }

        public bool Exists(TId id)
        {
            return _dbSet.Any(e => e.Id.Equals(id));
        }

        public virtual async Task<TEntity> ObterPorId(TId id)
        {
            return await _dbSet.FindAsync(id);
        }
    }

    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
     where TEntity : Entity
    {
        public BaseDbContext DbContext { get; }

        public IUnitOfWork UnitOfWork { get; private set; }

        protected DbSet<TEntity> _dbSet;

        public RepositoryBase(BaseDbContext dbContext)
        {
            DbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }

        public virtual IUnitOfWork InstanceUnitOfWork(IValidator validator)
        {
            UnitOfWork = new UnitOfWork(DbContext, validator);
            return UnitOfWork;
        }

        public virtual void SetUnitOfWork(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public virtual IUnitOfWork BeginTransaction(IValidator _validator)
        {
            if (UnitOfWork == null)
            {
                UnitOfWork = new UnitOfWork(DbContext, _validator);
            }

            UnitOfWork.SetValidator(_validator);
            UnitOfWork.BeginTransaction().Wait();
            return UnitOfWork;
        }

        public virtual async Task Excluir<TId>(TId id)
        {
            TEntity originalEntity = await ObterPorId(id);
            DbContext.Remove(originalEntity);
        }

        public virtual async Task<TEntity> ObterPorId<TId>(TId id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<TEntity>> ObterTodos()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual IQueryable<TEntity> ObterAsQueryable()
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

            if (DbContext == null) return;

            DbContext.Dispose();
        }
    }
}