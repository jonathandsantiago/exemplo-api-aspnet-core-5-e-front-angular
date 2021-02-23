using FavoDeMel.Domain.Comandas;

namespace FavoDeMel.Api.Dtos
{
    public class ComandaPedidoDto : DtoBase<int>
    {
        public int ComandaId { get; set; }
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public ComandaPedidoSituacao Situacao { get; set; }
    }
}
