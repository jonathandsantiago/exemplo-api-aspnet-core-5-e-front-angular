using FavoDeMel.Api.Providers.Interface;
using FavoDeMel.Domain.Extensions;
using FavoDeMel.Domain.Interfaces;
using FavoDeMel.Domain.Models.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace FavoDeMel.Api.Providers
{
    public class HealthChecksProvider : IApiProvider
    {
        public void AddProvider(IServiceCollection services, ISettings<string, object> settings)
        {
            var redisSettings = settings.GetSetting<RedisSettings>();
            var dbSettings = settings.GetSetting<DbSettings>();
            var rabbitMqSettings = settings.GetSetting<RabbitMqSettings>();

            services.AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy(), tags: new[] { "essential" })
                .AddNpgSql(dbSettings.ConnectionName, tags: new[] { "essential" })
                .AddRabbitMQ(rabbitConnectionString: $"amqp://{rabbitMqSettings.Usuario}:{rabbitMqSettings.Senha}@{rabbitMqSettings.Url}:{rabbitMqSettings.Port}/{rabbitMqSettings.Vhost}", tags: new[] { "essential" })
                .AddRedis(redisSettings.Connection, tags: new[] { "essential" });
        }
    }
}
