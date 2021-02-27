using FavoDeMel.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FavoDeMel.Domain.Usuarios
{
    public interface IUsuarioRepository : IRepositoryBase<int, Usuario>
    {
        Task<Usuario> Login(string login, string password);
        Task<bool> ExistsLogin(string login);
        Task<IEnumerable<Usuario>> ObterTodosPorPerfil(UsuarioPerfil perfil);
    }
}

