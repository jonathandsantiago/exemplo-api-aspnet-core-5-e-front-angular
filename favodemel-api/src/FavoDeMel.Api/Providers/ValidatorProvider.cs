using FavoDeMel.Api.Providers.Interface;
using FavoDeMel.Domain.Interfaces;
using FavoDeMel.Domain.Entities.Comandas;
using FavoDeMel.Domain.Entities.Produtos;
using FavoDeMel.Domain.Entities.Usuarios;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FavoDeMel.Api.Providers
{
    public class ValidatorProvider : IApiProvider
    {
        public void AddProvider(IServiceCollection services, ISettings<string, object> settings)
        {
            services.TryAddScoped<ProdutoValidator>();
            services.TryAddScoped<ComandaValidator>();
            services.TryAddScoped<UsuarioValidator>();
        }
    }
}