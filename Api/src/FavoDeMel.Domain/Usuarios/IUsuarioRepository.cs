using FavoDeMel.Domain.Interfaces;
using System.Threading.Tasks;

namespace FavoDeMel.Domain.Usuarios
{
    public interface IUsuarioRepository : IRepositoryBase<int, Usuario>
    {
        Task<Usuario> GetByLoginPassword(string login, string password);
        Task<bool> ExistsLogin(string login);
    }
}

