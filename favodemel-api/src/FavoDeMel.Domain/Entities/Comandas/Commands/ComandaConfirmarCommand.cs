using FavoDeMel.Domain.Dtos;
using FavoDeMel.Domain.Interfaces;

namespace FavoDeMel.Domain.Entities.Comandas.Commands
{
    public class ComandaConfirmarCommand : IMensageriaCommand
    {
        public ComandaDto Comanda { get; }

        public ComandaConfirmarCommand(ComandaDto comanda)
        {
            Comanda = comanda;
        }
    }
}