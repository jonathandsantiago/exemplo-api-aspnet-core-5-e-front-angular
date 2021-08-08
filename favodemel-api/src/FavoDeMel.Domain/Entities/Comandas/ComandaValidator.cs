using FavoDeMel.Domain.Common;
using FavoDeMel.Domain.Dtos;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FavoDeMel.Domain.Entities.Comandas
{
    public class ComandaValidator : ValidatorBase<ComandaDto>
    {
        private readonly IComandaRepository _repository;

        public ComandaValidator(IComandaRepository repository)
        {
            _repository = repository;
        }

        public override async Task<bool> Validar(ComandaDto comanda)
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

        private bool ValidarPedido(ComandaPedidoDto pedido)
        {
            if (pedido.ProdutoId == Guid.Empty)
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

        public bool PermiteAlterarSituacao(Guid comandaId)
        {
            if (comandaId == Guid.Empty || !_repository.Exists(comandaId))
            {
                AddMensagem(ComandaMessage.ComandaInvalida);
            }

            return IsValido;
        }
    }
}
