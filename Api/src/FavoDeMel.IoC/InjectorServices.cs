using FavoDeMel.Service.Interfaces;
using FavoDeMel.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FavoDeMel.IoC
{
    public static class InjectorServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IProdutoService, ProdutoService>();
            services.AddTransient<IUsuarioService, UsuarioService>();
            services.AddTransient<IComandaService, ComandaService>();
            return services;
        }
    }
}
