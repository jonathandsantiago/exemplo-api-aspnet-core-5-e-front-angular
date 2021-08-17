using FavoDeMel.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FavoDeMel.Api.Providers.Interface
{
    public interface IApiProvider
    {
        /// <summary>
        /// Adiciona providers para controle de injeção de dependencia no startup do projeto
        /// </summary>
        /// <param name="services">IServiceCollection da aplicação</param>
        /// <param name="settings">Configurações da aplicação descrita no appsettings</param>
        void AddProvider(IServiceCollection services, ISettings<string, object> settings);
    }
}
