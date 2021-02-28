using FavoDeMel.Redis.Repository.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace FavoDeMel.Redis.Repository.Services
{
    public class ServiceCache<T> : IServiceCache<T>
    {
        private readonly IDistributedCache _cache;
        public ServiceCache(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task RemoverAsync(string key)
        {
            await _cache.RemoveAsync(key);
        }

        public async Task<T> ObterAsync(string key)
        {
            var json = await _cache.GetStringAsync(key);

            if (json == null)
                return default;

            return JsonConvert.DeserializeObject<T>(json);
        }

        public async Task SalvarAsync(string key, T objeto, int timeToLive = 300)
        {
            await _cache.SetStringAsync(key, JsonConvert.SerializeObject(objeto), new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(timeToLive)
            });
        }
    }
}
