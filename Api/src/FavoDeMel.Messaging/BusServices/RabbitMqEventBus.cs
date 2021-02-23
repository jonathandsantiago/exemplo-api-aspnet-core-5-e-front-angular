using FavoDeMel.Messaging.Interfaces;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace FavoDeMel.Messaging.BusServices
{
    public class RabbitMqEventBus : IEventBus
    {
        private readonly IBusControl _busControl;

        public RabbitMqEventBus(IBusControl busControl)
        {
            _busControl = busControl;
        }

        public async Task Publish<T>(T @event) where T : IMessageEvent
        {
            await _busControl.Publish(@event);
        }

        public async Task Send<T>(T command) where T : IMessageCommand
        {
            var queue = command.GetType().FullName;
            var sendEndpoint = await _busControl.GetSendEndpoint(new Uri($"queue:{queue}"));

            await sendEndpoint.Send(command, new System.Threading.CancellationToken());
        }
    }
}
