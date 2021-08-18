using FavoDeMel.Domain.Entities.Comandas.Commands;
using FavoDeMel.Domain.Interfaces;
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
            services.AddSingleton(ObterMensageriaService<ComandaCadastroCommand>());
            services.AddSingleton(ObterMensageriaService<ComandaEditarCommand>());
            services.AddSingleton(ObterMensageriaService<ComandaFecharCommand>());
            services.AddSingleton(ObterMensageriaService<ComandaConfirmarCommand>());

            return services;
        }

        public static IMensageriaService ObterMensageriaService<T>() 
            where T : IMensageriaCommand
        {
            var mock = new Mock<IMensageriaService>();
            mock.Setup(c => c.EnviarAsync<T>(It.IsAny<T>()));

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