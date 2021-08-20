using FavoDeMel.Api.Configs.Interfaces;
using FavoDeMel.Api.Providers.Interface;
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

        /// <summary>
        /// Configura as injeções de dependências da aplicação, buscando todos as classes assinadas com IApiProvider
        /// </summary>
        /// <param name="services">IServiceCollection da aplicação</param>
        /// <param name="configuration">IConfiguration da aplicação</param>
        public static void AddApiProvidersAssembly(this IServiceCollection services, IConfiguration configuration)
        {
            ISettings<string, object> settings = ObterSettingsAssembly(configuration, services);
            var providers = typeof(Startup).Assembly.ExportedTypes.Where(x => typeof(IApiProvider).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(Activator.CreateInstance).Cast<IApiProvider>().ToList();

            providers.ForEach(install => install.AddProvider(services, settings));
        }

        /// <summary>
        /// Adiciona as configurações na aplicação de build assinadas com IApiConfigure
        /// </summary>
        /// <param name="app">IApplicationBuilder da aplicação</param>
        /// <param name="env">IWebHostEnvironment da aplicação</param>
        /// <param name="configuration">IConfiguration da aplicação</param>
        public static void AddApiConfigsAssembly(this IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
        {
            ISettings<string, object> settings = ObterSettingsAssembly(configuration);
            var providers = typeof(Startup).Assembly.ExportedTypes.Where(x => typeof(IApiConfigure).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(Activator.CreateInstance).Cast<IApiConfigure>().ToList();

            providers.ForEach(install => install.AddAplication(app, env, settings));
        }

        /// <summary>
        /// Obter todas as configurações da aplicação com base no appsettings assinadas com ISettings
        /// </summary>
        /// <param name="configuration">IConfiguration da aplicação</param>
        /// <param name="services">IServiceCollection da aplicação</param>
        /// <returns>Retorna todas as configurações da aplicação com base no appsettings assinadas com ISettings</returns>
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