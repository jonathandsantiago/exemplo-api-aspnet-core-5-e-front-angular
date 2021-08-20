using FavoDeMel.Domain.Interfaces;
using FavoDeMel.Domain.Entities.Usuarios;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;

namespace FavoDeMel.Tests.Mocks
{
    public static class RedisMock
    {
        public static IServiceCollection AddRedisMock(this IServiceCollection services)
        {
            services.AddSingleton(ObterServicoCache<Usuario, Guid>());

            return services;
        }

        public static IServiceCache ObterServicoCache<T, TId>()
        {
            var mock = new Mock<IServiceCache>();
            mock.Setup(c => c.ObterAsync<T, TId>(It.IsAny<TId>()));
            mock.Setup(c => c.SalvarAsync(It.IsAny<string>(), It.IsAny<T>(), It.IsAny<int>()));
            mock.Setup(c => c.RemoverAsync(It.IsAny<string>()));

            return mock.Object;
        }
    }
}