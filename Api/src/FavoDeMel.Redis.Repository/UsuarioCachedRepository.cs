using FavoDeMel.Domain.Usuarios;
using FavoDeMel.Redis.Repository.Abstractions;
using FavoDeMel.Redis.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FavoDeMel.Redis.Repository
{
    public class UsuarioCachedRepository : RedisRepositoryBase<Usuario>, IUsuarioRepository
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioCachedRepository(IServiceCache<Usuario> serviceCache,
          IUsuarioRepository usuarioRepository,
          ILogger<UsuarioCachedRepository> logger)
          : base(serviceCache, logger, usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task Editar(Usuario usuario)
        {
            await _usuarioRepository.Editar(usuario);
            await base.Salvar($"{usuario.Id}", usuario, 7200);
        }

        public async Task Excluir(int id)
        {
            await _usuarioRepository.Excluir(id);
            await base.Remover($"{id}");
        }

        public bool Exists(int id)
        {
            return _usuarioRepository.Exists(id);
        }

        public async Task<bool> ExistsLogin(string login)
        {
            return await _usuarioRepository.ExistsLogin(login);
        }

        public async Task Inserir(Usuario usuario)
        {
            await _usuarioRepository.Inserir(usuario);
            await base.Salvar($"{usuario.Id}", usuario, 7200);
        }

        public async Task<Usuario> Login(string login, string password)
        {
            return await _usuarioRepository.Login(login, password);
        }

        public async Task<Usuario> ObterPorId(int id)
        {
            var usuario = await base.Obter($"{id}");

            if (usuario == null)
            {
                usuario = await _usuarioRepository.ObterPorId(id);

                if (usuario != null)
                {
                    await base.Salvar($"{id}", usuario, 7200);
                }
            }

            return usuario;
        }
    }
}