using System;

namespace FavoDeMel.Domain.Dtos
{
    public class ProdutoDto : DtoBase<Guid?>
    {
        public string Nome { get; set; }
        public decimal Preco { get; set; }
    }
}
