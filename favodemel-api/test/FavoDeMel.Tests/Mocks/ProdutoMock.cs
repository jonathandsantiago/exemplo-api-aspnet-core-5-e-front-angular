using FavoDeMel.Domain.Entities.Produtos;
using System;
using System.Collections.Generic;

namespace FavoDeMel.Tests.Mocks
{
    public static class ProdutoMock
    {
        public static IEnumerable<Produto> ObterListaDeProdutos()
        {
            IList<Produto> produtos = new List<Produto>();
            produtos.Add(new Produto { Nome = "Coca Cola 2ML", Preco = Convert.ToDecimal(15) });
            produtos.Add(new Produto { Nome = "Guaraná 2ML", Preco = Convert.ToDecimal(10) });
            produtos.Add(new Produto { Nome = "Coca Cola 350ML", Preco = Convert.ToDecimal(5.50) });
            produtos.Add(new Produto { Nome = "Guaraná 350ML", Preco = Convert.ToDecimal(5.50) });
            produtos.Add(new Produto { Nome = "Heineken 250ML", Preco = Convert.ToDecimal(10) });
            produtos.Add(new Produto { Nome = "Pizza G", Preco = Convert.ToDecimal(70) });
            produtos.Add(new Produto { Nome = "Pizza M", Preco = Convert.ToDecimal(55.90) });
            produtos.Add(new Produto { Nome = "Pizza P", Preco = Convert.ToDecimal(35.90) });
            return produtos;
        }
    }
}
