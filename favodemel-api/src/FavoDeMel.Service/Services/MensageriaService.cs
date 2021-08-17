using FavoDeMel.Domain.Interfaces;
using FavoDeMel.Service.Interfaces;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace FavoDeMel.Service.Services
{
    public class MensageriaService : IMensageriaService
    {
        private readonly IBusControl _busControl;

        public MensageriaService(IBusControl busControl)
        {
            _busControl = busControl;
        }

        public async Task EnviarAsync<T>(T command) where T : IMensageriaCommand
        {
            var queue = command.GetType().Name;         

            var sendEndpoint = await _busControl.GetSendEndpoint(new Uri($"queue:{queue}"));
            await sendEndpoint.Send(command, new System.Threading.CancellationToken());
        }
    }
}