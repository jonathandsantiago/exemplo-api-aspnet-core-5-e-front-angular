using FavoDeMel.Domain.Dtos;
using FavoDeMel.Domain.Models;
using FavoDeMel.Domain.Usuarios;
using FavoDeMel.Framework.Helpers;
using FavoDeMel.Service.Common;
using FavoDeMel.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FavoDeMel.Service.Services
{
    public class UsuarioService : ServiceBase<int, Usuario, IUsuarioRepository>, IUsuarioService
    {
        public UsuarioService(IUsuarioRepository repository) : base(repository)
        {
            _validador = new UsuarioValidator(repository);
        }

        public async Task<Usuario> Login(string login, string password)
        {
            return await _repository.Login(login, password);
        }

        public async Task<Usuario> Inserir(Usuario usuario)
        {
            using (var dbTransaction = _repository.BeginTransaction(_validador))
            {
                if (!await ObterValidador<UsuarioValidator>().Validar(usuario))
                {
                    return null;
                }

                usuario.Prepare();
                await _repository.Inserir(usuario);
                return usuario;
            }
        }

        public async Task<Usuario> Editar(Usuario usuario)
        {
            using (var dbTransaction = _repository.BeginTransaction(_validador))
            {
                if (!await ObterValidador<UsuarioValidator>().Validar(usuario))
                {
                    return null;
                }

                usuario.Prepare();

                Usuario usuarioDb = await _repository.ObterPorId(usuario.Id);
                usuario.Login = usuarioDb.Login;
                usuario.Password = usuarioDb.Password;

                await _repository.Editar(usuario);
                return usuario;
            }
        }

        public async Task<bool> AlterarSenha(int id, string password)
        {
            using (var dbTransaction = _repository.BeginTransaction(_validador))
            {
                Usuario usuario = await _repository.ObterPorId(id);

                if (!ObterValidador<UsuarioValidator>().PermiteEditarSenha(usuario.Password, password))
                {
                    return false;
                }

                usuario.Password = StringHelper.CalculateMD5Hash(password);
                await _repository.Editar(usuario);
                return true;
            }
        }

        public async Task<PaginacaoDto<UsuarioDto>> ObterTodosPaginado(FiltroUsuario filtro)
        {
            int pagina = filtro.Pagina;
            var usuarios = ObterAsQueryable()
               .Where(c => (!filtro.UsuariosIds.Any() || filtro.UsuariosIds.Contains(c.Id)) &&
                     (filtro.Perfil == null || filtro.Perfil == c.Perfil));
            int total = await usuarios.CountAsync();

            if (total == 0)
            {
                return new PaginacaoDto<UsuarioDto>();
            }

            var items = filtro.Limite > 0 && total >= filtro.Limite ? await usuarios.Skip(pagina * filtro.Limite).Take(filtro.Limite).ToListAsync() :
                await usuarios.ToListAsync();

            return new PaginacaoDto<UsuarioDto>(total, filtro.Limite, pagina)
                .SetItens(items.OrderBy(c => c.Nome).ToList());
        }

        public async Task<IEnumerable<Usuario>> ObterTodosPorPerfil(UsuarioPerfil perfil)
        {
            return await _repository.ObterTodosPorPerfil(perfil);
        }
    }
}