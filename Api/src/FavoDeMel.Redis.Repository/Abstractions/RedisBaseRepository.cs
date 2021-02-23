using FavoDeMel.Redis.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace FavoDeMel.Redis.Repository.Abstractions
{
    public abstract class RedisBaseRepository<T>
    {
        private readonly IServiceCache<T> _serviceCache;
        protected readonly ILogger<RedisBaseRepository<T>> Logger;

        public RedisBaseRepository(IServiceCache<T> serviceCache, ILogger<RedisBaseRepository<T>> logger)
        {
            _serviceCache = serviceCache;
            Logger = logger;
        }

        protected string Nome => Tipo.Name;

        protected Type Tipo => typeof(T);

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

        protected async Task<T> Obter(string chave)
        {
            var chaveReal = GerarChave(chave);

            try
            {
                var result = await _serviceCache.ObterAsync(chaveReal);
                return result;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Erro ao obter objeto {tipo} do redis {chave}", typeof(T), chave);
                return default;
            }
        }

        protected async Task Salvar(string chave, T obj)
        {
            var chaveReal = GerarChave(chave);

            await _serviceCache.SalvarAsync(chaveReal, obj);
        }

        protected async Task Salvar(string chave, T obj, int timeToLiveSec)
        {
            var chaveReal = GerarChave(chave);

            await _serviceCache.SalvarAsync(chaveReal, obj, timeToLiveSec);
        }
    }
}
