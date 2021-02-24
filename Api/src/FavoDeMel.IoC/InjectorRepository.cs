using FavoDeMel.Domain.Comandas;
using FavoDeMel.Domain.Produtos;
using FavoDeMel.Domain.Usuarios;
using FavoDeMel.EF.Repository;
using FavoDeMel.Redis.Repository;
using FavoDeMel.Redis.Repository.Interfaces;
using FavoDeMel.Redis.Repository.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FavoDeMel.IoC
{
    public static class InjectorRepository
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddScoped(typeof(IServiceCache<>), typeof(ServiceCache<>));

            services.AddScoped<UsuarioRepository>();
            services.AddScoped<IUsuarioRepository>(provider =>
               new UsuarioCachedRepository(
                   provider.GetService<IServiceCache<Usuario>>(),
                   provider.GetRequiredService<UsuarioRepository>(),
                   provider.GetService<ILogger<UsuarioCachedRepository>>()));

            services.AddScoped<ProdutoRepository>();
            services.AddScoped<IProdutoRepository>(provider =>
               new ProdutoCachedRepository(
                   provider.GetService<IServiceCache<Produto>>(),
                   provider.GetRequiredService<ProdutoRepository>(),
                   provider.GetService<ILogger<ProdutoCachedRepository>>()));

            services.AddScoped<ComandaRepository>();
            services.AddScoped<IComandaRepository>(provider =>
               new ComandaCachedRepository(
                   provider.GetService<IServiceCache<Comanda>>(),
                   provider.GetRequiredService<ComandaRepository>(),
                   provider.GetService<ILogger<ComandaCachedRepository>>()));

            return services;
        }
    }
}
