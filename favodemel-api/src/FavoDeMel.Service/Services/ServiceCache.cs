using FavoDeMel.Domain.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace FavoDeMel.Service.Services
{
    public class ServiceCache : IServiceCache
    {
        private readonly IDistributedCache _cache;

        public ServiceCache(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task RemoverAsync(string chave)
        {
            await _cache.RemoveAsync(chave);
        }

        public async Task SalvarAsync<T, TId>(TId id, T entidade, int cicloDeVida = 7200)
        {
            var chave = GerarChave<T>($"{id}");
            await _cache.SetStringAsync(chave, JsonConvert.SerializeObject(entidade), new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(cicloDeVida)
            });
        }

        public async Task<T> ObterAsync<T, TId>(TId id)
        {
            var chaveReal = GerarChave<T>($"{id}");
            try
            {
                var json = await _cache.GetStringAsync(chaveReal);

                if (json == null)
                    return default;

                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception)
            {
                return default;
            }
        }

        private string GerarChave<T>(string chave)
        {
            if (string.IsNullOrWhiteSpace(chave) || chave.Contains(":")) throw new ArgumentException("chave invalida.");

            Type tipo = typeof(T);
            return string.Concat(tipo.Name.ToLower(), ":", chave.ToLower());
        }
    }
}
