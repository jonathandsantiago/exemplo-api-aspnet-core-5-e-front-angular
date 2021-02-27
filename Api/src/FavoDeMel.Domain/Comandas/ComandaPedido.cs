using FavoDeMel.Domain.Common;
using FavoDeMel.Domain.Produtos;
using System.ComponentModel.DataAnnotations;

namespace FavoDeMel.Domain.Comandas
{
    public class ComandaPedido : Entity<int>
    {
        [Required]
        public int ComandaId { get; set; }
        [Required]
        public Produto Produto { get; set; }
        [Required]
        public int Quantidade { get; set; }
        [Required]
        public ComandaPedidoSituacao Situacao { get; set; }

        public ComandaPedido()
        { }
    }
}
