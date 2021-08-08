using FavoDeMel.Domain.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace FavoDeMel.Api.Configs.Interfaces
{
    public interface IApiConfigure
    {
        void AddAplication(IApplicationBuilder app, IWebHostEnvironment env, ISettings<string, object> settings);
    }
}
