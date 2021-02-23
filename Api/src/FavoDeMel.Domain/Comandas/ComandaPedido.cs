using FavoDeMel.Domain.Common;
using FavoDeMel.Domain.Produtos;

namespace FavoDeMel.Domain.Comandas
{
    public class ComandaPedido : Entity<int>
    {
        public Comanda Comanda { get; set; }
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
        public ComandaPedidoSituacao Situacao { get; set; }

        public ComandaPedido()
        { }
    }
}
