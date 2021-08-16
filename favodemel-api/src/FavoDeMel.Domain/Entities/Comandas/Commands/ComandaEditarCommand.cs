using FavoDeMel.Domain.Dtos;
using FavoDeMel.Domain.Interfaces;

namespace FavoDeMel.Domain.Entities.Comandas.Commands
{
    public class ComandaEditarCommand : IMensageriaCommand
    {
        public ComandaDto Comanda { get; }

        public ComandaEditarCommand(ComandaDto comanda)
        {
            Comanda = comanda;
        }
    }
}