using FavoDeMel.Api.Extensions;
using FavoDeMel.Domain.Entities.Comandas;
using FavoDeMel.Domain.Entities.Produtos;
using FavoDeMel.Domain.Entities.Usuarios;
using FavoDeMel.Service.Interfaces;
using FavoDeMel.Service.Services;
using FavoDeMel.Tests.Mocks;
using FavoDeMel.Tests.Mocks.Parameters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FavoDeMel.Tests
{
    public class Startup
    {
        private static IConfigurationRoot _configuration;

        public static IConfigurationRoot Configuration
        {
            get
            {
                if (_configuration == null)
                {
                    _configuration = new Startup().GetConfigurationRoot();
                }

                return _configuration;
            }
        }

        public IConfigurationRoot GetConfigurationRoot()
        {
            return new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json")
                .Build();
        }

        public static ServiceProvider GetServiceProvider(ServiceParameter serviceParameter)
        {
            var services = new ServiceCollection();
            services.AddControllers();

            services.AddHttpContextAccessor()
                  .AddRepositoryMock(serviceParameter)
                  .AddServicesMock()
                  .AddRedisMock();

            services.TryAddScoped<IProdutoService, ProdutoService>();
            services.TryAddScoped<IComandaService, ComandaService>();
            services.TryAddScoped<IUsuarioService, UsuarioService>();

            services.TryAddScoped<ProdutoValidator>();
            services.TryAddScoped<ComandaValidator>();
            services.TryAddScoped<UsuarioValidator>();

            return services.BuildServiceProvider();
        }
    }
}
