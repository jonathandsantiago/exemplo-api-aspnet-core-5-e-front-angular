using FavoDeMel.Domain.Entities.Usuarios;
using System;

namespace FavoDeMel.Domain.Dtos
{
    public class UsuarioDto : DtoBase<Guid?>
    {
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public UsuarioPerfil Perfil { get; set; }
        public decimal Comissao { get; set; }
        public bool Ativo { get; set; }
    }
}
