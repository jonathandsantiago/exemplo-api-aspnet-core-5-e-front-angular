using FavoDeMel.Domain.Usuarios;
using System.Threading.Tasks;

namespace FavoDeMel.Service.Interfaces
{
    public interface IUsuarioService : IServiceBase<int, Usuario>
    {
        Task<Usuario> GetByLoginPassword(string login, string password);
    }
}
