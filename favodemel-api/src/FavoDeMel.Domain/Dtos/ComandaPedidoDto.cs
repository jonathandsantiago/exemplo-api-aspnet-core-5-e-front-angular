using FavoDeMel.Domain.Entities.Comandas;
using System;

namespace FavoDeMel.Domain.Dtos
{
    public class ComandaPedidoDto : DtoBase<Guid?>
    {
        public Guid? ComandaId { get; set; }
        public Guid ProdutoId { get; set; }
        public string ProdutoNome { get; set; }
        public decimal ProdutoPreco { get; set; }
        public int Quantidade { get; set; }
        public decimal Total { get { return ProdutoPreco * Quantidade; } }
        public ComandaPedidoSituacao Situacao { get; set; }
    }
}
