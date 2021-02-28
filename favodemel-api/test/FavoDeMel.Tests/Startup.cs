using AutoMapper;
using FavoDeMel.Api.Factories;
using FavoDeMel.Domain.Configs;
using FavoDeMel.IoC;
using FavoDeMel.Tests.Mocks;
using FavoDeMel.Tests.Mocks.Parameters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

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
            var logger = SerilogFactory.GetLogger();
            var services = new ServiceCollection();
            services
               .AddLogging(loggingBuilder =>
               {
                   loggingBuilder
                   .ClearProviders()
                   .AddConsole()
                   .AddSerilog(logger, dispose: true);
               });

            services.AddControllers();

            Mapper.Reset();
            services.AddAutoMapper()
                  .AddMySql(Configuration)
                  .AddJwtBearer(Configuration)
                  .AddServices()
                  .AddHttpContextAccessor()
                  .AddRepositoryMock(serviceParameter)
                  .AddRabbitMqMock()
                  .AddRedisMock()
                  .AddMinio(new MinioConfig(Configuration));
            return services.BuildServiceProvider();
        }
    }
}
