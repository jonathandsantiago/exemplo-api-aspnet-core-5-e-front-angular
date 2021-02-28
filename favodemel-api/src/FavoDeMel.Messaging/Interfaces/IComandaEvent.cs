using FavoDeMel.Domain.Dtos;

namespace FavoDeMel.Messaging.Interfaces.Comandas
{
    public interface IComandaEvent : IMessageEvent
    {
        public ComandaDto Comanda { get;}
    }
}
