using FavoDeMel.Domain.Usuarios;
using FavoDeMel.EF.Repository.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
                InserirUsuariosDefault(context as BaseDbContext);
#endif
            }

            return host;
        }
        private static void InserirUsuariosDefault(BaseDbContext baseDbContext)
        {
            var usuarioDbSet = baseDbContext.Set<Usuario>();

            //if (usuarioDbSet.Count() == 0)
            //{
            //    usuarioDbSet.Add(new Usuario
            //    {
            //        Nome = "Administrador",
            //        Login = "Admin",
            //        Senha = StringHelper.CalculateMD5Hash("Admin"),
            //        Email = "admin@ewave.com.br",
            //        PessoaTipo = PessoaTipo.Juridica,
            //        Perfil = UsuarioPerfil.Administrador,
            //        Tipo = UsuarioTipo.Prestador,
            //        Ativo = true
            //    });
            //    baseDbContext.SaveChanges();
            //}
        }
    }
}

