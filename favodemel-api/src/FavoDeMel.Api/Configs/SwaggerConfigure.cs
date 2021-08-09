using FavoDeMel.Api.Configs.Interfaces;
using FavoDeMel.Domain.Extensions;
using FavoDeMel.Domain.Interfaces;
using FavoDeMel.Domain.Models.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace FavoDeMel.Api.Configs
{
    public class SwaggerConfigure : IApiConfigure
    {
        public void AddAplication(IApplicationBuilder app, IWebHostEnvironment env, ISettings<string, object> settings, IEvents events)
        {
            SwaggerSettings authSettings = settings.GetSetting<SwaggerSettings>();

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint(authSettings.UrlEndpoint, authSettings.NameEndpoint));
            }
        }
    }
}
