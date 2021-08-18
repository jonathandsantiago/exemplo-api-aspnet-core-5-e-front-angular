using FavoDeMel.Domain.Dtos;
using FavoDeMel.Domain.Dtos.Filtros;
using FavoDeMel.Domain.Entities.Usuarios;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FavoDeMel.Service.Interfaces
{
    public interface IUsuarioService : IService
    {
        /// <summary>
        /// Obter usuário pelo login de forma assíncrona
        /// </summary>
        /// <param name="loginDto">Login do usuário</param>
        /// <returns>Retorna o usuário pelo login</returns>
        Task<UsuarioDto> LoginAsync(LoginDto loginDto);
        /// <summary>
        /// Alterar a senha do usuário pelo id de forma assíncrona
        /// </summary>
        /// <param name="idUsuario">Id do usuário</param>
        /// <param name="password">Nova senha</param>
        /// <returns>Retorna um boleano representando a eficácia da edição da senha</returns>
        Task<bool> AlterarSenhaAsync(Guid idUsuario, string password);
        /// <summary>
        /// Obter todos os usuários filtrado e paginado de forma assíncrona
        /// </summary>
        /// <param name="filtro">Filtro</param>
        /// <returns>Retorna todos os usuários filtrado e paginado</returns>
        Task<PaginacaoDto<UsuarioDto>> ObterTodosPaginadoAsync(FiltroUsuario filtro);
        /// <summary>
        /// Obter todos os usuários por perfil de forma assíncrona
        /// </summary>
        /// <param name="perfil">Perfil</param>
        /// <returns>Retorna todos os usuários por perfil</returns>
        Task<IEnumerable<UsuarioDto>> ObterTodosPorPerfilAsync(UsuarioPerfil perfil);
        /// <summary>
        /// Cadastar o usuário de forma assíncrona
        /// </summary>
        /// <param name="usuarioDto">Usuário Dto</param>
        /// <returns>Retorna o usuário cadastrado</returns>
        Task<UsuarioDto> CadastrarAsync(UsuarioDto usuarioDto);
        /// <summary>
        /// Editar o usuário de forma assíncrona
        /// </summary>
        /// <param name="usuarioDto">Usuário Dto</param>
        /// <returns>Retorna o usuário editado</returns>
        Task<UsuarioDto> EditarAsync(UsuarioDto usuarioDto);
        /// <summary>
        /// Obter o usuário de forma assíncrona
        /// </summary>
        /// <param name="idUsuario">Id do usuário</param>
        /// <returns></returns>
        Task<UsuarioDto> ObterPorIdAsync(Guid idUsuario);
    }
}