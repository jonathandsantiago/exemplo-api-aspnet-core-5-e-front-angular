using FavoDeMel.Domain.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace FavoDeMel.Api.Configs.Interfaces
{
    public interface IApiConfigure
    {
        /// <summary>
        /// Adiciona as aplicações de build no startup do projeto
        /// </summary>
        /// <param name="app">IApplicationBuilder da aplicação</param>
        /// <param name="env">IWebHostEnvironment da aplicação</param>
        /// <param name="settings">Configurações da aplicação descrita no appsettings</param>
        void AddAplication(IApplicationBuilder app, IWebHostEnvironment env, ISettings<string, object> settings);
    }
}
