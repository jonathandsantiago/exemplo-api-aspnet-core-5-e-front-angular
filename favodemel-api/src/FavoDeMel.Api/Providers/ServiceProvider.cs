using FavoDeMel.Api.Providers.Interface;
using FavoDeMel.Domain.Interfaces;
using FavoDeMel.Service.Interfaces;
using FavoDeMel.Service.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FavoDeMel.Api.Providers
{
    public class ServiceProvider : IApiProvider
    {
        public void AddProvider(IServiceCollection services, ISettings<string, object> settings)
        {
            services.AddScoped<IGeradorGuidService, GeradorGuidService>();
            services.TryAddScoped<IProdutoService, ProdutoService>();
            services.TryAddScoped<IComandaService, ComandaService>();
            services.TryAddScoped<IUsuarioService, UsuarioService>();
            services.TryAddScoped<IUsuarioService, UsuarioService>();
        }
    }
}