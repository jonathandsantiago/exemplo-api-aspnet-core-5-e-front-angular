using FavoDeMel.Domain.Common;
using FavoDeMel.Domain.Interfaces;
using FavoDeMel.Service.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FavoDeMel.Service.Common
{
    public class ServiceBase<TId, TEntity, TRepository> : ServiceBase<TEntity, TRepository>, IServiceBase<TId, TEntity>
        where TEntity : Entity<TId>
        where TRepository : IRepositoryBase<TId, TEntity>
    {
        protected IValidator<TId, TEntity> _validador;

        public IList<string> MensagensValidacao { get { return _validador?.Mensagens ?? new List<string>(); } }

        public ServiceBase(TRepository repository) : base(repository)
        {
            UnitOfWork = _repository.InstanceUnitOfWork(_validador);
        }

        public ServiceBase(IUnitOfWork unitOfWork, TRepository repository)
            : base(unitOfWork, repository)
        { }

        protected virtual TValidador ObterValidador<TValidador>()
            where TValidador : IValidator<TId, TEntity>
        {
            return (TValidador)_validador;
        }

        public virtual async Task<TEntity> ObterPorId(TId id)
        {
            return await _repository.ObterPorId(id);
        }

        public virtual async Task<bool> Excluir(TId id)
        {
            using (var dbTransaction = _repository.BeginTransaction(_validador))
            {
                if (_validador != null && !await _validador.PermiteExcluir(id))
                {
                    return false;
                }

                await _repository.Excluir(id);
                return true;
            }
        }

        public bool Exists(TId id)
        {
            return _repository.Exists(id);
        }
    }

    public class ServiceBase<TEntity, TRepository> : IServiceBase<TEntity>
        where TEntity : Entity
        where TRepository : IRepositoryBase<TEntity>
    {
        protected TRepository _repository;
        protected virtual IUnitOfWork UnitOfWork { get; set; }

        public ServiceBase(TRepository repository)
        {
            _repository = repository;
        }

        public ServiceBase(IUnitOfWork unitOfWork, TRepository repository)
        {
            _repository = repository;
            SetUnitOfWork(unitOfWork);
        }

        public virtual void SetUnitOfWork(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            _repository.SetUnitOfWork(unitOfWork);
        }

        public virtual async Task<TEntity> ObterPorId<TId>(TId id)
        {
            return await _repository.ObterPorId(id);
        }

        public virtual async Task<IEnumerable<TEntity>> ObterTodos()
        {
            return await _repository.ObterTodos();
        }

        public virtual IQueryable<TEntity> ObterAsQueryable()
        {
            return _repository.ObterAsQueryable();
        }

        public virtual void Dispose()
        {
            _repository.Dispose();
        }
    }
}