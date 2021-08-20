using FavoDeMel.Domain.Common;
using FavoDeMel.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FavoDeMel.Domain.Entities.Usuarios
{
    public interface IUsuarioRepository : IRepositoryBase<Guid, Usuario>
    {
        /// <summary>
        /// Obter usuário pelo login de forma assíncrona
        /// </summary>
        /// <param name="login">Login do usuário</param>
        /// <param name="password">Senha do usuário</param>
        /// <returns>Retorna o usuário pelo login</returns>
        Task<Usuario> LoginAsync(string login, string password);
        /// <summary>
        /// Validar se existe o login cadastrado no sistema de forma assíncrona
        /// </summary>
        /// <returns></returns>
        Task<bool> ExistsLoginAsync(string login);
        /// <summary>
        /// Obter todos os usuários por perfil de forma assíncrona
        /// </summary>
        /// <param name="perfil">Perfil</param>
        /// <returns>Retorna todos os usuários por perfil</returns>
        Task<IEnumerable<Usuario>> ObterTodosPorPerfilAsync(UsuarioPerfil perfil);
        /// <summary>
        /// Validar se existe o usuário pelo login e senha de forma assíncrona
        /// </summary>
        /// <param name="login">Login do usuário</param>
        /// <param name="password">Senha do usuário</param>
        /// <returns>Retorna a validação do usuário pelo login e senha</returns>
        Task<bool> UsuarioSenhaValidoAsync(string login, string password);
        /// <summary>
        /// Obter os usuários paginados de forma assíncrona
        /// </summary>
        /// <param name="page">Pagina atual</param>
        /// <param name="pageSize">Quantidade por pagina</param>
        /// <returns>Retorna os usuários paginado</returns>
        Task<PagedList<Usuario>> ObterTodosPaginadoAsync(int page = 1, int pageSize = 20);
    }
}