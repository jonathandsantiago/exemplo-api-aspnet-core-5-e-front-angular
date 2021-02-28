using System.Threading.Tasks;

namespace FavoDeMel.Messaging.Interfaces
{
    public interface IEventBus
    {
        Task Publish<T>(T @event) where T : IMessageEvent;

        Task Send<T>(T command) where T : IMessageCommand;
    }
}
