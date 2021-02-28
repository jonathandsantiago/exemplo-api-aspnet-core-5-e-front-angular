using FavoDeMel.Domain.Configs;
using FavoDeMel.Service.Interfaces;
using FavoDeMel.Service.Storage;
using Microsoft.Extensions.DependencyInjection;
using Minio;

namespace FavoDeMel.IoC
{
    public static class InjectorMinio
    {
        public static IServiceCollection AddMinio(this IServiceCollection services, MinioConfig config)
        {
            services.AddScoped<MinioClient>(sp =>
            {
                var client = new MinioClient(config.Endpoint, config.AccessKey, config.SecretKey);
                client.SetTraceOn();

                if (config.ForceSsl)
                {
                    client = client.WithSSL();
                }

                return client;
            });


            services.AddScoped(c => config);
            services.AddScoped<IFileStorageClient, FileStorageClient>();
            services.AddScoped<IFileStorageService, FileStorageService>();

            return services;
        }
    }
}