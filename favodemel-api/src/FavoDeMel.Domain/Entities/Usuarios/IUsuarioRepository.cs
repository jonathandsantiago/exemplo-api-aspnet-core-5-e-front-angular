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
        /// Método responsável por obter usuário pelo login e senha
        /// </summary>
        /// <returns></returns>
        Task<Usuario> Login(string login, string password);
        /// <summary>
        /// Método responsável por validar se já existe o login cadastrado
        /// </summary>
        /// <returns></returns>
        Task<bool> ExistsLogin(string login);
        /// <summary>
        /// Método responsável por obter todos por perfil
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Usuario>> ObterTodosPorPerfil(UsuarioPerfil perfil);
        /// <summary>
        /// Método responsável por validar usuário pelo login e senha
        /// </summary>
        /// <returns></returns>
        Task<bool> UsuarioSenhaValido(string login, string password);
        /// <summary>
        /// Método responsável por obter todos paginado
        /// </summary>
        /// <returns></returns>
        Task<PagedList<Usuario>> ObterTodosPaginado(int page = 1, int pageSize = 20);
    }
}