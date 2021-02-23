using FavoDeMel.Domain.Usuarios;

namespace FavoDeMel.Api.Dtos
{
    public class UsuarioDto : DtoBase<int>
    {
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public UsuarioPerfil Perfil { get; set; }
    }
}
