using FavoDeMel.Domain.Dtos;
using FavoDeMel.Domain.Interfaces;

namespace FavoDeMel.Domain.Entities.Comandas.Commands
{
    public class ComandaFecharCommand : IMensageriaCommand
    {
        public ComandaDto Comanda { get; }

        public ComandaFecharCommand(ComandaDto comanda)
        {
            Comanda = comanda;
        }
    }
}