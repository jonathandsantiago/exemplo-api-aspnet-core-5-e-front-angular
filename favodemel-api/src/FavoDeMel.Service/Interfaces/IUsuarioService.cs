using FavoDeMel.Domain.Dtos;
using FavoDeMel.Domain.Dtos.Filtros;
using FavoDeMel.Domain.Entities.Usuarios;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FavoDeMel.Service.Interfaces
{
    public interface IUsuarioService : IServiceBase
    {
        /// <summary>
        /// Método responsável por obter usuário por login e senha
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        Task<UsuarioDto> Login(LoginDto loginDto);
        /// <summary>
        /// Método responsável por alterar a senha do usuário
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<bool> AlterarSenha(Guid id, string password);
        /// <summary>
        /// Método responsável por obter os usuários paginado
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        Task<PaginacaoDto<UsuarioDto>> ObterTodosPaginado(FiltroUsuario filtro);
        /// <summary>
        /// Método responsável por obter por perfil
        /// </summary>
        /// <param name="perfil"></param>
        /// <returns></returns>
        Task<IEnumerable<UsuarioDto>> ObterTodosPorPerfil(UsuarioPerfil perfil);
        /// <summary>
        /// Método responsável por cadastrar o usúario
        /// </summary>
        /// <param name="usuarioDto"></param>
        /// <returns></returns>
        Task<UsuarioDto> Inserir(UsuarioDto usuarioDto);
        /// <summary>
        /// Método responsável por editar o usúario
        /// </summary>
        /// <param name="usuarioDto"></param>
        /// <returns></returns>
        Task<UsuarioDto> Editar(UsuarioDto usuarioDto);
        /// <summary>
        /// Método responsável obter o usúario por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UsuarioDto> ObterPorId(Guid id);
    }
}