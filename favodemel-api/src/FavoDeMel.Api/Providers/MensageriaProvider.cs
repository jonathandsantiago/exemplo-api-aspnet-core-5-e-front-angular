using Confluent.Kafka;
using FavoDeMel.Api.Providers.Interface;
using FavoDeMel.Domain.Extensions;
using FavoDeMel.Domain.Interfaces;
using FavoDeMel.Domain.Models.Settings;
using FavoDeMel.Service.Interfaces;
using FavoDeMel.Service.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FavoDeMel.Api.Providers
{
    public class MensageriaProvider : IApiProvider
    {
        public void AddProvider(IServiceCollection services, ISettings<string, object> settings)
        {
            var kafkaSettings = settings.GetSetting<KafkaSettings>();
            var config = new ProducerConfig
            {
                BootstrapServers = kafkaSettings.BootstrapServers
            };
            
            services.AddTransient(c => new ProducerBuilder<Null, string>(config));
            services.TryAddScoped<IMensageriaService, MensageriaService>();
        }
    }
}