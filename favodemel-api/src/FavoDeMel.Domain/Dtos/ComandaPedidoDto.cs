using FavoDeMel.Domain.Comandas;

namespace FavoDeMel.Domain.Dtos
{
    public class ComandaPedidoDto : DtoBase<int>
    {
        public int ComandaId { get; set; }
        public int ProdutoId { get; set; }
        public string ProdutoNome { get; set; }
        public decimal ProdutoPreco { get; set; }
        public int Quantidade { get; set; }
        public decimal Total { get { return ProdutoPreco * Quantidade; } }
        public ComandaPedidoSituacao Situacao { get; set; }
    }
}
