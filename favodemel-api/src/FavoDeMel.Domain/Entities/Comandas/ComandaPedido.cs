using FavoDeMel.Domain.Common;
using FavoDeMel.Domain.Entities.Produtos;
using System;
using System.Text.Json.Serialization;

namespace FavoDeMel.Domain.Entities.Comandas
{
    public class ComandaPedido : Entity<Guid>
    {
        [JsonIgnore]
        public Comanda Comanda { get; set; }
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
        public ComandaPedidoSituacao Situacao { get; set; }

        public ComandaPedido()
        { }
    }
}
