using FavoDeMel.Api.Extensions;
using FavoDeMel.Domain.Events;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FavoDeMel.Api
{
    public class Startup
    {
        private readonly MensageriaEvents MensageriaEvents;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            MensageriaEvents = new MensageriaEvents();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton(MensageriaEvents);
            services.AddApiProvidersAssembly(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.AddApiConfigsAssembly(env, Configuration, MensageriaEvents);
        }
    }
}