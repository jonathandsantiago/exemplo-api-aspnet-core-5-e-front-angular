using FavoDeMel.Api.Configs.Interfaces;
using FavoDeMel.Api.Providers.Interface;
using FavoDeMel.Domain.Events;
using FavoDeMel.Domain.Interfaces;
using FavoDeMel.Domain.Models.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace FavoDeMel.Api.Extensions
{
    public static class AppExtension
    {
        public static bool ConsoleAlreadyWritten = false;

        public static void AddApiProvidersAssembly(this IServiceCollection services, IConfiguration configuration)
        {
            ISettings<string, object> settings = ObterSettingsAssembly(configuration, services);
            var providers = typeof(Startup).Assembly.ExportedTypes.Where(x => typeof(IApiProvider).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(Activator.CreateInstance).Cast<IApiProvider>().ToList();

            providers.ForEach(install => install.AddProvider(services, settings));
        }

        public static void AddApiConfigsAssembly(this IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
        {
            ISettings<string, object> settings = ObterSettingsAssembly(configuration);
            var providers = typeof(Startup).Assembly.ExportedTypes.Where(x => typeof(IApiConfigure).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(Activator.CreateInstance).Cast<IApiConfigure>().ToList();

            providers.ForEach(install => install.AddAplication(app, env, settings));
        }

        private static ISettings<string, object> ObterSettingsAssembly(IConfiguration configuration, IServiceCollection services = null)
        {
            ISettings<string, object> settings = new Settings();
            var settginsAssembly = typeof(ISettings).Assembly.ExportedTypes.Where(x => typeof(ISettings).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
             .Select(c => Activator.CreateInstance(c, configuration)).Cast<ISettings>().ToList();

            settginsAssembly.ForEach(setting =>
            {
                if (services != null)
                {
                    services.AddSingleton(setting.GetType());
                }

                if (!ConsoleAlreadyWritten)
                {
                    Console.WriteLine(setting.ToString());
                }

                if (!settings.ContainsKey(setting.GetType().Name))
                {
                    settings.Add(setting.GetType().Name, setting);
                }
            });

            ConsoleAlreadyWritten = true;
            return settings;
        }
    }
}