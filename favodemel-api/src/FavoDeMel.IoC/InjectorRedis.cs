using FavoDeMel.Domain.Configs;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace FavoDeMel.IoC
{
    public static class InjectorRedis
    {
        public static IServiceCollection AddRedis(this IServiceCollection services, RedisConfig config)
        {
            services.AddStackExchangeRedisCache(o =>
            {
                o.ConfigurationOptions = ConfigurationOptions.Parse(config.Connection);
                o.InstanceName = config.Instance;
            });

            return services;
        }
    }
}