using FavoDeMel.Domain.Common;
using FavoDeMel.Domain.Interfaces;
using FavoDeMel.Service.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FavoDeMel.Service.Common
{
    public class ServiceBase<TId, TEntity, TRepository> : IServiceBase<TId, TEntity>
        where TEntity : Entity<TId>
        where TRepository : IRepositoryBase<TId, TEntity>
    {
        protected TRepository _repository;
        protected ValidatorBase<TId, TEntity, TRepository> _validator;
        public Dictionary<string, string> Result { get { return _validator?.Messages ?? new Dictionary<string, string>(); } }

        public ServiceBase(TRepository repository)
        {
            _repository = repository;
        }

        public virtual async Task<TEntity> GetById(TId id)
        {
            return await _repository.GetById(id);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _repository.GetAll();
        }

        public virtual async Task<bool> Add(TEntity entity)
        {
            if (_validator != null && !await _validator.Validate(entity))
            {
                return false;
            }

            _repository.Add(entity);
            await _repository.Commit();
            return true;

        }

        public virtual async Task<bool> Edity(TEntity entity)
        {
            if (_validator != null && !await _validator.Validate(entity))
            {
                return false;
            }

            await _repository.Edit(entity);
            await _repository.Commit();
            return true;
        }

        public virtual async Task<bool> Delete(TId id)
        {
            var entity = await GetById(id);
            if (_validator != null && !await _validator.AllowsRemove(entity))
            {
                return false;
            }

            await _repository.Delete(entity);
            await _repository.Commit();
            return true;
        }

        public virtual IQueryable<TEntity> GetQueryable()
        {
            return _repository.GetQueryable();
        }

        public virtual void Dispose()
        {
            _repository.Dispose();
        }

        public bool Exists(TId id)
        {
            return _repository.Exists(id);
        }
    }
}
