using FavoDeMel.Domain.Dtos;
using FavoDeMel.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace FavoDeMel.Tests.Mocks
{
    public static class ServicesMock
    {
        public static IServiceCollection AddServicesMock(this IServiceCollection services)
        {
            services.AddSingleton(ObterGeradorGuidService());
            services.AddSingleton(ObterMensageriaService<ComandaDto>());
            services.AddSingleton(ObterMensageriaService<ProdutoDto>());
            services.AddSingleton(ObterMensageriaService<UsuarioDto>());

            return services;
        }

        public static IMensageriaService ObterMensageriaService<T>()
        {
            var mock = new Mock<IMensageriaService>();
            mock.Setup(c => c.Publish(It.IsAny<T>(), It.IsAny<string>()));
            mock.Setup(c => c.Publish(It.IsAny<string>(), It.IsAny<string>()));

            return mock.Object;
        }

        public static IGeradorGuidService ObterGeradorGuidService()
        {
            var mock = new Mock<IGeradorGuidService>();
            mock.Setup(c => c.GetNexGuid());

            return mock.Object;
        }
    }
}