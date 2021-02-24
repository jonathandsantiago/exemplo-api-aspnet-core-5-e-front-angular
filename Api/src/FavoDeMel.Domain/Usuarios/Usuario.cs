using FavoDeMel.Domain.Common;

namespace FavoDeMel.Domain.Usuarios
{
    public class Usuario : Entity<int>
    {
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public UsuarioPerfil Perfil { get; set; }
        public bool Ativo { get; set; }
    }
}
