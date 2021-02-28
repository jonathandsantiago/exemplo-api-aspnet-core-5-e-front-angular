using AutoMapper;
using FavoDeMel.Domain.Comandas;
using FavoDeMel.Domain.Dtos;
using FavoDeMel.Messaging.Interfaces;

namespace FavoDeMel.Messaging.Commands
{
    public class ComandaCommand : IMessageCommand
    {
        public ComandaDto Comanda { get; }

        public ComandaCommand(Comanda comanda)
        {
            Comanda = Mapper.Map<ComandaDto>(comanda);
        }
    }
}
