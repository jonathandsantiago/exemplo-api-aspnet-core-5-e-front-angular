using FavoDeMel.Domain.Common;
using System;
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
                AddMensagem(ComandaMessage.PedidoObrigatorio);
            }
            else if (comanda.Pedidos.Any(c => !ValidarPedido(c)))
            {
                return false;
            }

            if (!Enum.IsDefined(typeof(ComandaSituacao), comanda.Situacao))
            {
                AddMensagem(ComandaMessage.SituacaoInvalida);
            }

            return await base.Validar(comanda);
        }

        private bool ValidarPedido(ComandaPedido pedido)
        {
            if (pedido.Produto == null)
            {
                AddMensagem(ComandaMessage.ProdutoObrigatorio);
            }

            if (pedido.Quantidade <= 0)
            {
                AddMensagem(ComandaMessage.QuantidadeInvalida);
            }

            if (!Enum.IsDefined(typeof(ComandaPedidoSituacao), pedido.Situacao))
            {
                AddMensagem(ComandaMessage.SituacaoInvalida);
            }

            return IsValido;
        }

        public bool PermiteAlterarSituacao(int comandaId)
        {
            if (comandaId <= 0 || !_repository.Exists(comandaId))
            {
                AddMensagem(ComandaMessage.ComandaInvalida);
            }

            return IsValido;
        }
    }
}
