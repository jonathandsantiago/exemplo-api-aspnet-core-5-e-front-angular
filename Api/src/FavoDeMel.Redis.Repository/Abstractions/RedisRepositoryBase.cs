using FavoDeMel.Domain.Common;
using FavoDeMel.Domain.Interfaces;
using FavoDeMel.Redis.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FavoDeMel.Redis.Repository.Abstractions
{
    public abstract class RedisRepositoryBase<TEntity> : IRepositoryBase<TEntity>
        where TEntity : Entity
    {
        private readonly IServiceCache<TEntity> _serviceCache;
        protected readonly ILogger<RedisRepositoryBase<TEntity>> Logger;
        private readonly IRepositoryBase<TEntity> _repository;

        public RedisRepositoryBase(IServiceCache<TEntity> serviceCache,
            ILogger<RedisRepositoryBase<TEntity>> logger,
            IRepositoryBase<TEntity> repository)
        {
            _serviceCache = serviceCache;
            Logger = logger;
            _repository = repository;
        }

        protected string Nome => Tipo.Name;

        protected Type Tipo => typeof(TEntity);

        protected string GerarChave(string chave)
        {
            if (string.IsNullOrWhiteSpace(chave) || chave.Contains(":")) throw new ArgumentException("chave invalida.");

            return string.Concat(Nome.ToLower(), ":", chave.ToLower());
        }

        protected async Task Remover(string chave)
        {
            chave = GerarChave(chave);

            await _serviceCache.RemoverAsync(chave);
        }

        protected async Task<TEntity> Obter(string chave)
        {
            var chaveReal = GerarChave(chave);

            try
            {
                var result = await _serviceCache.ObterAsync(chaveReal);
                return result;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Erro ao obter objeto {tipo} do redis {chave}", typeof(TEntity), chave);
                return default;
            }
        }

        protected async Task Salvar(string chave, TEntity obj)
        {
            var chaveReal = GerarChave(chave);

            await _serviceCache.SalvarAsync(chaveReal, obj);
        }

        protected async Task Salvar(string chave, TEntity obj, int timeToLiveSec)
        {
            var chaveReal = GerarChave(chave);

            await _serviceCache.SalvarAsync(chaveReal, obj, timeToLiveSec);
        }

        public virtual IUnitOfWork BeginTransaction(IValidator _validator)
        {
            return _repository.BeginTransaction(_validator);
        }

        public virtual void Dispose()
        {
            _repository.Dispose();
        }

        public virtual IUnitOfWork InstanceUnitOfWork(IValidator validator)
        {
            return _repository.InstanceUnitOfWork(validator);
        }

        public virtual IQueryable<TEntity> ObterAsQueryable()
        {
            return _repository.ObterAsQueryable();
        }

        public virtual async Task<IEnumerable<TEntity>> ObterTodos()
        {
            return await _repository.ObterTodos();
        }

        public virtual void SetUnitOfWork(IUnitOfWork unitOfWork)
        {
            _repository.SetUnitOfWork(unitOfWork);
        }

        public virtual async Task Excluir<TId>(TId id)
        {
            await _repository.Excluir<TId>(id);
            await Remover($"{id}");
        }

        public virtual async Task<TEntity> ObterPorId<TId>(TId id)
        {
            var entity = await Obter($"{id}");

            if (entity == null)
            {
                entity = await _repository.ObterPorId<TId>(id);

                if (entity != null)
                {
                    await Salvar($"{id}", entity, 7200);
                }
            }

            return entity;
        }
    }
}
