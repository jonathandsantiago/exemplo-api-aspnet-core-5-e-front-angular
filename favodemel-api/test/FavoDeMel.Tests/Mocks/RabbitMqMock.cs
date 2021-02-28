using FavoDeMel.Messaging.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace FavoDeMel.Tests.Mocks
{
    public static class RabbitMqMock
    {
        public static IServiceCollection AddRabbitMqMock(this IServiceCollection services)
        {
            services.AddSingleton(ObterEventBus());

            return services;
        }

        public static IEventBus ObterEventBus()
        {
            var mock = new Mock<IEventBus>();
            mock.Setup(c => c.Publish(It.IsAny<IMessageEvent>()));
            mock.Setup(c => c.Send(It.IsAny<IMessageCommand>()));

            return mock.Object;
        }
    }
}