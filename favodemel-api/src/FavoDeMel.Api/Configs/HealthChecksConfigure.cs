using FavoDeMel.Api.Configs.Interfaces;
using FavoDeMel.Domain.Interfaces;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;

namespace FavoDeMel.Api.Configs
{
    public class HealthChecksConfigure : IApiConfigure
    {
        public void AddAplication(IApplicationBuilder app, IWebHostEnvironment env, ISettings<string, object> settings)
        {
            app.UseHealthChecks("/health", new HealthCheckOptions
            {
                Predicate = r => r.Tags.Contains("essential")
            })
            .UseHealthChecks("/healthz", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
        }
    }
}
