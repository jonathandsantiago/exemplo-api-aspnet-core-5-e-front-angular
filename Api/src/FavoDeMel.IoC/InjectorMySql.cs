using FavoDeMel.Domain.Usuarios;
using FavoDeMel.EF.Repository.Common;
using FavoDeMel.Framework.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;

namespace FavoDeMel.IoC
{
    public static class InjectorMySql
    {
        public static IServiceCollection AddMySql(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<BaseDbContext>(c => c.UseMySql(configuration.GetConnectionString("connectionName")));

            return services;
        }

        public static IHost MigrateDbContext<TContext>(this IHost host) where TContext : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetService<TContext>();
                context.Database.Migrate();
#if (DEBUG)
                InserirUsuarioDefault(context as BaseDbContext);
#endif
            }

            return host;
        }
        private static void InserirUsuarioDefault(BaseDbContext baseDbContext)
        {
            var usuarioDbSet = baseDbContext.Set<Usuario>();

            if (usuarioDbSet.Count() == 0)
            {
                usuarioDbSet.Add(new Usuario
                {
                    Nome = "Administrador",
                    Login = "Admin",
                    Password = StringHelper.CalculateMD5Hash("Admin"),
                    Perfil = UsuarioPerfil.Administrador,
                    Ativo = true
                });
                baseDbContext.SaveChanges();
            }
        }
    }
}

