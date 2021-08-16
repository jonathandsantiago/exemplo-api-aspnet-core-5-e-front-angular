using FavoDeMel.Domain.Dtos;
using FavoDeMel.Domain.Interfaces;

namespace FavoDeMel.Domain.Entities.Comandas.Commands
{
    public class ComandaCadastroCommand : IMensageriaCommand
    {
        public ComandaDto Comanda { get; }

        public ComandaCadastroCommand(ComandaDto comanda)
        {
            Comanda = comanda;
        }
    }
}