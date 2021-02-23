using FavoDeMel.Domain.Usuarios;
using FavoDeMel.Service.Common;
using FavoDeMel.Service.Interfaces;
using System.Threading.Tasks;

namespace FavoDeMel.Service.Services
{
    public class UsuarioService : ServiceBase<int, Usuario, IUsuarioRepository>, IUsuarioService
    {
        public UsuarioService(IUsuarioRepository repository) : base(repository)
        { }

        public async Task<Usuario> GetByLoginPassword(string login, string password)
        {
            return await _repository.GetByLoginPassword(login, password);
        }
    }
}
