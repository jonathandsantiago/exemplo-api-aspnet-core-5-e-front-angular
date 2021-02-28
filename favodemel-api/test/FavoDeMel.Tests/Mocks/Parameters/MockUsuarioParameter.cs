using FavoDeMel.Domain.Usuarios;
using System.Collections.Generic;

namespace FavoDeMel.Tests.Mocks.Parameters
{
    public class MockUsuarioParameter
    {
        public bool Exists { get; set; }
        public IEnumerable<Usuario> Usuarios { get; set; }
        public Usuario Usuario { get; set; }
        public bool ExistsLogin { get; set; }
    }
}
