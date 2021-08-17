using FavoDeMel.Api.Providers.Interface;
using FavoDeMel.Domain.Extensions;
using FavoDeMel.Domain.Interfaces;
using FavoDeMel.Domain.Models.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace FavoDeMel.Api.Providers
{
    public class CorsProvider : IApiProvider
    {
        public void AddProvider(IServiceCollection services, ISettings<string, object> settings)
        {
            CorsSettings corsSettings = settings.GetSetting<CorsSettings>();
            services.AddCors(options =>
            {
                options.AddPolicy(corsSettings.Policy,
                    builder => builder
                        .WithOrigins(corsSettings.WithOrigins)
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });
        }
    }
}
