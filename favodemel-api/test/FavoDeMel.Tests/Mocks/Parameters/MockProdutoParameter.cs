using FavoDeMel.Domain.Produtos;
using System.Collections.Generic;

namespace FavoDeMel.Tests.Mocks.Parameters
{
    public class MockProdutoParameter
    {
        public bool NomeJaCadastrado { get; set; }
        public bool Exists { get; set; }
        public IEnumerable<Produto> Produtos { get; set; }
        public Produto Produto { get; set; }
    }
}
