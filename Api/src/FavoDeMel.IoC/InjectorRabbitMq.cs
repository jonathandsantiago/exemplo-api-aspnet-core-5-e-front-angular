using FavoDeMel.Domain.Configs;
using FavoDeMel.Messaging.BusServices;
using FavoDeMel.Messaging.Interfaces;
using GreenPipes;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FavoDeMel.IoC
{
    public static class InjectorRabbitMq
    {
        public static IServiceCollection AddRabbitMq(this IServiceCollection services, RabbitMqConfig config)
        {
            services.AddMassTransit(x =>
            {
                x.AddBus(context => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.ConfigureEndpoints(context);
                    cfg.Host(config.Url, config.VirtualHost, h =>
                    {
                        h.Username(config.Usuario);
                        h.Password(config.Senha);
                    });
                    cfg.UseMessageRetry(retry => retry.Interval(3, TimeSpan.FromSeconds(10)));
                }));
            });

            services.AddSingleton<IPublishEndpoint>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<ISendEndpointProvider>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<IEventBus, RabbitMqEventBus>();
            services.AddHostedService<BusService>();

            return services;
        }
    }
}