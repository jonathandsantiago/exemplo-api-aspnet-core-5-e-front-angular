using FavoDeMel.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FavoDeMel.Domain.Common
{
    public abstract class ServiceCacheBase
    {
        private readonly IServiceCache _serviceCache;

        public ServiceCacheBase(IServiceCache serviceCache)
        {
            _serviceCache = serviceCache;
        }

        protected string GerarChave<T>(string chave)
        {
            if (string.IsNullOrWhiteSpace(chave) || chave.Contains(":")) throw new ArgumentException("chave invalida.");

            Type tipo = typeof(T);
            return string.Concat(tipo.Name.ToLower(), ":", chave.ToLower());
        }

        protected async Task<T> Obter<T>(string chave)
        {
            var chaveReal = GerarChave<T>(chave);
            try
            {
                var result = await _serviceCache.ObterAsync<T>(chaveReal);
                return result;
            }
            catch (Exception)
            {
                return default;
            }
        }

        protected async Task Salvar<T>(string chave, T obj, int timeToLiveSec)
        {
            var chaveReal = GerarChave<T>(chave);

            await _serviceCache.SalvarAsync(chaveReal, obj, timeToLiveSec);
        }

        public virtual async Task SalvarCache<T, TId>(TId chave, T entidade)
        {
            await Salvar($"{chave}", entidade, 7200);
        }

        public virtual async Task<T> ObterPorIdInCache<T, TId>(TId id)
        {
            return await Obter<T>($"{id}");
        }
    }
}
