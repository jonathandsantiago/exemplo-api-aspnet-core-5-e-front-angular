using FavoDeMel.Domain.Common;
using FavoDeMel.Domain.Entities.Produtos;
using System;

namespace FavoDeMel.Domain.Entities.Comandas
{
    public class ComandaPedido : Entity<Guid>
    {
        public Guid ComandaId { get; set; }
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
        public ComandaPedidoSituacao Situacao { get; set; }

        public ComandaPedido()
        { }
    }
}
