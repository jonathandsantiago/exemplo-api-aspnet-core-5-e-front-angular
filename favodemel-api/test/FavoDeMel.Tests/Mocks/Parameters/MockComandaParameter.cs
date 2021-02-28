using FavoDeMel.Domain.Comandas;
using System.Collections.Generic;

namespace FavoDeMel.Tests.Mocks.Parameters
{
    public class MockComandaParameter
    {
        public bool Exists { get; set; }
        public IEnumerable<Comanda> Comandas { get; set; }
        public Comanda Comanda { get; set; }
    }
}
