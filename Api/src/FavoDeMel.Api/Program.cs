using FavoDeMel.EF.Repository.Common;
using FavoDeMel.IoC;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace FavoDeMel.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build()
                .MigrateDbContext<BaseDbContext>()
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
