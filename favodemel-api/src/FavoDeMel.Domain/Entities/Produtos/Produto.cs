using FavoDeMel.Domain.Common;
using System;

namespace FavoDeMel.Domain.Entities.Produtos
{
    public class Produto : Entity<Guid>
    {
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public bool Ativo { get; set; }
    }
}
