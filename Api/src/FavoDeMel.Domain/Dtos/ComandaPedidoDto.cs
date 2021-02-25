using FavoDeMel.Domain.Comandas;

namespace FavoDeMel.Domain.Dtos
{
    public class ComandaPedidoDto : DtoBase<int>
    {
        public int ComandaId { get; set; }
        public int ProdutoId { get; set; }
        public int ProdutoNome { get; set; }
        public int Quantidade { get; set; }
        public ComandaPedidoSituacao Situacao { get; set; }
    }
}
