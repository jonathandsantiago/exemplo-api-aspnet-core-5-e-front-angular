using FavoDeMel.Api.Configs.Interfaces;
using FavoDeMel.Domain.Extensions;
using FavoDeMel.Domain.Interfaces;
using FavoDeMel.Domain.Models.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace FavoDeMel.Api.Configs
{
    public class CorsConfigure : IApiConfigure
    {
        public void AddAplication(IApplicationBuilder app, IWebHostEnvironment env, ISettings<string, object> settings)
        {
            CorsSettings corsSettings = settings.GetSetting<CorsSettings>();
            app.UseCors(corsSettings.Policy);
        }
    }
}
