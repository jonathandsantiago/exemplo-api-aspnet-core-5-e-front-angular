using FavoDeMel.Api.Providers.Interface;
using FavoDeMel.Domain.Extensions;
using FavoDeMel.Domain.Interfaces;
using FavoDeMel.Domain.Entities.Comandas;
using FavoDeMel.Domain.Entities.Produtos;
using FavoDeMel.Domain.Models.Settings;
using FavoDeMel.Domain.Entities.Usuarios;
using FavoDeMel.Repository;
using FavoDeMel.Repository.Common;
using FavoDeMel.Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FavoDeMel.Api.Providers
{
    public class DbProvider : IApiProvider
    {
        public void AddProvider(IServiceCollection services, ISettings<string, object> settings)
        {
            var dbSettings = settings.GetSetting<DbSettings>();

            services.AddDbContext<FavoDeMelDbContext>(options => options.UseNpgsql(dbSettings.ConnectionName));
            services.AddScoped<FavoDeMelDbContext>();
            services.AddScoped(typeof(IServiceCache), typeof(ServiceCache));

            services.AddTransient<ComandaRepository<FavoDeMelDbContext>>();
            services.AddTransient<ProdutoRepository<FavoDeMelDbContext>>();
            services.AddTransient<UsuarioRepository<FavoDeMelDbContext>>();

            services.AddTransient<IUsuarioRepository, UsuarioRepository<FavoDeMelDbContext>>();
            services.AddTransient<IProdutoRepository, ProdutoRepository<FavoDeMelDbContext>>();
            services.AddTransient<IComandaRepository, ComandaRepository<FavoDeMelDbContext>>();
        }
    }
}