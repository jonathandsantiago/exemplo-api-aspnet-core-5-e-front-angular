using FavoDeMel.Api.Providers.Interface;
using FavoDeMel.Domain.Extensions;
using FavoDeMel.Domain.Interfaces;
using FavoDeMel.Domain.Models.Settings;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace FavoDeMel.Api.Providers
{
    public class CacheProvider : IApiProvider
    {
        public void AddProvider(IServiceCollection services, ISettings<string, object> settings)
        {
            var redisSettings = settings.GetSetting<RedisSettings>();

            services.AddStackExchangeRedisCache(o =>
            {
                o.ConfigurationOptions = ConfigurationOptions.Parse(redisSettings.Connection);
                o.InstanceName = redisSettings.Instance;
            });
        }
    }
}