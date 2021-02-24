using FavoDeMel.Domain.Dtos;
using FavoDeMel.Domain.Models;
using FavoDeMel.Domain.Usuarios;
using System.Threading.Tasks;

namespace FavoDeMel.Service.Interfaces
{
    public interface IUsuarioService : IServiceBase<int, Usuario>
    {
        Task<Usuario> Login(string login, string password);
        Task<Usuario> Inserir(Usuario usuario);
        Task<Usuario> Editar(Usuario usuario);
        Task<bool> AlterarSenha(int id, string password);
        Task<PaginacaoDto<UsuarioDto>> ObterTodosPaginado(FiltroUsuario filtro);
    }
}
