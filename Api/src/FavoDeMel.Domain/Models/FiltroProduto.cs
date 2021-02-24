using System.Collections.Generic;
using System.Linq;

namespace FavoDeMel.Domain.Models
{
    public class FiltroProduto : Filtro
    {
        public string Produtos { get; set; }

        public IList<int> ProdutosIds
        {
            get
            {
                if (Produtos == null)
                {
                    return new List<int>();
                }

                return Produtos.Split(',').Select(c => int.Parse(c)).ToList();
            }
        }
    }
}