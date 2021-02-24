using FavoDeMel.Domain.Common;
using System.Linq;
using System.Threading.Tasks;

namespace FavoDeMel.Domain.Comandas
{
    public class ComandaValidator : ValidatorBase<int, Comanda, IComandaRepository>
    {
        public ComandaValidator(IComandaRepository repository) :
            base(repository)
        { }

        public override async Task<bool> Validar(Comanda comanda)
        {
            if (!comanda.Pedidos.Any())
            {
                AddMensagem("Pedido é obrigatório.");
            }
            else if (comanda.Pedidos.Any(c => !ValidarPedido(c)))
            {
                return false;
            }

            return await base.Validar(comanda);
        }

        private bool ValidarPedido(ComandaPedido pedido)
        {
            if (pedido.Produto == null)
            {
                AddMensagem("Produto é obrigatório.");
            }

            if (pedido.Quantidade <= 0)
            {
                AddMensagem("Quantidade inválida.");
            }

            return IsValido;
        }
    }
}
