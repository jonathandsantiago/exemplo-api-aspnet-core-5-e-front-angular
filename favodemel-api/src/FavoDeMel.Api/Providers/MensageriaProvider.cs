using FavoDeMel.Api.Providers.Interface;
using FavoDeMel.Domain.Extensions;
using FavoDeMel.Domain.Interfaces;
using FavoDeMel.Domain.Models.Settings;
using FavoDeMel.Service.Interfaces;
using FavoDeMel.Service.Services;
using GreenPipes;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FavoDeMel.Api.Providers
{
    public class MensageriaProvider : IApiProvider
    {
        public void AddProvider(IServiceCollection services, ISettings<string, object> settings)
        {
            var rabbitMqSettings = settings.GetSetting<RabbitMqSettings>();

            services.AddMassTransit(c =>
            {
                c.AddBus(context => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.ConfigureEndpoints(context);
                    cfg.Host(rabbitMqSettings.Url, rabbitMqSettings.Vhost, h =>
                    {
                        h.Username(rabbitMqSettings.Usuario);
                        h.Password(rabbitMqSettings.Senha);
                    });
                    cfg.UseMessageRetry(retry => retry.Interval(3, TimeSpan.FromSeconds(10)));

                    cfg.ConfigureJsonSerializer(settings =>
                    {
                        settings.DefaultValueHandling = DefaultValueHandling.Include;
                        return settings;
                    });

                    foreach (var commandName in ObterCommandsNameInAssembly())
                    {
                        cfg.ReceiveEndpoint(commandName, e => e.Bind(commandName));
                    }
                }));
            });

            services.AddSingleton<IPublishEndpoint>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<ISendEndpointProvider>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<IMensageriaService, MensageriaService>();
            services.AddHostedService<BusService>();
        }

        private IList<string> ObterCommandsNameInAssembly()
        {
            return typeof(IMensageriaCommand).Assembly.ExportedTypes
                 .Where(x => typeof(IMensageriaCommand).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                 .Select(c => c.Name).ToList(); ;
        }
    }
}