using FavoDeMel.Domain.Common;

namespace FavoDeMel.Domain.Produtos
{
    public class Produto : Entity<int>
    {
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public string UlrImage { get; set; }
    }
}
