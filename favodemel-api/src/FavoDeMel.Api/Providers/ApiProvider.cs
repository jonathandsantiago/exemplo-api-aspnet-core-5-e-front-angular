using FavoDeMel.Api.Providers.Interface;
using FavoDeMel.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FavoDeMel.Api.Providers
{
    public class ApiProvider : IApiProvider
    {
        public void AddProvider(IServiceCollection services, ISettings<string, object> settings)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .WithOrigins(new string[]
                        {
                            "http://localhost:4200",
                            "https://localhost:4200",
                        })
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });
        }
    }
}
