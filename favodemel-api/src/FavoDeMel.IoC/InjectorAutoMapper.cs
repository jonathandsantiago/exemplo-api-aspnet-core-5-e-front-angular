using AutoMapper;
using FavoDeMel.Domain.Dtos;
using Microsoft.Extensions.DependencyInjection;

namespace FavoDeMel.IoC
{
    public static class InjectorAutoMapper
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            Mapper.Initialize(cfg => GetConfiguration(cfg));
            Mapper.AssertConfigurationIsValid();

            return services;
        }

        private static void GetConfiguration(IMapperConfigurationExpression cfg)
        {
            cfg.AddProfile<ProdutoMapProfile>();
            cfg.AddProfile<UsuarioMapProfile>();
            cfg.AddProfile<ComandaMapProfile>();
        }
    }
}