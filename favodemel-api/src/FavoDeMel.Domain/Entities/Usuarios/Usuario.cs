using FavoDeMel.Domain.Common;
using System;

namespace FavoDeMel.Domain.Entities.Usuarios
{
    public class Usuario : Entity<Guid>
    {
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public UsuarioPerfil Perfil { get; set; }
        public decimal Comissao { get; set; }
        public bool Ativo { get; set; }
    }
}
