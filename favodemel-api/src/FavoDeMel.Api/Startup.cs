using FavoDeMel.Api.Extensions;
using FavoDeMel.Api.Factories;
using FavoDeMel.Domain.Configs;
using FavoDeMel.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace FavoDeMel.Api
{
    public class Startup
    {
        private readonly string corsPolicy = "CorsPolicy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var logger = SerilogFactory.GetLogger();    
            services
               .AddLogging(loggingBuilder =>
               {
                   loggingBuilder
                   .ClearProviders()
                   .AddConsole()
                   .AddSerilog(logger, dispose: true);
               });

            services.AddControllers();

            services
                  .AddAutoMapper()
                  .AddMySql(Configuration)
                  .AddJwtBearer(Configuration)
                  .AddServices()
                  .AddHttpContextAccessor()
                  .AddRepository()
                  .AddSwagger()
                  .AddRabbitMq(new RabbitMqConfig(Configuration))
                  .AddRedis(new RedisConfig(Configuration))
                  .AddMinio(new MinioConfig(Configuration))
                  .AddCors(options =>
                  {
                      options.AddPolicy(corsPolicy,
                          builder => builder
                              .AllowAnyOrigin()
                              .AllowAnyMethod()
                              .AllowAnyHeader()
                              .WithExposedHeaders("Content-Disposition"));
                  });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwaggerGen()
                .UseCors(corsPolicy)
                .UseHttpsRedirection()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseStaticFiles()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
        }
    }
}
